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


项目设计思想：
依赖倒置原则（DIP）设计
控制反转（Ioc）设计
依赖注入（DI）设计


计划采用：
API网关作为服务调用的总入口，同时负责了负载均衡、身份认证、熔断、限流等功能，Ocelot是基于.NetCore实现的一个主流API网关，对于以.Net技术为主的研发人员来说，更容易使用及修改。
IdentityServer也是基于.NetCore开发，是ABP官方推荐的身份认证框架。在这里，我们也同样以IdentityServer4作为身份认证中心。
ELK（Elasticsearch、Logstash、Kibana）是目前最常用的日志服务之一(不仅限于.Net技术栈）,实际使用中，我们通常会有直接写入lasticsearch、使用ELK+Filebeat、ELK+Kafuka等多种方式。
微服务间通讯方式有同步和异步两种方式，需要依据不同业务场景进行选择。其中同步通讯有多种实现方式，这里我使用了和AbpvNext微服务Demo相同的内外双网关方式，服务间调用通过内网关调用WebAPI接口方式实现。
AbpvNext提供了跨服务的事件总线机制，但是Asp.Net Boilerplate未对此进行封装和支持，需要我们额外进行开发。
    除此之外，我们还会用到其他一些组成微服务架构必须的技术组件：
服务注册和发现采用Consul
配置中心采用Apollo
应用性能监测采用Skywalking