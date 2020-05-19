using Common.Utility.Extension;
using Common.Utility.Other;
using DataBase.IDataBase;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase.MongoDb
{
    public class MongoDbContext: DbContextAbstract, IMongoDbContext
    {
        private string ConnectionUrl { get; set; }
        private string DbName { get; set; }
        private ILogger Logger { get; set; }

        public MongoDbContext(string connectionUrl,string dbName)
        { 
            this.ConnectionUrl = connectionUrl;
            this.DbName = dbName;
            Logger=  typeof(MongoDbContext).Logger();
        }

        public IMongoDatabase CreateConnection()
        {
            var client = new MongoClient(ConnectionUrl);
            return client.GetDatabase(DbName);
        }

        private object GetPrimaryKeyValue<Tid>(Tid id)
        {
            object primaryKeyValue;
            if (id.ToString().Length.Equals(24))
            {
                primaryKeyValue = new ObjectId(id.ToString());
            }
            else
            {
                primaryKeyValue = id.ToString();
            }
            return primaryKeyValue;
        }

        public ObjectId PrimaryKey
        {
            get
            {
                return ObjectId.GenerateNewId(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
            }
        }

        /// <summary>
        /// 获取表mongodb
        /// </summary>
        /// <typeparam name="TDocument">表结构体类型</typeparam>
        /// <param name="collectionName">表名</param>
        /// <returns></returns>
        public IMongoCollection<TTableModel> GetCollection<TTableModel>()
        {
            return CreateConnection().GetCollection<TTableModel>(GetTableName<TTableModel>());
        }

        /// <summary>
        /// 获取表linq
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        public IMongoQueryable<TTableModel> GetQueryable<TTableModel>()
        {
            return CreateConnection().GetCollection<TTableModel>(GetTableName<TTableModel>())
                                     .AsQueryable();
        }

        public async Task<bool> CreateAsync<TTableModel>(TTableModel model) where TTableModel : class, new()
        {
            await GetCollection<TTableModel>().InsertOneAsync(model);
            return true;
        }

        public async Task<bool> CreateAsync<TTableModel>(TTableModel[] models) where TTableModel : class, new()
        {
            await GetCollection<TTableModel>().InsertManyAsync(models);
            return true;
        }

        public async Task<TTableModel> GetModelAsync<Tid, TTableModel>(Tid id) where TTableModel : class, new()
        {
            var filter = Builders<TTableModel>.Filter.Eq("_id", GetPrimaryKeyValue(id));
            var ms = await GetCollection<TTableModel>().Find(filter).ToListAsync();
            return ms.FirstOrDefault();
        }

        public TTableModel GetModel<Tid, TTableModel>(Tid id) where TTableModel : class, new()
        {
            
            var filter = Builders<TTableModel>.Filter.Eq("_id", GetPrimaryKeyValue(id));
            var ms = GetCollection<TTableModel>().Find(filter).ToList();
            return ms.FirstOrDefault();
        }

        public async Task<IEnumerable<TTableModel>> GetModelsAsync<TTableModel>() where TTableModel : class, new()
        {
            return await GetQueryable<TTableModel>().ToListAsync();
            //第一种 mongodb的用法
            //var filter = Builders<TTableModel>.Filter.Empty;
            //var t = await GetCollection<TTableModel>().FindAsync(filter);
            //return await t.ToListAsync();
            //第二种 linq的用法
            //var queryable = GetCollection<TTableModel>().AsQueryable();
            //var t = from p in queryable select p;
            //var c = t.ToList();
            //第三种 lambda表达式用法
            //return await GetQueryable<TTableModel>().ToListAsync();
        }

        public async Task<long> GetModelsCount<TTableModel>()
        {
            var filter = Builders<TTableModel>.Filter.Empty;
            return await GetCollection<TTableModel>().CountDocumentsAsync(filter);
        }

        public async Task<long> UpdateModelAsync<Tid, TTableModel>(Tid id, TTableModel model, string notFieldRegex = "^_id") where TTableModel : class, new()
        {
            var filter = Builders<TTableModel>.Filter.Eq("_id", GetPrimaryKeyValue(id));
            var fileds = ReflectHelper.ConvertToDictionary(model, notFieldRegex);//只有主键不更新
            var updateDefs = new List<UpdateDefinition<TTableModel>>();
            foreach (var item in fileds)
            {
                updateDefs.Add(Builders<TTableModel>.Update.Set(item.Key,item.Value));
            }
            var update = Builders<TTableModel>.Update.Combine(updateDefs);
            var ur = await GetCollection<TTableModel>().UpdateManyAsync(filter, update);
            //内容没有更改的时候也要返回成功
            return ur.ModifiedCount > 0 ? ur.ModifiedCount : 1;
        }

        public async Task<long> DeleteAsync<Tid, TTableModel>(Tid[] idArray) where TTableModel : class, new()
        {
            var _type = typeof(Tid) == typeof(ObjectId);
            var idList = new List<object>();
            foreach (var id in idArray)
            {
                if (_type)
                {
                    idList.Add(id);
                }
                else
                {
                    idList.Add(GetPrimaryKeyValue(id));
                }
            }
            var filter = Builders<TTableModel>.Filter.In("_id", idList);
            var dr= await GetCollection<TTableModel>().DeleteManyAsync(filter);
            return dr.DeletedCount;
        }

        public async Task<long> DeleteAsync<TTableModel>(TTableModel model) where TTableModel : class, new()
        {
            var filterDefs = new List<FilterDefinition<TTableModel>>();
            var fileds = ReflectHelper.ConvertToDictionary(model);
            foreach (var item in fileds)
            {
                filterDefs.Add(Builders<TTableModel>.Filter.Eq(item.Key, item.Value));
            }
            var filter = Builders<TTableModel>.Filter.And(filterDefs);
            var dr = await GetCollection<TTableModel>().DeleteManyAsync(filter);
            return dr.DeletedCount;
        }
    }
}
