##  SkeFramework-创客.net框架

 - 目标：集成.net常用的类库

 - 目录说明：

 - 01-Src 框架集成代码
 - ---DataBase：数据库通用层的代码封装
 - ---WinForm：WinForm常用类库的封装
 - ----------AutoUpdates：Winform》检查更新库
 - ---Cache：.NET常用缓存插件的封装
 - ----------SkeFramework.Cache.Redis》集成ServiceStack.Redis类库实现redis对String、List、HashTable的操作
 - ---Tool：工具库
 - -------NetConnectionPool：.Net数据库链接应用池的实现
 - ---02-CSharpTool:框架集成工具
 - ----------------CodeBuilder：数据库通用层代码生成工具；
 - ----------------ConnectionPool：数据库链接应用池DEMO；
 - ----------------CreateXmlTools:检查更新库自动生成版本工具；
 - ---04-Test：框架对应测试DEMO
##  版本更新说明
 - 2019-05-14：新增Redis缓存的操作》集成ServiceStack.Redis类库