-- MySQL dump 10.13  Distrib 8.0.12, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: skecloud
-- ------------------------------------------------------
-- Server version	8.0.12

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bs_dictionary`
--

DROP TABLE IF EXISTS `bs_dictionary`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bs_dictionary` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `DicNo` bigint(20) DEFAULT NULL COMMENT '字典码值',
  `DicType` varchar(100) DEFAULT NULL COMMENT '字典类型',
  `DicKey` varchar(100) DEFAULT NULL COMMENT '键',
  `DicValue` varchar(200) DEFAULT NULL COMMENT '值',
  `Descriptions` varchar(512) DEFAULT NULL COMMENT '描述',
  `PlatformNo` bigint(20) DEFAULT NULL COMMENT '平台编号',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateUser` varchar(10) DEFAULT NULL COMMENT '更新人',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='字典表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bs_dictionary`
--

LOCK TABLES `bs_dictionary` WRITE;
/*!40000 ALTER TABLE `bs_dictionary` DISABLE KEYS */;
INSERT INTO `bs_dictionary` VALUES (3,68505856,'permission.system','ip.interceptor.exclude.ip','192.168.104.60,192.168.76.237,127.0.0.1,','IP白名单',88073472,1,'999999','2020-08-26 15:17:43','999999','2020-08-27 16:18:43');
/*!40000 ALTER TABLE `bs_dictionary` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pd_project`
--

DROP TABLE IF EXISTS `pd_project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `pd_project` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `ProjectNo` bigint(20) DEFAULT NULL COMMENT '编码',
  `Name` varchar(120) DEFAULT NULL COMMENT '名称',
  `VersionType` varchar(50) DEFAULT NULL COMMENT '版本控制类型[Git/SVN]',
  `VersionUrl` varchar(512) DEFAULT NULL COMMENT '版本地址',
  `GitBranch` varchar(50) DEFAULT NULL COMMENT '分支',
  `GitBinPath` varchar(200) DEFAULT NULL COMMENT 'Git程序路径',
  `SourcePath` varchar(200) DEFAULT NULL COMMENT '源代码地址',
  `MSBuildPath` varchar(200) DEFAULT NULL COMMENT '打包程序路径',
  `ProjectFile` varchar(200) DEFAULT NULL COMMENT '项目文件',
  `notifyEmails` varchar(512) DEFAULT NULL COMMENT '通知邮箱列表',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='项目表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pd_project`
--

LOCK TABLES `pd_project` WRITE;
/*!40000 ALTER TABLE `pd_project` DISABLE KEYS */;
INSERT INTO `pd_project` VALUES (1,76905984,'发布系统','Github','https://gitee.com/SkeCloud/SkeFramework','dev','D:\\Program Files\\Git\\cmd\\git.exe','E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework','C:\\Windows\\System32\\cmd.exe','\\03-Business\\MicrosServices.API.PublishDeploy/Publish.bat','502525164@qq.com,','999999','0001-01-01 00:00:00','2020-08-12 08:10:17'),(4,83009536,'用户系统','Github','https://gitee.com/SkeCloud/SkeFramework','dev','D:\\Program Files\\Git\\cmd\\git.exe','E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework','C:\\Windows\\System32\\cmd.exe','\\03-Business\\MicrosServices.API.UserCenter/Publish.bat','502525164@qq.com,','999999','0001-01-01 00:00:00','2020-08-11 11:36:48'),(5,65614592,'权限系统','Github','https://gitee.com/SkeCloud/SkeFramework','dev','D:\\Program Files\\Git\\cmd\\git.exe','E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework','C:\\Windows\\System32\\cmd.exe','\\03-Business\\MicrosServices.API.PermissionSystem/Publish.bat','502525164@qq.com,','999999','0001-01-01 00:00:00','2020-08-12 08:10:07'),(7,11924992,'基础服务系统','Github','https://gitee.com/SkeCloud/SkeFramework','dev','D:\\Program Files\\Git\\cmd\\git.exe','E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework','C:\\Windows\\System32\\cmd.exe','\\03-Business\\MicrosServices.API.AdminServer/Publish.bat','502525164@qq.com,','999999','2020-08-24 17:37:19','0001-01-01 00:00:00');
/*!40000 ALTER TABLE `pd_project` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pd_publish`
--

DROP TABLE IF EXISTS `pd_publish`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `pd_publish` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `ProjectNo` bigint(20) DEFAULT NULL COMMENT '项目编码',
  `ServerNo` bigint(20) DEFAULT NULL COMMENT '服务器编号',
  `PublishCmd` varchar(512) DEFAULT NULL COMMENT '发布命令',
  `PublishProfile` varchar(200) DEFAULT NULL COMMENT '发布配置文件',
  `WebProjectOutputDir` varchar(512) DEFAULT NULL COMMENT '项目输出目录',
  `OutputPath` varchar(100) DEFAULT NULL COMMENT '输出路径',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='项目发布信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pd_publish`
--

LOCK TABLES `pd_publish` WRITE;
/*!40000 ALTER TABLE `pd_publish` DISABLE KEYS */;
/*!40000 ALTER TABLE `pd_publish` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pd_server`
--

DROP TABLE IF EXISTS `pd_server`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `pd_server` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `ServerNo` bigint(20) DEFAULT NULL COMMENT '编码',
  `Name` varchar(120) DEFAULT NULL COMMENT '名称',
  `IP` varchar(50) DEFAULT NULL COMMENT 'IP',
  `Port` int(11) DEFAULT NULL COMMENT '端口',
  `Description` varchar(512) DEFAULT NULL COMMENT '描述',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='服务器表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pd_server`
--

LOCK TABLES `pd_server` WRITE;
/*!40000 ALTER TABLE `pd_server` DISABLE KEYS */;
INSERT INTO `pd_server` VALUES (2,19296256,'IIS服务器','192.168.1.103',5002,'本地服务器',NULL,'0001-01-01 00:00:00','2020-04-29 23:13:46');
/*!40000 ALTER TABLE `pd_server` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_management`
--

DROP TABLE IF EXISTS `ps_management`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_management` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `ManagementNo` bigint(20) DEFAULT NULL,
  `ParentNo` bigint(20) DEFAULT NULL COMMENT '父节点',
  `TreeLevelNo` varchar(500) DEFAULT NULL COMMENT '树节点编号',
  `Name` varchar(120) DEFAULT NULL COMMENT '名称',
  `Description` varchar(512) DEFAULT NULL,
  `Value` varchar(120) DEFAULT NULL COMMENT '权限值',
  `Type` int(11) DEFAULT NULL COMMENT '权限类型【0默认权限，1普通权限，2菜单权限】',
  `Sort` int(11) DEFAULT NULL COMMENT '排序',
  `PlatformNo` int(11) DEFAULT NULL COMMENT '平台编号',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8 COMMENT='权限表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_management`
--

LOCK TABLES `ps_management` WRITE;
/*!40000 ALTER TABLE `ps_management` DISABLE KEYS */;
INSERT INTO `ps_management` VALUES (4,36115712,-1,'_-1','框架菜单',NULL,'0',2,1,88073472,1,'999999','2019-12-16 22:25:27','2019-12-26 22:34:13'),(5,84787968,-1,'_-1','权限系统菜单',NULL,'0',2,11,88073472,1,'999999','2020-01-01 23:11:21','2020-02-02 10:36:18'),(6,52697600,84787968,'_-1_84787968','菜单更新权限',NULL,'menu.update',1,1,88073472,1,'999999','2020-01-04 00:23:30','2020-08-11 11:42:40'),(7,45914624,84787968,'_-1_84787968','菜单新增权限',NULL,'menu.add',1,2,88073472,1,'999999','2020-02-06 16:48:30','2020-08-11 11:43:05'),(8,79318016,84787968,'_-1_84787968','菜单删除权限',NULL,'menu.delete',1,3,88073472,1,'999999','2020-02-07 15:30:48','2020-08-11 11:43:13'),(9,89873408,84787968,'_-1_84787968','菜单权限授权',NULL,'menu.management.assign',1,4,88073472,1,'999999','2020-02-07 15:33:45','2020-08-11 11:43:31'),(10,26164992,84787968,'_-1_84787968','权限新增权限',NULL,'management.add',1,1,88073472,1,'999999','2020-02-07 15:37:35','2020-08-11 11:44:57'),(11,78745088,84787968,'_-1_84787968','权限更新权限',NULL,'management.update',1,2,88073472,1,'999999','2020-02-07 15:57:18','2020-08-11 11:45:51'),(12,4728064,84787968,'_-1_84787968','权限删除权限',NULL,'management.delete',1,3,88073472,1,'999999','2020-02-07 15:59:23','2020-08-11 11:46:23'),(13,33165056,84787968,'_-1_84787968','权限菜单授权',NULL,'management.menu.assign',1,4,88073472,1,'999999','2020-02-07 15:59:53','2020-08-11 11:46:43'),(14,28688896,84787968,'_-1_84787968','角色新增权限',NULL,'role.add',1,1,88073472,1,'999999','2020-02-07 17:11:54','2020-08-11 11:47:01'),(15,80731904,84787968,'_-1_84787968','角色更新权限',NULL,'role.update',1,2,88073472,1,'999999','2020-02-07 17:13:09','2020-08-11 11:47:12'),(16,17200128,84787968,'_-1_84787968','角色删除权限',NULL,'role.delete',1,3,88073472,1,'999999','2020-02-07 20:17:09','2020-08-11 11:47:26'),(17,75594240,84787968,'_-1_84787968','角色权限分配',NULL,'role.management.assign',1,4,88073472,1,'999999','2020-02-07 20:18:48','2020-08-11 11:47:42'),(18,30197248,84787968,'_-1_84787968','机构新增权限',NULL,'org.add',1,1,88073472,1,'999999','2020-02-07 20:22:23','2020-08-11 11:47:55'),(19,97827840,84787968,'_-1_84787968','机构更新权限',NULL,'org.update',1,2,88073472,1,'999999','2020-02-07 20:22:55','2020-08-11 11:48:09'),(20,79525632,84787968,'_-1_84787968','机构删除权限',NULL,'org.delete',1,3,88073472,1,'999999','2020-02-07 20:23:31','2020-08-11 11:48:20'),(21,23708928,84787968,'_-1_84787968','机构角色授权',NULL,'org.role.assign',1,4,88073472,1,'999999','2020-02-07 20:24:07','2020-08-11 11:48:33'),(22,36370432,36115712,'_-1_36115712','平台新增权限',NULL,'platform.add',1,1,88073472,1,'999999','2020-02-08 10:31:56','2020-08-11 11:40:16'),(23,4016128,36115712,'_-1_36115712','平台更新管理',NULL,'platform.update',1,2,88073472,1,'999999','2020-02-08 10:32:19','2020-08-11 11:42:05'),(24,99909888,36115712,'_-1_36115712','平台删除权限',NULL,'platform.delete',1,3,88073472,1,'999999','2020-02-08 10:32:41','2020-08-11 11:41:07'),(25,72390144,72114688,'_-1_72114688','用户新增权限',NULL,'userAdd',1,2,88073472,1,'999999','2020-03-31 20:01:26','2020-04-22 21:34:03'),(26,9599744,-1,'_-1','发布系统菜单','发布系统菜单','PublishDeployMenu',2,2,88073472,1,'999999','2020-04-12 16:57:19','0001-01-01 00:00:00'),(27,72114688,-1,'_-1','用户系统菜单','usercentermenu','usercentermenu',2,4,88073472,1,'999999','2020-04-14 22:30:57','0001-01-01 00:00:00'),(28,56150528,72114688,'_-1_72114688','角色授权',NULL,'user.role.assign',1,1,88073472,1,'999999','2020-08-11 11:59:09','0001-01-01 00:00:00'),(29,53141248,72114688,'_-1_72114688','机构授权',NULL,'user.org.assign',1,3,88073472,1,'999999','2020-08-11 11:59:42','0001-01-01 00:00:00'),(30,43603456,9599744,'_-1_9599744','新增项目权限',NULL,'project.add',1,1,88073472,1,'999999','2020-08-11 14:36:01','0001-01-01 00:00:00'),(31,71882752,9599744,'_-1_9599744','更新项目权限',NULL,'project.update',1,2,88073472,1,'999999','2020-08-11 14:36:27','0001-01-01 00:00:00'),(32,93765632,9599744,'_-1_9599744','删除项目权限',NULL,'project.delete',1,3,88073472,1,'999999','2020-08-11 14:36:58','0001-01-01 00:00:00'),(33,20565248,9599744,'_-1_9599744','发布项目权限',NULL,'project.publish',1,4,88073472,1,'999999','2020-08-11 14:37:30','2020-08-11 14:37:41'),(34,72967168,36115712,'_-1_36115712','字典新增权限',NULL,'dictionary.add',1,4,88073472,1,'999999','2020-08-25 08:01:03','0001-01-01 00:00:00'),(35,8310784,36115712,'_-1_36115712','字典修改权限',NULL,'dictionary.update',1,5,88073472,1,'999999','2020-08-25 08:01:34','0001-01-01 00:00:00'),(36,53483264,36115712,'_-1_36115712','字典删除权限',NULL,'dictionary.delete',1,6,88073472,0,'999999','2020-08-25 08:01:57','2020-08-25 08:10:00'),(37,65967872,-1,'_-1','实时系统菜单',NULL,'menu.realtime',2,5,88073472,1,'999999','2020-08-31 09:17:56','0001-01-01 00:00:00');
/*!40000 ALTER TABLE `ps_management` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_management_roles`
--

DROP TABLE IF EXISTS `ps_management_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_management_roles` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `RolesNo` bigint(20) DEFAULT NULL COMMENT '角色编号',
  `ManagementNo` bigint(20) DEFAULT NULL COMMENT '权限编号',
  `ManagementValue` varchar(120) DEFAULT NULL COMMENT '权限值',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8 COMMENT='用户角色关系表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_management_roles`
--

LOCK TABLES `ps_management_roles` WRITE;
/*!40000 ALTER TABLE `ps_management_roles` DISABLE KEYS */;
INSERT INTO `ps_management_roles` VALUES (10,53635840,84787968,'0','999999','2020-02-02 10:37:06','2020-02-02 10:37:06'),(11,53635840,52697600,'0','999999','2020-02-02 10:37:06','2020-02-02 10:37:06'),(13,53635840,33165056,'0','999999','2020-03-31 20:38:19','2020-03-31 20:38:19'),(23,99578624,9599744,'PublishDeployMenu','999999','2020-08-11 14:44:33','2020-08-11 14:44:33'),(24,49830144,36115712,'0','999999','2020-08-31 09:21:28','2020-08-31 09:21:28'),(25,49830144,84787968,'0','999999','2020-08-31 09:21:28','2020-08-31 09:21:28'),(26,49830144,9599744,'PublishDeployMenu','999999','2020-08-31 09:21:28','2020-08-31 09:21:28'),(27,49830144,72114688,'usercentermenu','999999','2020-08-31 09:21:28','2020-08-31 09:21:28'),(28,49830144,65967872,'menu.realtime','999999','2020-08-31 09:21:28','2020-08-31 09:21:28');
/*!40000 ALTER TABLE `ps_management_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_menu`
--

DROP TABLE IF EXISTS `ps_menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_menu` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `MenuNo` bigint(20) DEFAULT NULL COMMENT '菜单编号',
  `ParentNo` bigint(20) DEFAULT NULL COMMENT '父节点',
  `TreeLevelNo` varchar(500) DEFAULT NULL COMMENT '树节点编号',
  `Name` varchar(120) DEFAULT NULL COMMENT '名称',
  `Value` varchar(120) DEFAULT NULL COMMENT '权限值',
  `icon` varchar(120) DEFAULT NULL COMMENT '图标',
  `url` varchar(120) DEFAULT NULL COMMENT '跳转地址',
  `Sort` int(11) DEFAULT NULL COMMENT '排序',
  `PlatformNo` bigint(20) DEFAULT NULL,
  `Enabled` int(11) DEFAULT NULL,
  `InputUser` varchar(120) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 COMMENT='菜单表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_menu`
--

LOCK TABLES `ps_menu` WRITE;
/*!40000 ALTER TABLE `ps_menu` DISABLE KEYS */;
INSERT INTO `ps_menu` VALUES (1,99999999,15325696,'_-1_15325696','菜单管理','menu','1','/Menu/MenuList',1,88073472,1,'999999','2019-11-22 00:00:00','2020-02-03 20:31:51'),(2,99479040,15325696,'_-1_15325696','权限管理','management','1','/management/managementlist',2,88073472,1,'999999','2019-11-28 23:06:33','2020-02-03 21:30:24'),(3,50878208,41653248,'_41653248','平台管理','platform','1','/platform/platformlist',1,88073472,1,'999999','2019-11-28 23:09:15','2020-08-24 14:22:38'),(4,76524032,15325696,'_-1_15325696','角色管理','role','1','/roles/rolesList',4,88073472,1,'999999','2019-11-28 23:09:41','2020-02-03 21:30:38'),(5,21080064,85645568,'_-1_85645568','用户管理','user','1','/User/UserList',5,88073472,1,'999999','2020-01-11 14:45:33','2020-02-03 21:42:56'),(6,99464448,15325696,'_-1_15325696','机构管理','organization','1','/Organization/OrganizationList',6,88073472,1,'999999','2020-02-02 10:53:44','2020-02-03 21:30:53'),(7,15325696,-1,'_-1','权限系统','permission','1','#',1,88073472,1,'999999','2020-02-03 20:31:17','2020-02-03 21:57:54'),(8,85645568,-1,'_-1','用户系统','UserSystem','1','#',1,88073472,1,'999999','2020-02-03 21:42:36','2020-08-19 14:40:49'),(9,41653248,-1,'_-1','框架系统','SkeCloud','1','#',0,88073472,1,'999999','2020-02-03 21:57:02','0001-01-01 00:00:00'),(10,58688512,-1,'_-1','发布系统','PublishDeploy',NULL,'#',4,88073472,1,'999999','2020-04-12 16:30:03','0001-01-01 00:00:00'),(11,68522752,58688512,'_-1_58688512','服务器','Server',NULL,'/Server/ServerList',0,88073472,1,'999999','2020-04-12 16:32:17','0001-01-01 00:00:00'),(12,4764416,58688512,'_58688512','项目管理','ProjectManager',NULL,'/Project/ProjectList',2,88073472,1,'999999','2020-04-29 21:28:35','0001-01-01 00:00:00'),(13,67317248,58688512,'_58688512','发布日志','publish.log',NULL,'/Log/PublishLogList',3,88073472,1,'999999','2020-08-18 12:05:32','0001-01-01 00:00:00'),(14,18916352,85645568,'_85645568','登录日志','login.log',NULL,'/Log/LoginLogList',2,88073472,1,'999999','2020-08-19 14:01:30','2020-08-19 14:06:33'),(15,72976384,41653248,'_41653248','字典管理','Dictionary',NULL,'/Dictionary/DictionaryList',2,88073472,1,'999999','2020-08-24 13:47:38','0001-01-01 00:00:00'),(16,80094720,-1,'_-1','实时系统','realtime',NULL,'#',5,88073472,1,'999999','2020-08-31 09:15:08','0001-01-01 00:00:00'),(17,91669760,80094720,'_80094720','推送设置','realtime.pushconfig',NULL,'/PushConfig/PushConfigList',1,88073472,1,'999999','2020-08-31 09:16:06','2020-08-31 10:05:43'),(18,24135168,80094720,'_80094720','推送记录','realtime.push.message',NULL,'/Message/MessageList',2,88073472,1,'999999','2020-08-31 09:16:57','2020-08-31 10:05:54');
/*!40000 ALTER TABLE `ps_menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_menu_management`
--

DROP TABLE IF EXISTS `ps_menu_management`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_menu_management` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `Name` varchar(120) DEFAULT NULL COMMENT '角色名称',
  `Description` varchar(512) DEFAULT NULL COMMENT '描述',
  `MenuNo` bigint(20) DEFAULT NULL COMMENT '权限值',
  `ManagementNo` bigint(20) DEFAULT NULL,
  `Type` int(11) DEFAULT NULL COMMENT '关系类型',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=164 DEFAULT CHARSET=utf8 COMMENT='菜单权限关系表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_menu_management`
--

LOCK TABLES `ps_menu_management` WRITE;
/*!40000 ALTER TABLE `ps_menu_management` DISABLE KEYS */;
INSERT INTO `ps_menu_management` VALUES (73,NULL,NULL,99479040,26164992,1,'999999','2020-02-07 16:47:07','2020-02-07 16:47:07'),(74,NULL,NULL,99479040,78745088,1,'999999','2020-02-07 16:47:07','2020-02-07 16:47:07'),(75,NULL,NULL,99479040,4728064,1,'999999','2020-02-07 16:47:07','2020-02-07 16:47:07'),(76,NULL,NULL,99479040,33165056,1,'999999','2020-02-07 16:47:07','2020-02-07 16:47:07'),(80,NULL,NULL,76524032,28688896,1,'999999','2020-02-07 20:19:07','2020-02-07 20:19:07'),(81,NULL,NULL,76524032,80731904,1,'999999','2020-02-07 20:19:07','2020-02-07 20:19:07'),(82,NULL,NULL,76524032,17200128,1,'999999','2020-02-07 20:19:07','2020-02-07 20:19:07'),(83,NULL,NULL,76524032,75594240,1,'999999','2020-02-07 20:19:07','2020-02-07 20:19:07'),(84,NULL,NULL,99464448,30197248,1,'999999','2020-02-07 20:25:33','2020-02-07 20:25:33'),(85,NULL,NULL,99464448,97827840,1,'999999','2020-02-07 20:25:33','2020-02-07 20:25:33'),(86,NULL,NULL,99464448,79525632,1,'999999','2020-02-07 20:25:33','2020-02-07 20:25:33'),(87,NULL,NULL,99464448,23708928,1,'999999','2020-02-07 20:25:33','2020-02-07 20:25:33'),(88,NULL,NULL,50878208,36370432,1,'999999','2020-02-08 10:33:13','2020-02-08 10:33:13'),(89,NULL,NULL,50878208,4016128,1,'999999','2020-02-08 10:33:13','2020-02-08 10:33:13'),(90,NULL,NULL,50878208,99909888,1,'999999','2020-02-08 10:33:13','2020-02-08 10:33:13'),(91,NULL,NULL,99999999,52697600,1,'999999','2020-02-08 10:38:44','2020-02-08 10:38:44'),(92,NULL,NULL,99999999,45914624,1,'999999','2020-02-08 10:38:44','2020-02-08 10:38:44'),(93,NULL,NULL,99999999,79318016,1,'999999','2020-02-08 10:38:44','2020-02-08 10:38:44'),(94,NULL,NULL,99999999,89873408,1,'999999','2020-02-08 10:38:44','2020-02-08 10:38:44'),(121,NULL,NULL,99999999,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(122,NULL,NULL,99479040,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(123,NULL,NULL,76524032,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(124,NULL,NULL,99464448,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(125,NULL,NULL,15325696,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(129,NULL,NULL,21080064,72390144,1,'999999','2020-08-11 12:00:36','2020-08-11 12:00:36'),(130,NULL,NULL,21080064,56150528,1,'999999','2020-08-11 12:00:36','2020-08-11 12:00:36'),(131,NULL,NULL,21080064,53141248,1,'999999','2020-08-11 12:00:36','2020-08-11 12:00:36'),(139,NULL,NULL,4764416,43603456,1,'999999','2020-08-11 14:46:48','2020-08-11 14:46:48'),(140,NULL,NULL,4764416,71882752,1,'999999','2020-08-11 14:46:48','2020-08-11 14:46:48'),(141,NULL,NULL,4764416,93765632,1,'999999','2020-08-11 14:46:48','2020-08-11 14:46:48'),(142,NULL,NULL,4764416,20565248,1,'999999','2020-08-11 14:46:48','2020-08-11 14:46:48'),(143,NULL,NULL,58688512,9599744,2,'999999','2020-08-18 12:06:06','2020-08-18 12:06:06'),(144,NULL,NULL,68522752,9599744,2,'999999','2020-08-18 12:06:06','2020-08-18 12:06:06'),(145,NULL,NULL,4764416,9599744,2,'999999','2020-08-18 12:06:06','2020-08-18 12:06:06'),(146,NULL,NULL,67317248,9599744,2,'999999','2020-08-18 12:06:06','2020-08-18 12:06:06'),(147,NULL,NULL,21080064,72114688,2,'999999','2020-08-19 14:07:34','2020-08-19 14:07:34'),(148,NULL,NULL,85645568,72114688,2,'999999','2020-08-19 14:07:34','2020-08-19 14:07:34'),(149,NULL,NULL,18916352,72114688,2,'999999','2020-08-19 14:07:34','2020-08-19 14:07:34'),(150,NULL,NULL,50878208,36115712,2,'999999','2020-08-24 13:50:30','2020-08-24 13:50:30'),(151,NULL,NULL,41653248,36115712,2,'999999','2020-08-24 13:50:30','2020-08-24 13:50:30'),(152,NULL,NULL,72976384,36115712,2,'999999','2020-08-24 13:50:30','2020-08-24 13:50:30'),(158,NULL,NULL,72976384,72967168,1,'999999','2020-08-25 08:09:02','2020-08-25 08:09:02'),(159,NULL,NULL,72976384,8310784,1,'999999','2020-08-25 08:09:02','2020-08-25 08:09:02'),(160,NULL,NULL,72976384,53483264,1,'999999','2020-08-25 08:09:02','2020-08-25 08:09:02'),(161,NULL,NULL,80094720,65967872,2,'999999','2020-08-31 09:22:47','2020-08-31 09:22:47'),(162,NULL,NULL,91669760,65967872,2,'999999','2020-08-31 09:22:47','2020-08-31 09:22:47'),(163,NULL,NULL,24135168,65967872,2,'999999','2020-08-31 09:22:48','2020-08-31 09:22:48');
/*!40000 ALTER TABLE `ps_menu_management` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_org_roles`
--

DROP TABLE IF EXISTS `ps_org_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_org_roles` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `RolesNo` bigint(20) DEFAULT NULL COMMENT '角色编号',
  `OrgNo` bigint(20) DEFAULT NULL COMMENT '组织编号',
  `ManagementValue` bigint(20) DEFAULT NULL COMMENT '权限值',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='组织角色关系表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_org_roles`
--

LOCK TABLES `ps_org_roles` WRITE;
/*!40000 ALTER TABLE `ps_org_roles` DISABLE KEYS */;
INSERT INTO `ps_org_roles` VALUES (4,49830144,53893376,0,NULL,'2020-02-04 22:44:53','2020-02-04 22:44:53');
/*!40000 ALTER TABLE `ps_org_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_organization`
--

DROP TABLE IF EXISTS `ps_organization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_organization` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `OrgNo` bigint(20) DEFAULT NULL COMMENT '组织编号',
  `ParentNo` bigint(20) DEFAULT NULL COMMENT '父节点',
  `TreeLevelNo` varchar(500) DEFAULT NULL COMMENT '树节点编号',
  `Name` varchar(120) DEFAULT NULL COMMENT '组织名称',
  `Description` varchar(512) DEFAULT NULL COMMENT '描述',
  `Category` varchar(50) DEFAULT NULL COMMENT '分类【集团、公司、部门、工作组】',
  `PlatformNo` bigint(20) DEFAULT NULL COMMENT '平台编号',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='权限组织机构';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_organization`
--

LOCK TABLES `ps_organization` WRITE;
/*!40000 ALTER TABLE `ps_organization` DISABLE KEYS */;
INSERT INTO `ps_organization` VALUES (1,53893376,-1,'_-1','五邑创客','12','0',88073472,1,'999999','2020-02-02 15:44:33','2020-02-03 14:42:20'),(2,25279488,53893376,'_-1_53893376','五邑云','专注科技类','1',88073472,1,'999999','2020-07-29 13:39:02','0001-01-01 00:00:00'),(3,69129984,25279488,'_-1_53893376_25279488','软件产品部',NULL,'2',88073472,1,'999999','2020-07-29 13:39:36','0001-01-01 00:00:00'),(4,2113792,25279488,'_-1_53893376_25279488','嵌入式产品部',NULL,'2',88073472,1,'999999','2020-07-29 13:40:09','0001-01-01 00:00:00');
/*!40000 ALTER TABLE `ps_organization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_platform`
--

DROP TABLE IF EXISTS `ps_platform`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_platform` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `PlatformNo` bigint(20) DEFAULT NULL COMMENT '平台编号',
  `ParentNo` bigint(20) DEFAULT NULL,
  `TreeLevelNo` varchar(500) DEFAULT NULL,
  `Name` varchar(120) DEFAULT NULL COMMENT '平台名称',
  `Value` varchar(120) DEFAULT NULL COMMENT '平台值',
  `DefaultUserName` varchar(120) DEFAULT NULL COMMENT '超级管理员名称',
  `DefaultUserNo` varchar(120) DEFAULT NULL COMMENT '超级管理员账号',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='平台表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_platform`
--

LOCK TABLES `ps_platform` WRITE;
/*!40000 ALTER TABLE `ps_platform` DISABLE KEYS */;
INSERT INTO `ps_platform` VALUES (1,88073472,-1,'-1_88073472','管理后台框架','SkeCloud','admin','999999',NULL,'0001-01-01 00:00:00','2020-08-07 18:14:27');
/*!40000 ALTER TABLE `ps_platform` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_roles`
--

DROP TABLE IF EXISTS `ps_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_roles` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `RolesNo` bigint(20) DEFAULT NULL COMMENT '角色编号',
  `ParentNo` bigint(20) DEFAULT NULL COMMENT '父节点',
  `TreeLevelNo` varchar(500) DEFAULT NULL COMMENT '树节点编号',
  `Name` varchar(120) DEFAULT NULL COMMENT '角色名称',
  `Description` varchar(512) DEFAULT NULL COMMENT '描述',
  `ManagementValue` varchar(120) DEFAULT NULL COMMENT '权限值',
  `PlatformNo` int(11) DEFAULT NULL COMMENT '平台编号',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(45) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='角色表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_roles`
--

LOCK TABLES `ps_roles` WRITE;
/*!40000 ALTER TABLE `ps_roles` DISABLE KEYS */;
INSERT INTO `ps_roles` VALUES (5,49830144,-1,'_-1','框架管理员','1','skecloudadmin',88073472,1,'999999','2019-12-30 20:12:57','2020-02-05 21:36:54'),(6,53635840,-1,'_-1','权限管理员','权限系统','permissionAdmin',88073472,1,'999999','2020-02-02 10:32:38','2020-04-12 16:56:05'),(7,99578624,-1,'_-1','发布管理员',NULL,'publish.admin',88073472,1,'999999','2020-08-11 14:44:11','2020-08-24 17:39:22');
/*!40000 ALTER TABLE `ps_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_user_org`
--

DROP TABLE IF EXISTS `ps_user_org`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_user_org` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `OrgNo` bigint(20) DEFAULT NULL COMMENT '组织编号',
  `UserNo` bigint(20) DEFAULT NULL COMMENT '用户编号',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='用户组织关系表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_user_org`
--

LOCK TABLES `ps_user_org` WRITE;
/*!40000 ALTER TABLE `ps_user_org` DISABLE KEYS */;
INSERT INTO `ps_user_org` VALUES (1,53893376,999999,NULL,'2020-02-05 15:26:18','2020-02-05 15:26:18');
/*!40000 ALTER TABLE `ps_user_org` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_user_roles`
--

DROP TABLE IF EXISTS `ps_user_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ps_user_roles` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `RolesNo` bigint(20) DEFAULT NULL COMMENT '角色编号',
  `UserNo` bigint(20) DEFAULT NULL COMMENT '用户编号',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='用户角色关系表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_user_roles`
--

LOCK TABLES `ps_user_roles` WRITE;
/*!40000 ALTER TABLE `ps_user_roles` DISABLE KEYS */;
INSERT INTO `ps_user_roles` VALUES (1,49830144,999999,'999999','2020-01-19 13:07:00','2020-01-19 13:07:00'),(5,99578624,999996,'999999','2020-08-11 14:44:52','2020-08-11 14:44:52');
/*!40000 ALTER TABLE `ps_user_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rt_message`
--

DROP TABLE IF EXISTS `rt_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `rt_message` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `AppId` varchar(100) DEFAULT NULL COMMENT '应用ID',
  `Message` varchar(512) DEFAULT NULL COMMENT '消息',
  `SendUserId` varchar(100) DEFAULT NULL COMMENT '发送用户',
  `UserId` varchar(100) DEFAULT NULL COMMENT '接受用户',
  `Status` int(11) DEFAULT NULL COMMENT '运行状态',
  `InputTime` datetime DEFAULT NULL COMMENT '创建时间',
  `HandleTime` datetime DEFAULT NULL COMMENT '处理时间',
  `HandleResult` varchar(512) DEFAULT NULL COMMENT '处理结果',
  `AvailTime` datetime DEFAULT NULL COMMENT '到达时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='实时推送记录';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rt_message`
--

LOCK TABLES `rt_message` WRITE;
/*!40000 ALTER TABLE `rt_message` DISABLE KEYS */;
INSERT INTO `rt_message` VALUES (1,'netske','123','999999','999999',1,'2020-09-14 00:00:00','2020-09-14 00:00:00','receipt_offline','2020-09-21 16:51:11');
/*!40000 ALTER TABLE `rt_message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rt_pushconfig`
--

DROP TABLE IF EXISTS `rt_pushconfig`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `rt_pushconfig` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `PushNo` bigint(20) DEFAULT NULL COMMENT '推送服务编号',
  `PushType` varchar(100) DEFAULT NULL COMMENT '推送类型',
  `PushPort` varchar(100) DEFAULT NULL COMMENT '推送端口',
  `AppId` varchar(100) DEFAULT NULL COMMENT '应用ID',
  `Descriptions` varchar(512) DEFAULT NULL COMMENT '描述',
  `ExtraProps` varchar(1000) DEFAULT NULL COMMENT '扩展信息',
  `Status` int(11) DEFAULT NULL COMMENT '运行状态',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateUser` varchar(10) DEFAULT NULL COMMENT '更新人',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='推送服务端配置';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rt_pushconfig`
--

LOCK TABLES `rt_pushconfig` WRITE;
/*!40000 ALTER TABLE `rt_pushconfig` DISABLE KEYS */;
INSERT INTO `rt_pushconfig` VALUES (1,82384384,'WebSocket','8021','skecloud','管理后台框架推送',NULL,0,1,'999999','2020-08-31 10:23:24','999999','2020-08-31 10:23:24');
/*!40000 ALTER TABLE `rt_pushconfig` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_auto_job`
--

DROP TABLE IF EXISTS `sys_auto_job`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `sys_auto_job` (
  `id` bigint(20) NOT NULL,
  `JobGroupName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `JobName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `JobStatus` int(11) NOT NULL,
  `CronExpression` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL DEFAULT 'cron表达式',
  `StartTime` datetime NOT NULL,
  `EndTime` datetime NOT NULL,
  `NextStartTime` datetime NOT NULL,
  `Remark` text CHARACTER SET utf8 COLLATE utf8_bin,
  `Enabled` int(11) NOT NULL,
  `Version` int(11) NOT NULL,
  `InputTime` datetime NOT NULL,
  `UpdateTime` datetime NOT NULL,
  `InputUser` bigint(20) NOT NULL,
  `UpdateUser` bigint(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_auto_job`
--

LOCK TABLES `sys_auto_job` WRITE;
/*!40000 ALTER TABLE `sys_auto_job` DISABLE KEYS */;
/*!40000 ALTER TABLE `sys_auto_job` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_auto_job_log`
--

DROP TABLE IF EXISTS `sys_auto_job_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `sys_auto_job_log` (
  `id` bigint(20) NOT NULL,
  `JobGroupName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `JobName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `JobStatus` int(11) NOT NULL,
  `Remark` text CHARACTER SET utf8 COLLATE utf8_bin,
  `InputTime` datetime NOT NULL,
  `InputUser` bigint(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_auto_job_log`
--

LOCK TABLES `sys_auto_job_log` WRITE;
/*!40000 ALTER TABLE `sys_auto_job_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `sys_auto_job_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uc_authorize_blackip`
--

DROP TABLE IF EXISTS `uc_authorize_blackip`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `uc_authorize_blackip` (
  `id` int(11) NOT NULL COMMENT '主键',
  `Name` varchar(150) DEFAULT NULL COMMENT '标题',
  `Message` varchar(1500) DEFAULT NULL COMMENT '内容',
  `AuthorizeType` int(11) DEFAULT NULL COMMENT '类型[0>永久；1》单次；2》循环；3限时]',
  `AuthorizeCount` varchar(10) DEFAULT NULL COMMENT '次数',
  `StartDate` date DEFAULT NULL COMMENT '开始日期',
  `EndDate` date DEFAULT NULL COMMENT '结束日期',
  `RepeatWeek` int(11) DEFAULT NULL COMMENT '重复星期制【1111111】',
  `StartTime` time DEFAULT '00:00:00' COMMENT '开始时间',
  `EndTime` time DEFAULT NULL COMMENT '结束时间',
  `StartIP` varchar(50) DEFAULT NULL COMMENT '开始IP',
  `EndIP` varchar(50) DEFAULT NULL COMMENT '结束IP',
  `Enabled` int(11) DEFAULT '0' COMMENT '状态',
  `InputUser` varchar(6) DEFAULT NULL COMMENT '输入人',
  `InputTime` datetime DEFAULT NULL COMMENT '输入时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uc_authorize_blackip`
--

LOCK TABLES `uc_authorize_blackip` WRITE;
/*!40000 ALTER TABLE `uc_authorize_blackip` DISABLE KEYS */;
/*!40000 ALTER TABLE `uc_authorize_blackip` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uc_login_log`
--

DROP TABLE IF EXISTS `uc_login_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `uc_login_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Titile` varchar(150) DEFAULT NULL COMMENT '日志标题',
  `Message` varchar(1500) DEFAULT NULL COMMENT '日志内容',
  `LogType` varchar(50) DEFAULT NULL COMMENT '日志类型',
  `RequestUser` varchar(10) DEFAULT NULL COMMENT '请求者',
  `RequestTime` datetime DEFAULT NULL COMMENT '请求时间',
  `HandleTime` datetime DEFAULT NULL COMMENT '处理时间',
  `HandleUser` varchar(10) DEFAULT NULL COMMENT '处理人',
  `HandleResult` int(11) DEFAULT '0' COMMENT '处理结果',
  `HandleMessage` varchar(1500) DEFAULT NULL COMMENT '处理消息',
  `Token` varchar(50) DEFAULT NULL COMMENT '访问口令',
  `ExpiresIn` double DEFAULT NULL COMMENT '过期时间',
  `Status` int(11) DEFAULT '0' COMMENT '状态',
  `InputUser` varchar(6) DEFAULT NULL COMMENT '输入人',
  `InputTime` datetime DEFAULT NULL COMMENT '输入时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=673 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uc_login_log`
--

LOCK TABLES `uc_login_log` WRITE;
/*!40000 ALTER TABLE `uc_login_log` DISABLE KEYS */;
INSERT INTO `uc_login_log` VALUES (597,'拉取代码','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishGit','999999','2020-08-19 14:34:45','2020-08-19 14:34:45','Publish',204,'troller.cs               |   2 +-\n .../PermissionSystem.UI.WebSites.csproj            |   7 ++\n .../Views/Log/LoginLogList.cshtml                  | 123 +++++++++++++++++++++\n .../Views/Log/PublishLogList.cshtml                | 123 +++++++++++++++++++++\n SkeFramework.sln                                   |   7 ++\n 43 files changed, 943 insertions(+), 111 deletions(-)\n create mode 100644 03-Business/MicrosServices.API.PublishDeploy/Controllers/LogController.cs\n create mode 100644 03-Business/MicrosServices.Entities.Core/DataForm/LogQuery/LogQueryForm.cs\n create mode 100644 03-Business/MicrosServices.Entities/Constants/HandleUserEumns.cs\n create mode 100644 03-Business/MicrosServices.SDK.LogSystem/DataUtil/NetwordConstants.cs\n create mode 100644 03-Business/MicrosServices.SDK.LogSystem/LogBaseSDK.cs\n create mode 100644 03-Business/MicrosServices.SDK.LogSystem/LoginLogSDK.cs\n create mode 100644 03-Business/MicrosServices.SDK.LogSystem/MicrosServices.SDK.LogSystem.csproj\n create mode 100644 03-Business/MicrosServices.SDK.LogSystem/Properties/AssemblyInfo.cs\n create mode 100644 03-Business/MicrosServices.SDK.LogSystem/PublishLogSDK.cs\n create mode 100644 03-Business/MicrosServices.SDK.LogSystem/packages.config\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Controllers/LogController.cs\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/Log/LoginLogList.cshtml\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/Log/PublishLogList.cshtml',NULL,3600000,0,'999999','2020-08-19 14:34:45'),(598,'发布服务','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishCmd','999999','2020-08-19 14:35:00','2020-08-19 14:35:00','Publish',400,'NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Configuration.ConfigurationManager.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Configuration.ConfigurationManager.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Data.SQLite.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Data.SQLite.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Security.Permissions.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Security.Permissions.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];    0 个警告;    23 个错误;;已用时间 00:00:14.59;;E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter>exi',NULL,3600000,0,'999999','2020-08-19 14:35:00'),(599,'拉取代码','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishGit','999999','2020-08-19 14:39:42','2020-08-19 14:39:42','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-08-19 14:39:42'),(600,'发布服务','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishCmd','999999','2020-08-19 14:39:46','2020-08-19 14:39:46','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-08-19 14:39:46'),(601,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-19 14:39:54','2020-08-19 14:39:54','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-19 14:39:54'),(602,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-19 14:40:57','2020-08-19 14:40:57','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-19 14:40:57'),(603,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-20 08:59:54','2020-08-20 08:59:54','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-20 08:59:54'),(604,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-20 09:36:01','2020-08-20 09:36:01','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-20 09:36:01'),(605,'拉取代码','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishGit','999999','2020-08-20 09:37:05','2020-08-20 09:37:05','Publish',204,'113.0.nupkg\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net20/app.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net20/web.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net40/app.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net40/web.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net45/app.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net45/web.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net451/app.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net451/web.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net46/app.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/content/net46/web.config.transform\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/lib/net20/System.Data.SQLite.Linq.dll\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/lib/net40/System.Data.SQLite.Linq.dll\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/lib/net45/System.Data.SQLite.Linq.dll\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/lib/net451/System.Data.SQLite.Linq.dll\n create mode 100644 packages/System.Data.SQLite.Linq.1.0.113.0/lib/net46/System.Data.SQLite.Linq.dll',NULL,3600000,0,'999999','2020-08-20 09:37:05'),(606,'发布服务','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishCmd','999999','2020-08-20 09:37:22','2020-08-20 09:37:22','Publish',400,'NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Configuration.ConfigurationManager.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Configuration.ConfigurationManager.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Data.SQLite.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Data.SQLite.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Security.Permissions.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Security.Permissions.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];    0 个警告;    23 个错误;;已用时间 00:00:15.14;;E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter>exi',NULL,3600000,0,'999999','2020-08-20 09:37:22'),(607,'拉取代码','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishGit','999999','2020-08-20 09:38:01','2020-08-20 09:38:01','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-08-20 09:38:01'),(608,'发布服务','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishCmd','999999','2020-08-20 09:38:04','2020-08-20 09:38:04','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-08-20 09:38:04'),(609,'拉取代码','{\"id\":1,\"ProjectNo\":76905984,\"Name\":\"发布系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.PublishDeploy/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-12T08:10:17\"}','PublishGit','999999','2020-08-20 13:38:50','2020-08-20 13:38:50','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-08-20 13:38:50'),(610,'发布服务','{\"id\":1,\"ProjectNo\":76905984,\"Name\":\"发布系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.PublishDeploy/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-12T08:10:17\"}','PublishCmd','999999','2020-08-20 13:38:58','2020-08-20 13:38:58','Publish',400,'blishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PublishDeployApi\\System.Configuration.ConfigurationManager.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PublishDeployApi\\System.Configuration.ConfigurationManager.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PublishDeploy\\MicrosServices.API.PublishDeploy.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PublishDeployApi\\System.Data.SQLite.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PublishDeployApi\\System.Data.SQLite.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PublishDeploy\\MicrosServices.API.PublishDeploy.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PublishDeployApi\\System.Security.Permissions.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PublishDeployApi\\System.Security.Permissions.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PublishDeploy\\MicrosServices.API.PublishDeploy.csproj];    0 个警告;    23 个错误;;已用时间 00:00:07.08;;E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PublishDeploy>exi',NULL,3600000,0,'999999','2020-08-20 13:38:58'),(611,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 13:46:23','2020-08-24 13:46:23','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 13:46:23'),(612,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 13:50:37','2020-08-24 13:50:37','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 13:50:37'),(613,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 14:10:08','2020-08-24 14:10:08','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 14:10:08'),(614,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 14:18:33','2020-08-24 14:18:33','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 14:18:33'),(615,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 14:22:50','2020-08-24 14:22:50','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 14:22:50'),(616,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 14:59:51','2020-08-24 14:59:51','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 14:59:51'),(617,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 15:06:15','2020-08-24 15:06:15','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 15:06:15'),(618,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 15:55:09','2020-08-24 15:55:09','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 15:55:09'),(619,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 17:34:42','2020-08-24 17:34:42','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 17:34:42'),(620,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-24 17:36:36','2020-08-24 17:36:36','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-24 17:36:36'),(621,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-25 07:59:57','2020-08-25 07:59:57','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-25 07:59:57'),(622,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-25 08:07:06','2020-08-25 08:07:06','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-25 08:07:06'),(623,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-25 08:07:54','2020-08-25 08:07:54','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-25 08:07:54'),(624,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-25 08:10:08','2020-08-25 08:10:08','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-25 08:10:08'),(625,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-25 11:23:21','2020-08-25 11:23:21','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-25 11:23:21'),(626,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-26 12:47:19','2020-08-26 12:47:19','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-26 12:47:19'),(627,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-26 14:29:48','2020-08-26 14:29:48','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-26 14:29:48'),(628,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-26 14:58:09','2020-08-26 14:58:09','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-26 14:58:09'),(629,'拉取代码','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishGit','999999','2020-08-26 15:07:07','2020-08-26 15:07:07','Publish',204,'ryHandle/BsDictionaryHandle.cs\n create mode 100644 03-Business/MicrosServices.BLL.Business/BaseSystem/BsDictionaryHandle/IBsDictionaryHandle.cs\n create mode 100644 03-Business/MicrosServices.DAL.DataAccess/Repository/BaseSystem/BsDictionaryHandle/BsDictionaryHandleCommon.cs\n create mode 100644 03-Business/MicrosServices.DAL.DataAccess/Repository/BaseSystem/BsDictionaryHandle/IBsDictionaryHandleCommon.cs\n delete mode 100644 03-Business/MicrosServices.Entities.Core/Class1.cs\n create mode 100644 03-Business/MicrosServices.Entities/Common/BaseSystem/BsDictionary.cs\n create mode 100644 03-Business/MicrosServices.Helper.Core/Extends/DictionaryOptionValue.cs\n create mode 100644 03-Business/MicrosServices.SDK.AdminSystem/DataUtil/NetwordConstants.cs\n create mode 100644 03-Business/MicrosServices.SDK.AdminSystem/DictionarySDK.cs\n create mode 100644 03-Business/MicrosServices.SDK.AdminSystem/MicrosServices.SDK.AdminSystem.csproj\n create mode 100644 03-Business/MicrosServices.SDK.AdminSystem/Properties/AssemblyInfo.cs\n create mode 100644 03-Business/MicrosServices.SDK.AdminSystem/packages.config\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Controllers/DictionaryController.cs\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/Dictionary/DictionaryAdd.cshtml\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/Dictionary/DictionaryList.cshtml\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/Dictionary/DictionaryUpdate.cshtml',NULL,3600000,0,'999999','2020-08-26 15:07:07'),(630,'发布服务','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishCmd','999999','2020-08-26 15:07:33','2020-08-26 15:07:33','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-08-26 15:07:33'),(631,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-26 15:44:56','2020-08-26 15:44:56','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-26 15:44:56'),(632,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-27 16:16:41','2020-08-27 16:16:41','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-27 16:16:41'),(633,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-27 17:24:09','2020-08-27 17:24:09','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-27 17:24:09'),(634,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 09:11:44','2020-08-31 09:11:44','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 09:11:44'),(635,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 09:21:55','2020-08-31 09:21:55','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 09:21:55'),(636,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 09:23:17','2020-08-31 09:23:17','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 09:23:17'),(637,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 10:04:49','2020-08-31 10:04:49','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 10:04:49'),(638,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 10:06:15','2020-08-31 10:06:15','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 10:06:15'),(639,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 10:09:11','2020-08-31 10:09:11','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 10:09:11'),(640,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 10:17:24','2020-08-31 10:17:24','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 10:17:24'),(641,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 10:34:40','2020-08-31 10:34:40','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 10:34:40'),(642,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-08-31 11:47:02','2020-08-31 11:47:02','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-08-31 11:47:02'),(643,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-02 10:37:40','2020-09-02 10:37:40','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-02 10:37:40'),(644,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-02 10:39:57','2020-09-02 10:39:57','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-02 10:39:57'),(645,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-02 10:41:19','2020-09-02 10:41:19','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-02 10:41:19'),(646,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-02 10:53:00','2020-09-02 10:53:00','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-02 10:53:00'),(647,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-02 10:54:28','2020-09-02 10:54:28','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-02 10:54:28'),(648,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-07 11:07:08','2020-09-07 11:07:08','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-07 11:07:08'),(649,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-07 11:40:41','2020-09-07 11:40:41','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-07 11:40:41'),(650,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-07 11:43:54','2020-09-07 11:43:54','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-07 11:43:54'),(651,'拉取代码','{\"id\":5,\"ProjectNo\":65614592,\"Name\":\"权限系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.PermissionSystem/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-12T08:10:07\"}','PublishGit','999999','2020-09-07 11:46:21','2020-09-07 11:46:21','Publish',204,'s/RealTimeSystem/RtPushConfigHandle/IRtPushconfigHandleCommon.cs\n create mode 100644 03-Business/MicrosServices.DAL.DataAccess/RealTimeSystem/RtPushConfigHandle/RtPushconfigHandleCommon.cs\n create mode 100644 03-Business/MicrosServices.Entities/Common/RealTimeSystem/RtMessage.cs\n create mode 100644 03-Business/MicrosServices.Entities/Common/RealTimeSystem/RtPushconfig.cs\n create mode 100644 03-Business/MicrosServices.SDK.RealTimeSystem/DataUtil/NetwordConstants.cs\n create mode 100644 03-Business/MicrosServices.SDK.RealTimeSystem/MessageSDK.cs\n create mode 100644 03-Business/MicrosServices.SDK.RealTimeSystem/MicrosServices.SDK.RealTimeSystem.csproj\n create mode 100644 03-Business/MicrosServices.SDK.RealTimeSystem/Properties/AssemblyInfo.cs\n create mode 100644 03-Business/MicrosServices.SDK.RealTimeSystem/PushConfigSDK.cs\n create mode 100644 03-Business/MicrosServices.SDK.RealTimeSystem/packages.config\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Controllers/MessageController.cs\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Controllers/PushConfigController.cs\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/Message/MessageList.cshtml\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/PushConfig/PushConfigAdd.cshtml\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/PushConfig/PushConfigList.cshtml\n create mode 100644 03-Business/PermissionSystem.UI.WebSites/Views/PushConfig/PushConfigUpdate.cshtml',NULL,3600000,0,'999999','2020-09-07 11:46:21'),(652,'发布服务','{\"id\":5,\"ProjectNo\":65614592,\"Name\":\"权限系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.PermissionSystem/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-12T08:10:07\"}','PublishCmd','999999','2020-09-07 11:46:42','2020-09-07 11:46:42','Publish',400,'shTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PermissionApi\\System.Configuration.ConfigurationManager.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PermissionApi\\System.Configuration.ConfigurationManager.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PermissionSystem\\MicrosServices.API.PermissionSystem.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PermissionApi\\System.Data.SQLite.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PermissionApi\\System.Data.SQLite.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PermissionSystem\\MicrosServices.API.PermissionSystem.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PermissionApi\\System.Security.Permissions.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PermissionApi\\System.Security.Permissions.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PermissionSystem\\MicrosServices.API.PermissionSystem.csproj];    0 个警告;    22 个错误;;已用时间 00:00:19.14;;E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PermissionSystem>exi',NULL,3600000,0,'999999','2020-09-07 11:46:42'),(653,'拉取代码','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishGit','999999','2020-09-07 11:48:22','2020-09-07 11:48:22','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-09-07 11:48:22'),(654,'发布服务','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishCmd','999999','2020-09-07 11:48:25','2020-09-07 11:48:25','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-09-07 11:48:25'),(655,'拉取代码','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishGit','999999','2020-09-07 11:48:56','2020-09-07 11:48:56','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-09-07 11:48:56'),(656,'发布服务','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishCmd','999999','2020-09-07 11:48:58','2020-09-07 11:48:58','Publish',400,'sh\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\AdminServerApi\\System.Configuration.ConfigurationManager.dll”。Access to the path \'C:\\inetpub\\wwwroot\\AdminServerApi\\System.Configuration.ConfigurationManager.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.AdminServer\\MicrosServices.API.AdminServer.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\AdminServerApi\\System.Data.SQLite.dll”。Access to the path \'C:\\inetpub\\wwwroot\\AdminServerApi\\System.Data.SQLite.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.AdminServer\\MicrosServices.API.AdminServer.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\AdminServerApi\\System.Security.Permissions.dll”。Access to the path \'C:\\inetpub\\wwwroot\\AdminServerApi\\System.Security.Permissions.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.AdminServer\\MicrosServices.API.AdminServer.csproj];    0 个警告;    19 个错误;;已用时间 00:00:01.78;;E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.AdminServer>exi',NULL,3600000,0,'999999','2020-09-07 11:48:58'),(657,'拉取代码','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishGit','999999','2020-09-07 11:49:03','2020-09-07 11:49:03','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-09-07 11:49:03'),(658,'发布服务','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishCmd','999999','2020-09-07 11:49:05','2020-09-07 11:49:05','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-09-07 11:49:05'),(659,'拉取代码','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishGit','999999','2020-09-07 11:49:35','2020-09-07 11:49:35','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-09-07 11:49:35'),(660,'发布服务','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishCmd','999999','2020-09-07 11:49:38','2020-09-07 11:49:38','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-09-07 11:49:38'),(661,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-07 13:39:51','2020-09-07 13:39:51','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-07 13:39:51'),(662,'拉取代码','{\"id\":5,\"ProjectNo\":65614592,\"Name\":\"权限系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.PermissionSystem/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-12T08:10:07\"}','PublishGit','999999','2020-09-07 13:40:06','2020-09-07 13:40:06','Publish',204,'Updating 05b58a2..efb3a97\nFast-forward\n 01-Src/SkeFramework.DataBase.DataAccess/Common/DataFactory/DbFactory.cs | 2 +-\n 03-Business/PermissionSystem.UI.WebSites/Views/Shared/_Layout.cshtml    | 2 +-\n 2 files changed, 2 insertions(+), 2 deletions(-)\n',NULL,3600000,0,'999999','2020-09-07 13:40:06'),(663,'发布服务','{\"id\":5,\"ProjectNo\":65614592,\"Name\":\"权限系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.PermissionSystem/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-12T08:10:07\"}','PublishCmd','999999','2020-09-07 13:40:12','2020-09-07 13:40:12','Publish',400,'shTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PermissionApi\\System.Configuration.ConfigurationManager.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PermissionApi\\System.Configuration.ConfigurationManager.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PermissionSystem\\MicrosServices.API.PermissionSystem.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PermissionApi\\System.Data.SQLite.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PermissionApi\\System.Data.SQLite.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PermissionSystem\\MicrosServices.API.PermissionSystem.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\PermissionApi\\System.Security.Permissions.dll”。Access to the path \'C:\\inetpub\\wwwroot\\PermissionApi\\System.Security.Permissions.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PermissionSystem\\MicrosServices.API.PermissionSystem.csproj];    0 个警告;    20 个错误;;已用时间 00:00:04.97;;E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.PermissionSystem>exi',NULL,3600000,0,'999999','2020-09-07 13:40:12'),(664,'拉取代码','{\"id\":5,\"ProjectNo\":65614592,\"Name\":\"权限系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.PermissionSystem/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-12T08:10:07\"}','PublishGit','999999','2020-09-07 13:40:17','2020-09-07 13:40:17','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-09-07 13:40:17'),(665,'发布服务','{\"id\":5,\"ProjectNo\":65614592,\"Name\":\"权限系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.PermissionSystem/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-12T08:10:07\"}','PublishCmd','999999','2020-09-07 13:40:20','2020-09-07 13:40:20','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-09-07 13:40:20'),(666,'拉取代码','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishGit','999999','2020-09-07 13:41:47','2020-09-07 13:41:47','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-09-07 13:41:47'),(667,'发布服务','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishCmd','999999','2020-09-07 13:41:51','2020-09-07 13:41:51','Publish',400,'NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Configuration.ConfigurationManager.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Configuration.ConfigurationManager.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Data.SQLite.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Data.SQLite.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];C:\\Program Files\\dotnet\\sdk\\2.2.101\\Sdks\\Microsoft.NET.Sdk.Publish\\build\\netstandard1.0\\PublishTargets\\Microsoft.NET.Sdk.Publish.FileSystem.targets(63,5): error MSB3061: 无法删除文件“C:\\inetpub\\wwwroot\\UserCenterApi\\System.Security.Permissions.dll”。Access to the path \'C:\\inetpub\\wwwroot\\UserCenterApi\\System.Security.Permissions.dll\' is denied. [E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter\\MicrosServices.API.UserCenter.csproj];    0 个警告;    21 个错误;;已用时间 00:00:03.03;;E:\\JProject\\GitRepository\\SkeCloud\\SkeFramework\\03-Business\\MicrosServices.API.UserCenter>exi',NULL,3600000,0,'999999','2020-09-07 13:41:51'),(668,'拉取代码','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishGit','999999','2020-09-07 13:41:55','2020-09-07 13:41:55','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-09-07 13:41:55'),(669,'发布服务','{\"id\":4,\"ProjectNo\":83009536,\"Name\":\"用户系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.UserCenter/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"0001-01-01T00:00:00\",\"UpdateTime\":\"2020-08-11T11:36:48\"}','PublishCmd','999999','2020-09-07 13:41:58','2020-09-07 13:41:58','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-09-07 13:41:58'),(670,'拉取代码','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishGit','999999','2020-09-07 13:42:41','2020-09-07 13:42:41','Publish',204,'Already up to date.\n',NULL,3600000,0,'999999','2020-09-07 13:42:41'),(671,'发布服务','{\"id\":7,\"ProjectNo\":11924992,\"Name\":\"基础服务系统\",\"VersionType\":\"Github\",\"VersionUrl\":\"https://gitee.com/SkeCloud/SkeFramework\",\"GitBranch\":\"dev\",\"GitBinPath\":\"D:\\\\Program Files\\\\Git\\\\cmd\\\\git.exe\",\"SourcePath\":\"E:\\\\JProject\\\\GitRepository\\\\SkeCloud\\\\SkeFramework\",\"MSBuildPath\":\"C:\\\\Windows\\\\System32\\\\cmd.exe\",\"ProjectFile\":\"\\\\03-Business\\\\MicrosServices.API.AdminServer/Publish.bat\",\"notifyEmails\":\"502525164@qq.com,\",\"InputUser\":\"999999\",\"InputTime\":\"2020-08-24T17:37:19\",\"UpdateTime\":\"0001-01-01T00:00:00\"}','PublishCmd','999999','2020-09-07 13:42:44','2020-09-07 13:42:44','Publish',205,'发布成功',NULL,3600000,0,'999999','2020-09-07 13:42:44'),(672,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-09-07 13:43:21','2020-09-07 13:43:21','UserCenter',201,'登录成功',NULL,3600000,0,'999999','2020-09-07 13:43:21');
/*!40000 ALTER TABLE `uc_login_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uc_users`
--

DROP TABLE IF EXISTS `uc_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `uc_users` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键\n\n            ',
  `UserNo` varchar(6) DEFAULT NULL COMMENT '用户账号',
  `UserName` varchar(20) DEFAULT NULL COMMENT '用户名',
  `Password` varchar(50) DEFAULT NULL COMMENT '登录密码',
  `FullName` varchar(20) DEFAULT NULL COMMENT '真实名称',
  `NickName` varchar(20) DEFAULT NULL COMMENT '用户昵称',
  `IsExpire` int(11) DEFAULT NULL COMMENT '是否过期',
  `IdentityCard` varchar(18) DEFAULT NULL COMMENT '身份证号码',
  `Phone` varchar(20) DEFAULT NULL COMMENT '电话',
  `Email` varchar(50) DEFAULT NULL COMMENT '邮箱',
  `Address` varchar(200) DEFAULT NULL COMMENT '居住地址',
  `WorkAddress` varchar(200) DEFAULT NULL COMMENT '工作地址',
  `Gender` int(11) DEFAULT NULL COMMENT '性别【0,1,2来表示，未知,男,女】',
  `Birthday` varchar(20) DEFAULT NULL COMMENT '生日',
  `QQ` varchar(20) DEFAULT NULL COMMENT 'QQ',
  `WeChat` varchar(20) DEFAULT NULL COMMENT 'WeChat',
  `Signature` varchar(200) DEFAULT NULL COMMENT '个性签名',
  `ImageHead` varchar(20) DEFAULT NULL COMMENT '头像',
  `Note` varchar(200) DEFAULT NULL COMMENT '备注',
  `StatusAudit` int(11) DEFAULT NULL COMMENT '审核状态',
  `InputUser` varchar(6) DEFAULT NULL COMMENT '创建人',
  `InputTime` datetime DEFAULT NULL COMMENT '创建时间',
  `UpdateUser` varchar(6) DEFAULT NULL COMMENT '更新人',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  `Enabled` int(11) DEFAULT NULL COMMENT '是否启用',
  `LastLoginIP` varchar(20) DEFAULT NULL COMMENT '上次登录IP',
  `LastLoginTime` datetime DEFAULT NULL COMMENT '上次登录时间',
  `LastLoginMac` varchar(20) DEFAULT NULL COMMENT '上次登录MAC地址',
  `FailedLoginCount` int(11) DEFAULT NULL COMMENT '错误登录次数',
  `FailedLoginTime` datetime DEFAULT NULL COMMENT '错误登录时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uc_users`
--

LOCK TABLES `uc_users` WRITE;
/*!40000 ALTER TABLE `uc_users` DISABLE KEYS */;
INSERT INTO `uc_users` VALUES (1,'999999','admin','c36725de6a71af4a302b55c0ab4fabcc','zengyingjun','Sunshall',0,'0','0','zengyingjun@ut.cn','0','0',0,'0','0','0','0','0','0',0,'0','2019-11-02 00:00:00','0','2019-11-02 00:00:00',1,'0','2019-11-02 00:00:00','0',0,'2020-09-07 13:43:21'),(4,'999996','test','c36725de6a71af4a302b55c0ab4fabcc',NULL,NULL,0,NULL,'123','123',NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL,0,'999999','2020-03-31 21:04:59',NULL,'0001-01-01 00:00:00',1,NULL,'0001-01-01 00:00:00',NULL,0,'2020-08-11 14:46:10');
/*!40000 ALTER TABLE `uc_users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uc_users_setting`
--

DROP TABLE IF EXISTS `uc_users_setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `uc_users_setting` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `UserNo` varchar(6) DEFAULT NULL COMMENT '用户账号',
  `AppId` varchar(45) DEFAULT NULL COMMENT '用户ID',
  `AppSecret` varchar(45) DEFAULT NULL COMMENT '用户密钥',
  `PlatformNo` varchar(45) DEFAULT NULL COMMENT '平台编号',
  `ManagementId` int(11) DEFAULT NULL COMMENT '权限角色ID',
  `ManagementValue` double DEFAULT NULL COMMENT '权限值',
  `OrgNo` char(10) DEFAULT NULL COMMENT '用户所属组织',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(45) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='用户设定表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uc_users_setting`
--

LOCK TABLES `uc_users_setting` WRITE;
/*!40000 ALTER TABLE `uc_users_setting` DISABLE KEYS */;
INSERT INTO `uc_users_setting` VALUES (2,'999999','999999','999999','88073472',1,1,NULL,1,'999999','2020-01-12 13:14:51'),(5,'999996','355454','196825','88073472',1,1,NULL,1,'999999','2020-03-31 21:04:59');
/*!40000 ALTER TABLE `uc_users_setting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vc_record_type`
--

DROP TABLE IF EXISTS `vc_record_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `vc_record_type` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `RecordNo` bigint(20) DEFAULT NULL COMMENT '版本号记录编号',
  `ParentRecordNo` bigint(20) DEFAULT NULL COMMENT '所属版本号',
  `TypeNo` bigint(20) DEFAULT NULL COMMENT '版本类型编号',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='版本号映射表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vc_record_type`
--

LOCK TABLES `vc_record_type` WRITE;
/*!40000 ALTER TABLE `vc_record_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `vc_record_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vc_version_record`
--

DROP TABLE IF EXISTS `vc_version_record`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `vc_version_record` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `RecordNo` bigint(20) DEFAULT NULL COMMENT '版本号记录编号',
  `TypeNo` bigint(20) DEFAULT NULL COMMENT '类型编码',
  `Version` varchar(120) DEFAULT NULL COMMENT '版本号',
  `Description` varchar(512) DEFAULT NULL COMMENT '描述',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='版本号记录';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vc_version_record`
--

LOCK TABLES `vc_version_record` WRITE;
/*!40000 ALTER TABLE `vc_version_record` DISABLE KEYS */;
/*!40000 ALTER TABLE `vc_version_record` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vc_version_type`
--

DROP TABLE IF EXISTS `vc_version_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `vc_version_type` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `TypeNo` bigint(20) DEFAULT NULL COMMENT '组织编号',
  `Name` varchar(120) DEFAULT NULL COMMENT '类型名称',
  `Code` varchar(50) DEFAULT NULL COMMENT '类型值',
  `Description` varchar(512) DEFAULT NULL COMMENT '描述',
  `Enabled` int(11) DEFAULT NULL COMMENT '启用状态',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='版本类型';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vc_version_type`
--

LOCK TABLES `vc_version_type` WRITE;
/*!40000 ALTER TABLE `vc_version_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `vc_version_type` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-11-03 10:57:52
