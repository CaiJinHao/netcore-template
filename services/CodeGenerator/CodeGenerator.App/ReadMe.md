appsettings.json 使用说明

    var table = Model;//表对象
    var table_name = table.table_name;//表名
    var columns = table.columns;//列集合

    var name_space = table.name_space;//命名空间
    var table_name_pascal = table.table_name_pascal;//pascal大驼峰格式表名
    var table_name_camel = table.table_name_camel;//camel 小驼峰格式表名
    var primary_key_name = table.primary_key_name;//主键名称暂时只支持MySql
    var primary_key_data_type = table.primary_key_data_type;//主键数据类型

    var table_name_lower = table.table_name_lower;//全小写去除_字符的表名、路由名
    var api_version = "1";//api 版本

    NamingFormat命名格式：
    /// <summary>
    /// 如：Sys_User,Su_UserName
    /// appsettings中要命名为此格式
    /// </summary>
    大驼峰Pascal_符号分割 = 1,
    /// <summary>
    /// 如：sys_user,su_user_name
    /// appsettings中要命名为此格式
    /// </summary>
    small_camel_符号分割 = 2,

FileName 的后缀命名 需要跟NamingFormat格式一致

{
  "Template": {
    "SaveFilesPath": "D:\\CodematicDemo",//要保存的电脑路径
    "NameSpace": "YourWebApiName",//生成文件使用的命名空间 存在变量 name_space
    "NamingFormat": 1,// 命名格式
    "TemplateFiles": [ //模板文件集合
      {
        "FileName": "{0}Model",//文件名、类名
        "FileDirName": "DbModels",//文件存放的文件夹
        "TemplateFile": "Resource/Template/DbModelsTemplate.cshtml"//razor模板文件
      },
      {
        "FileName": "{0}RequestModel",
        "FileDirName": "RequestModels",
        "TemplateFile": "Resource/Template/RequestModelsTemplate.cshtml"
      },
      {
        "FileName": "{0}ResponeModel",
        "FileDirName": "ResponeModels",
        "TemplateFile": "Resource/Template/ResponeModelsTemplate.cshtml"
      },
      {
        "FileName": "I{0}Repository",
        "FileDirName": "IDbRepository",
        "TemplateFile": "Resource/Template/IDbRepositorysTemplate.cshtml"
      },
      {
        "FileName": "{0}Repository",
        "FileDirName": "DbRepository",
        "TemplateFile": "Resource/Template/MongoDb/DbRepositorysTemplate.cshtml"
      },
      {
        "FileName": "I{0}service",
        "FileDirName": "IDbServices",
        "TemplateFile": "Resource/Template/IDbServicesTemplate.cshtml"
      },
      {
        "FileName": "{0}Service",
        "FileDirName": "DbServices",
        "TemplateFile": "Resource/Template/DbServicesTemplate.cshtml"
      },
      {
        "FileName": "{0}Controller",
        "FileDirName": "ApiController",
        "TemplateFile": "Resource/Template/ApiTemplate.cshtml"
      }
    ]
  },
  "DbConnection": {//数据库连接字符串暂时只支持Mysql 主键判断
    "MySqlConnection": "Server=39.107.111.128;DataBase=cloudhijack;Uid=root;Pwd=CaiJinHao@940421.;pooling=true;port=3306;sslmode=none;charset=utf8",
    "DbName": "warehouse_box"//数据库名称 
  }
}
