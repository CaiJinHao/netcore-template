using CodeGenerator.App.DbModels;
using CodeGenerator.App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.App.Extensions;
using CodeGenerator.App.Models;
using RazorEngine;
using System.IO;
using RazorEngine.Templating;

namespace CodeGenerator.App.BuildFiles
{
    public class BuildModels
    {
        private TableRepository tableRepository { get; set; }
        private ColumnRepsitory columnRepsitory { get; set; }
        /// <summary>
        /// 构建Models层
        /// </summary>
        public BuildModels()
        {
            tableRepository = RepositoryFactory.CreateTableRepository();
            columnRepsitory = RepositoryFactory.CreateColumnRepsitory();
        }

        /// <summary>
        /// 开始构建
        /// </summary>
        /// <param name="tableStr">表名以,号隔开</param>
        /// <returns></returns>
        public async Task<string> BuildStart(string tableStr, string templateStr)
        {
            var tables = await tableRepository.GetModelsAsync();
            if (!string.IsNullOrEmpty(tableStr))
            {
                var tlist = tableStr.Split(',');
                tables = tables.Where(a => tlist.Contains(a.table_name));
            }
            return await BuildModelsStart(tables, templateStr);
        }

        /// <summary>
        /// 构建Models层
        /// </summary>
        /// <returns></returns>
        private async Task<string> BuildModelsStart(IEnumerable<TablesModel> tables, string templateStr)
        {
            foreach (var t in tables)
            {
                var cols = await columnRepsitory.GetModelsAsync(t.table_name);
                foreach (var c in cols)
                {
                    c.data_type = c.data_type.ConvertType();
                }

                var primaryKey = cols.Where(a => a.primary_key).First();
                var razorModelData = new ModelsFileModel()
                {
                    table_name = t.table_name,
                    table_comment = t.table_comment,
                    columns = cols,
                    primary_key_name = primaryKey.column_name,
                    primary_key_data_type = primaryKey.data_type,
                    table_name_pascal = t.table_name.ConvertToPascal(),
                    table_name_camel = t.table_name.ConvertToCamel(),
                    table_name_lower = t.table_name.ConvertToLower(),
                    name_space = StaticConfig.AppSettings.Template.NameSpace
                };

                var templatelist = StaticConfig.AppSettings.Template.TemplateFiles;
                if (!string.IsNullOrEmpty(templateStr))
                {
                    var tlist = templateStr.Split(',').ToList();
                    templatelist = templatelist.Where(a => tlist.Contains(a.FileDirName)).ToArray();
                }
                foreach (var template in templatelist)
                {
                    try
                    {
                        var fileModel = new TemplateFilesModel();
                        fileModel.FileDirName = template.FileDirName;
                        fileModel.FileName = template.FileName;
                        fileModel.FileSuf = template.FileSuf;
                        if (template.FileSuf.EndsWith("html"))
                        {
                            fileModel.FileName = fileModel.FileName.ToLower();
                            fileModel.FileSavePath = Path.Combine(StaticConfig.AppSettings.Template.SaveFilesPath, fileModel.FileDirName, t.table_name.ConvertToLower(), fileModel.FileName + fileModel.FileSuf);
                        }
                        else
                        {
                            fileModel.FileName = string.Format(fileModel.FileName, t.table_name).ConvertToPascal();
                            fileModel.FileSavePath = Path.Combine(StaticConfig.AppSettings.Template.SaveFilesPath, fileModel.FileDirName, fileModel.FileName + fileModel.FileSuf);
                        }
                        //模板构建文件
                        await CreateModelsFiels(fileModel, razorModelData, template.TemplateFile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            return StaticConfig.AppSettings.Template.SaveFilesPath;
        }

        /// <summary>
        /// 创建DbModels
        /// </summary>
        /// <param name="razorModelData"></param>
        /// <returns></returns>
        private async Task CreateModelsFiels(TemplateFilesModel fileModel, ModelsFileModel razorModelData, string templateFile)
        {
            string razorTemplate = await templateFile.ReadFileAsync();
            var DbModelContent = Engine.Razor.RunCompile(razorTemplate, fileModel.FileName, null, razorModelData);
            fileModel.FileSavePath.SaveFile(DbModelContent);
        }

        private void CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
