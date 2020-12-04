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
            var _template = StaticConfig.AppSettings.Template;
            foreach (var t in tables.ToArray())
            {
                var cols = (await columnRepsitory.GetModelsAsync(t.table_name)).ToArray();
                if (cols.Length <= 0)
                {
                    continue;
                }
                foreach (var c in cols)
                {
                    c.data_type = c.data_type.ConvertType();
                }

                var primaryKey = cols.Where(a => a.primary_key).FirstOrDefault();
                if (primaryKey == null)
                {
                    primaryKey = cols.First();
                }

                var razorModelData = new ModelsFileModel()
                {
                    api_version = _template.ApiVersion,
                    api_controller_name_space = _template.ApiControllerNameSpace,
                    table_name = t.table_name,
                    table_comment = t.table_comment,
                    columns = cols,
                    primary_key_name = primaryKey.column_name,
                    primary_key_data_type = primaryKey.data_type,
                    name_space = _template.NameSpace
                };

                var _table_name_rename = razorModelData.table_name;
                if (_template.TableRename != null)
                {
                    foreach (var _regexModel in _template.TableRename)
                    {
                        if (!string.IsNullOrEmpty(_regexModel.SearchRegexStr))
                        {
                            _table_name_rename = _regexModel.SearchRegex.Replace(_table_name_rename, _regexModel.NewContent);
                        }
                    }
                }
                razorModelData.table_name_rename = _table_name_rename;

                razorModelData.table_name_pascal = razorModelData.table_name_rename.ConvertToPascal();
                razorModelData.table_name_camel = razorModelData.table_name_rename.ConvertToCamel();
                razorModelData.table_name_lower = razorModelData.table_name_rename.ConvertToLower();

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
                            fileModel.FileSavePath = Path.Combine(StaticConfig.AppSettings.Template.SaveFilesPath, fileModel.FileDirName, razorModelData.table_name_lower, fileModel.FileName + fileModel.FileSuf);
                        }
                        else
                        {
                            fileModel.FileName = string.Format(fileModel.FileName, razorModelData.table_name_pascal);
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
