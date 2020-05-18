##  SkeFramework-创客.net框架

 - 目标：集成.net常用的类库

 - 目录说明：

 - 01-Src 框架集成代码
 - ---0101-DataBase：数据库通用层的代码封装
 - ---0102-WinForm：WinForm常用类库的封装
 - ----------AutoUpdates：Winform》检查更新库
 - ---0103-Cache：.NET常用缓存插件的封装
 - ----------SkeFramework.Cache.Redis》集成ServiceStack.Redis类库实现redis对String、List、HashTable的操作
 - ---0104-Core：.NET常用类库的封装
 - ----------SkeFramework.Core.ApiCommons》对.Net WebApi常用的库进行封装;
 - ----------SkeFramework.Core.Common》对.Net常用的基础库的封装;
 - ----------SkeFramework.Core.Encrypts》对.Net常用的加密库的封装;
 - ----------SkeFramework.Core.SnowFlake》集成获取分布式自增ID的操作
 - ----------SkeFramework.Core.IO》集成对流文件操作的帮助类
 - --------------------------ByteBuffer>>对字节数组的快捷操作，使用Pop和Push方法操作字节数组。
 - ---0105-Socket：通信类库的封装
 - ----------SkeFramework.NetSerialPort》串口通信类库的封装，包括超时重发、超时任务等功能。
 - ---0106-Tool：工具库
 - -------NetConnectionPool：.Net数据库链接应用池的实现
 - ---02-CSharpTool:框架集成工具
 - ----------------CodeBuilder：数据库通用层代码生成工具；
 - ----------------ConnectionPool：数据库链接应用池DEMO；
 - ----------------CreateXmlTools:检查更新库自动生成版本工具；
 - ---03-Business：框架对应的业务系统
 - ----------01-Entities         通用数据模型层
 - ----------------MicrosServices.Entities          数据实体与对应的枚举
 - ----------------MicrosServices.Entities.Common   公共基础数据
 - ----------------MicrosServices.Entities.Core     数据交互核心层
 - ----------02-DAL              数据访问层 
 - ----------------MicrosServices.DAL.DataAccess    数据交互核心层，支持SQLite、MySQL和SQLServer的数据库支持
 - ----------03-BLL              业务逻辑层
 - ----------------MicrosServices.BLL.Business      业务处理
 - ----------04-Helper           工具库
 - ----------05-SDK              第三方API的SDK
 - ----------06-UI               展示层UI
 - ----------07-Cache            缓存层
 - ----------08-MicrosServices   微服务-WebAPI接口
 - ----------
 - ---04-Test：框架对应测试DEMO
##  版本更新说明
- 2019-06-11：新增流文件类库,完成ByteBuffer操作帮助类
- 2019-05-14：新增Redis缓存的操作》集成ServiceStack.Redis类库
