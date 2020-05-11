<!--
 * @Descripttion: 
 * @version: 
 * @Author: Hawking
 * @Date: 2020-05-10 13:49:35
 * @LastEditors: Hawking
 * @LastEditTime: 2020-05-11 23:21:01
 -->
注意：解决方案.sln不要提交


这是一个通用的基本型的webapi程序
包括：用户管理、权限管理、菜单管理
前端使用：vue-elementui
后台使用：.net core 3.1 + sqlserver|mysql|mongodb + Dapper

Dev分支测试

开始使用：
删掉YourNameSln.sln文件，重新创建
先修改文件夹及文件名称：
services文件夹下的<YourNameSln>
YourWebApiName.ApiServices
YourWebApiName.ApiServices.csproj
打开VS2019创建空解决方案：解决方案名称写你修改的名称<YourNameSln>。路径填到/services，不要填<YourNameSln>这个文件夹
创建好解决方案之后，右键解决方案将<YourNameSln>文件夹下的所有项目添加进来，然后进行引用。
整个解决方案批量替换<YourWebApiName>为你的项目名称即可。重新生成

配置忽略文件：.gitignore
使用vs2019，右键解决方案，将解决方案添加到代码管理。之后就自动生成了.gitignore。好了现在就可以使用git命令操作了
