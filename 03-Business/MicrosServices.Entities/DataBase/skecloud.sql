-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: skecloud
-- ------------------------------------------------------
-- Server version	5.7.14-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `pd_project`
--

DROP TABLE IF EXISTS `pd_project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pd_project` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `ProjectNo` bigint(20) DEFAULT NULL COMMENT '编码',
  `Name` varchar(120) DEFAULT NULL COMMENT '名称',
  `VersionType` varchar(50) DEFAULT NULL COMMENT '版本控制类型[Git/SVN]',
  `VersionUrl` varchar(512) DEFAULT NULL COMMENT '版本地址',
  `SourcePath` varchar(200) DEFAULT NULL COMMENT '源代码地址',
  `MSBuildPath` varchar(200) DEFAULT NULL COMMENT '打包程序路径',
  `ProjectFile` varchar(200) DEFAULT NULL COMMENT '项目文件',
  `notifyEmails` varchar(512) DEFAULT NULL COMMENT '通知邮箱列表',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='项目表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pd_project`
--

LOCK TABLES `pd_project` WRITE;
/*!40000 ALTER TABLE `pd_project` DISABLE KEYS */;
INSERT INTO `pd_project` VALUES (1,76905984,'发布系统','Github','https://github.com/0582zyj/SkeFramework','D:\\Github\\SkeFramework\\03-Business\\MicrosServices.API.PublishDeploy/MicrosServices.API.PublishDeploy.csproj','E:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Professional\\MSBuild\\15.0\\Bin\\MSBuild.exe','IISProfile.pubxml','502525164@qq.com,','999999','0001-01-01 00:00:00','2020-04-30 00:11:26');
/*!40000 ALTER TABLE `pd_project` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pd_publish`
--

DROP TABLE IF EXISTS `pd_publish`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8 COMMENT='权限表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_management`
--

LOCK TABLES `ps_management` WRITE;
/*!40000 ALTER TABLE `ps_management` DISABLE KEYS */;
INSERT INTO `ps_management` VALUES (4,36115712,-1,'_-1','框架菜单',NULL,'0',2,1,88073472,1,'999999','2019-12-16 22:25:27','2019-12-26 22:34:13'),(5,84787968,-1,'_-1','权限系统菜单',NULL,'0',2,11,88073472,1,'999999','2020-01-01 23:11:21','2020-02-02 10:36:18'),(6,52697600,84787968,'_-1_84787968','菜单更新权限',NULL,'menuUpdate',1,1,88073472,1,'999999','2020-01-04 00:23:30','2020-02-07 15:57:33'),(7,45914624,84787968,'_-1_84787968','菜单新增权限',NULL,'menuAdd',1,2,88073472,1,'999999','2020-02-06 16:48:30','2020-04-22 21:31:25'),(8,79318016,84787968,'_-1_84787968','菜单删除权限','1','menuDelete',1,3,88073472,1,'999999','2020-02-07 15:30:48','0001-01-01 00:00:00'),(9,89873408,84787968,'_-1_84787968','菜单权限授权',NULL,'menuManagementAssign',1,4,88073472,1,'999999','2020-02-07 15:33:45','2020-02-07 15:33:56'),(10,26164992,84787968,'_-1_84787968','权限新增权限','1','managementAdd',1,1,88073472,1,'999999','2020-02-07 15:37:35','0001-01-01 00:00:00'),(11,78745088,84787968,'_-1_84787968','权限更新权限',NULL,'managementUpdate',1,2,88073472,1,'999999','2020-02-07 15:57:18','2020-02-07 15:58:51'),(12,4728064,84787968,'_-1_84787968','权限删除权限','2','managementDelete',1,3,88073472,1,'999999','2020-02-07 15:59:23','0001-01-01 00:00:00'),(13,33165056,84787968,'_-1_84787968','权限菜单授权','4','managementMenuAssign',1,4,88073472,1,'999999','2020-02-07 15:59:53','0001-01-01 00:00:00'),(14,28688896,84787968,'_-1_84787968','角色新增权限','1','roleAdd',1,1,88073472,1,'999999','2020-02-07 17:11:54','0001-01-01 00:00:00'),(15,80731904,84787968,'_-1_84787968','角色更新权限','2','roleUpdate',1,2,88073472,1,'999999','2020-02-07 17:13:09','0001-01-01 00:00:00'),(16,17200128,84787968,'_-1_84787968','角色删除权限','12','roleDelete',1,3,88073472,1,'999999','2020-02-07 20:17:09','0001-01-01 00:00:00'),(17,75594240,84787968,'_-1_84787968','角色权限分配','4','roleManagementAssign',1,4,88073472,1,'999999','2020-02-07 20:18:48','0001-01-01 00:00:00'),(18,30197248,84787968,'_-1_84787968','机构新增权限','1','orgAdd',1,1,88073472,1,'999999','2020-02-07 20:22:23','0001-01-01 00:00:00'),(19,97827840,84787968,'_-1_84787968','机构更新权限','2','orgUpdate',1,2,88073472,1,'999999','2020-02-07 20:22:55','0001-01-01 00:00:00'),(20,79525632,84787968,'_-1_84787968','机构删除权限','3','orgDelete',1,3,88073472,1,'999999','2020-02-07 20:23:31','0001-01-01 00:00:00'),(21,23708928,84787968,'_-1_84787968','机构角色授权','4','orgRoleAssign',1,4,88073472,1,'999999','2020-02-07 20:24:07','0001-01-01 00:00:00'),(22,36370432,36115712,'_-1_36115712','平台新增权限','1','platformAdd',1,1,88073472,1,'999999','2020-02-08 10:31:56','0001-01-01 00:00:00'),(23,4016128,36115712,'_-1_36115712','平台更新管理','1','platformUpdate',1,2,88073472,1,'999999','2020-02-08 10:32:19','0001-01-01 00:00:00'),(24,99909888,36115712,'_-1_36115712','平台删除权限','1','platformDelete',1,3,88073472,1,'999999','2020-02-08 10:32:41','0001-01-01 00:00:00'),(25,72390144,72114688,'_-1_72114688','用户新增权限',NULL,'userAdd',1,2,88073472,1,'999999','2020-03-31 20:01:26','2020-04-22 21:34:03'),(26,9599744,-1,'_-1','发布系统菜单','发布系统菜单','PublishDeployMenu',2,2,88073472,1,'999999','2020-04-12 16:57:19','0001-01-01 00:00:00'),(27,72114688,-1,'_-1','用户系统菜单','usercentermenu','usercentermenu',2,4,88073472,1,'999999','2020-04-14 22:30:57','0001-01-01 00:00:00');
/*!40000 ALTER TABLE `ps_management` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_management_roles`
--

DROP TABLE IF EXISTS `ps_management_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ps_management_roles` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `RolesNo` bigint(20) DEFAULT NULL COMMENT '角色编号',
  `ManagementNo` bigint(20) DEFAULT NULL COMMENT '权限编号',
  `ManagementValue` varchar(120) DEFAULT NULL COMMENT '权限值',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 COMMENT='用户角色关系表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_management_roles`
--

LOCK TABLES `ps_management_roles` WRITE;
/*!40000 ALTER TABLE `ps_management_roles` DISABLE KEYS */;
INSERT INTO `ps_management_roles` VALUES (10,53635840,84787968,'0','999999','2020-02-02 10:37:06','2020-02-02 10:37:06'),(11,53635840,52697600,'0','999999','2020-02-02 10:37:06','2020-02-02 10:37:06'),(13,53635840,33165056,'0','999999','2020-03-31 20:38:19','2020-03-31 20:38:19'),(19,49830144,36115712,'0','999999','2020-04-14 22:31:28','2020-04-14 22:31:28'),(20,49830144,84787968,'0','999999','2020-04-14 22:31:28','2020-04-14 22:31:28'),(21,49830144,9599744,'PublishDeployMenu','999999','2020-04-14 22:31:28','2020-04-14 22:31:28'),(22,49830144,72114688,'usercentermenu','999999','2020-04-14 22:31:28','2020-04-14 22:31:28');
/*!40000 ALTER TABLE `ps_management_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_menu`
--

DROP TABLE IF EXISTS `ps_menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COMMENT='菜单表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_menu`
--

LOCK TABLES `ps_menu` WRITE;
/*!40000 ALTER TABLE `ps_menu` DISABLE KEYS */;
INSERT INTO `ps_menu` VALUES (1,99999999,15325696,'_-1_15325696','菜单管理','menu','1','/Menu/MenuList',1,88073472,1,'999999','2019-11-22 00:00:00','2020-02-03 20:31:51'),(2,99479040,15325696,'_-1_15325696','权限管理','management','1','/management/managementlist',2,88073472,1,'999999','2019-11-28 23:06:33','2020-02-03 21:30:24'),(3,50878208,41653248,'_-1_41653248','平台管理','platform','1','/platform/platformlist',30,88073472,1,'999999','2019-11-28 23:09:15','2020-02-03 21:57:14'),(4,76524032,15325696,'_-1_15325696','角色管理','role','1','/roles/rolesList',4,88073472,1,'999999','2019-11-28 23:09:41','2020-02-03 21:30:38'),(5,21080064,85645568,'_-1_85645568','用户管理','user','1','/User/UserList',5,88073472,1,'999999','2020-01-11 14:45:33','2020-02-03 21:42:56'),(6,99464448,15325696,'_-1_15325696','机构管理','organization','1','/Organization/OrganizationList',6,88073472,1,'999999','2020-02-02 10:53:44','2020-02-03 21:30:53'),(7,15325696,-1,'_-1','权限系统','permission','1','#',1,88073472,1,'999999','2020-02-03 20:31:17','2020-02-03 21:57:54'),(8,85645568,-1,'_-1','用户系统','UserSystem','1','#',2,88073472,1,'999999','2020-02-03 21:42:36','0001-01-01 00:00:00'),(9,41653248,-1,'_-1','框架系统','SkeCloud','1','#',0,88073472,1,'999999','2020-02-03 21:57:02','0001-01-01 00:00:00'),(10,58688512,-1,'_-1','发布系统','PublishDeploy',NULL,'#',4,88073472,1,'999999','2020-04-12 16:30:03','0001-01-01 00:00:00'),(11,68522752,58688512,'_-1_58688512','服务器','Server',NULL,'/Server/ServerList',0,88073472,1,'999999','2020-04-12 16:32:17','0001-01-01 00:00:00'),(12,4764416,58688512,'_58688512','项目管理','ProjectManager',NULL,'/Project/ProjectList',2,88073472,1,'999999','2020-04-29 21:28:35','0001-01-01 00:00:00');
/*!40000 ALTER TABLE `ps_menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_menu_management`
--

DROP TABLE IF EXISTS `ps_menu_management`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=129 DEFAULT CHARSET=utf8 COMMENT='菜单权限关系表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_menu_management`
--

LOCK TABLES `ps_menu_management` WRITE;
/*!40000 ALTER TABLE `ps_menu_management` DISABLE KEYS */;
INSERT INTO `ps_menu_management` VALUES (73,NULL,NULL,99479040,26164992,1,'999999','2020-02-07 16:47:07','2020-02-07 16:47:07'),(74,NULL,NULL,99479040,78745088,1,'999999','2020-02-07 16:47:07','2020-02-07 16:47:07'),(75,NULL,NULL,99479040,4728064,1,'999999','2020-02-07 16:47:07','2020-02-07 16:47:07'),(76,NULL,NULL,99479040,33165056,1,'999999','2020-02-07 16:47:07','2020-02-07 16:47:07'),(80,NULL,NULL,76524032,28688896,1,'999999','2020-02-07 20:19:07','2020-02-07 20:19:07'),(81,NULL,NULL,76524032,80731904,1,'999999','2020-02-07 20:19:07','2020-02-07 20:19:07'),(82,NULL,NULL,76524032,17200128,1,'999999','2020-02-07 20:19:07','2020-02-07 20:19:07'),(83,NULL,NULL,76524032,75594240,1,'999999','2020-02-07 20:19:07','2020-02-07 20:19:07'),(84,NULL,NULL,99464448,30197248,1,'999999','2020-02-07 20:25:33','2020-02-07 20:25:33'),(85,NULL,NULL,99464448,97827840,1,'999999','2020-02-07 20:25:33','2020-02-07 20:25:33'),(86,NULL,NULL,99464448,79525632,1,'999999','2020-02-07 20:25:33','2020-02-07 20:25:33'),(87,NULL,NULL,99464448,23708928,1,'999999','2020-02-07 20:25:33','2020-02-07 20:25:33'),(88,NULL,NULL,50878208,36370432,1,'999999','2020-02-08 10:33:13','2020-02-08 10:33:13'),(89,NULL,NULL,50878208,4016128,1,'999999','2020-02-08 10:33:13','2020-02-08 10:33:13'),(90,NULL,NULL,50878208,99909888,1,'999999','2020-02-08 10:33:13','2020-02-08 10:33:13'),(91,NULL,NULL,99999999,52697600,1,'999999','2020-02-08 10:38:44','2020-02-08 10:38:44'),(92,NULL,NULL,99999999,45914624,1,'999999','2020-02-08 10:38:44','2020-02-08 10:38:44'),(93,NULL,NULL,99999999,79318016,1,'999999','2020-02-08 10:38:44','2020-02-08 10:38:44'),(94,NULL,NULL,99999999,89873408,1,'999999','2020-02-08 10:38:44','2020-02-08 10:38:44'),(95,NULL,NULL,21080064,72390144,1,'999999','2020-03-31 20:02:53','2020-03-31 20:02:53'),(117,NULL,NULL,50878208,36115712,2,'999999','2020-04-14 22:29:53','2020-04-14 22:29:53'),(118,NULL,NULL,41653248,36115712,2,'999999','2020-04-14 22:29:53','2020-04-14 22:29:53'),(119,NULL,NULL,21080064,72114688,2,'999999','2020-04-14 22:31:14','2020-04-14 22:31:14'),(120,NULL,NULL,85645568,72114688,2,'999999','2020-04-14 22:31:14','2020-04-14 22:31:14'),(121,NULL,NULL,99999999,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(122,NULL,NULL,99479040,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(123,NULL,NULL,76524032,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(124,NULL,NULL,99464448,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(125,NULL,NULL,15325696,84787968,2,'999999','2020-04-14 22:39:01','2020-04-14 22:39:01'),(126,NULL,NULL,58688512,9599744,2,'999999','2020-04-29 21:29:20','2020-04-29 21:29:20'),(127,NULL,NULL,68522752,9599744,2,'999999','2020-04-29 21:29:20','2020-04-29 21:29:20'),(128,NULL,NULL,4764416,9599744,2,'999999','2020-04-29 21:29:20','2020-04-29 21:29:20');
/*!40000 ALTER TABLE `ps_menu_management` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_org_roles`
--

DROP TABLE IF EXISTS `ps_org_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='权限组织机构';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_organization`
--

LOCK TABLES `ps_organization` WRITE;
/*!40000 ALTER TABLE `ps_organization` DISABLE KEYS */;
INSERT INTO `ps_organization` VALUES (1,53893376,-1,'_-1','五邑创客','12','0',88073472,1,'999999','2020-02-02 15:44:33','2020-02-03 14:42:20');
/*!40000 ALTER TABLE `ps_organization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_platform`
--

DROP TABLE IF EXISTS `ps_platform`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='平台表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_platform`
--

LOCK TABLES `ps_platform` WRITE;
/*!40000 ALTER TABLE `ps_platform` DISABLE KEYS */;
INSERT INTO `ps_platform` VALUES (1,88073472,-1,'-1','框架','SkeCloud','admin','999999','999999','0001-01-01 00:00:00','2019-12-14 14:24:42');
/*!40000 ALTER TABLE `ps_platform` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_roles`
--

DROP TABLE IF EXISTS `ps_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='角色表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_roles`
--

LOCK TABLES `ps_roles` WRITE;
/*!40000 ALTER TABLE `ps_roles` DISABLE KEYS */;
INSERT INTO `ps_roles` VALUES (5,49830144,-1,'_-1','框架管理员','1','skecloudadmin',88073472,1,'999999','2019-12-30 20:12:57','2020-02-05 21:36:54'),(6,53635840,-1,'_-1','权限管理员','权限系统','permissionAdmin',88073472,1,'999999','2020-02-02 10:32:38','2020-04-12 16:56:05');
/*!40000 ALTER TABLE `ps_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ps_user_org`
--

DROP TABLE IF EXISTS `ps_user_org`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ps_user_roles` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `RolesNo` bigint(20) DEFAULT NULL COMMENT '角色编号',
  `UserNo` bigint(20) DEFAULT NULL COMMENT '用户编号',
  `InputUser` varchar(10) DEFAULT NULL COMMENT '操作员',
  `InputTime` datetime DEFAULT NULL COMMENT '操作时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='用户角色关系表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ps_user_roles`
--

LOCK TABLES `ps_user_roles` WRITE;
/*!40000 ALTER TABLE `ps_user_roles` DISABLE KEYS */;
INSERT INTO `ps_user_roles` VALUES (1,49830144,999999,'999999','2020-01-19 13:07:00','2020-01-19 13:07:00');
/*!40000 ALTER TABLE `ps_user_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_auto_job`
--

DROP TABLE IF EXISTS `sys_auto_job`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_auto_job` (
  `id` bigint(20) NOT NULL,
  `JobGroupName` varchar(50) COLLATE utf8_bin NOT NULL,
  `JobName` varchar(50) COLLATE utf8_bin NOT NULL,
  `JobStatus` int(11) NOT NULL,
  `CronExpression` varchar(50) COLLATE utf8_bin NOT NULL DEFAULT 'cron表达式',
  `StartTime` datetime NOT NULL,
  `EndTime` datetime NOT NULL,
  `NextStartTime` datetime NOT NULL,
  `Remark` text COLLATE utf8_bin,
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
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_auto_job_log` (
  `id` bigint(20) NOT NULL,
  `JobGroupName` varchar(50) COLLATE utf8_bin NOT NULL,
  `JobName` varchar(50) COLLATE utf8_bin NOT NULL,
  `JobStatus` int(11) NOT NULL,
  `Remark` text COLLATE utf8_bin,
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
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = utf8 */;
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
  `HandleMessage` varchar(500) DEFAULT NULL COMMENT '处理消息',
  `Token` varchar(50) DEFAULT NULL COMMENT '访问口令',
  `ExpiresIn` double DEFAULT NULL COMMENT '过期时间',
  `Status` int(11) DEFAULT '0' COMMENT '状态',
  `InputUser` varchar(6) DEFAULT NULL COMMENT '输入人',
  `InputTime` datetime DEFAULT NULL COMMENT '输入时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uc_login_log`
--

LOCK TABLES `uc_login_log` WRITE;
/*!40000 ALTER TABLE `uc_login_log` DISABLE KEYS */;
INSERT INTO `uc_login_log` VALUES (29,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-06-21 21:35:37','2020-06-21 21:35:37','SkeCloud',200,'注销成功',NULL,3600000,0,'999999','2020-06-21 21:35:37'),(30,'登录类型','{\"UserName\":\"admin\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"code\"}','Login','999999','2020-06-21 21:45:55','2020-06-21 21:45:55','code',200,'登录成功',NULL,3600000,0,'999999','2020-06-21 21:45:55'),(31,'登录类型','{\"UserName\":\"admin\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"code\"}','Login','999999','2020-06-21 21:48:24','2020-06-21 21:48:24','code',200,'注销成功',NULL,3600000,0,'999999','2020-06-21 21:48:24'),(32,'登录类型','{\"UserName\":\"admin\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"code\"}','Login','999999','2020-06-21 21:51:45','2020-06-21 21:51:45','code',201,'登录成功',NULL,3600000,0,'999999','2020-06-21 21:51:45'),(33,'登录类型','{\"UserName\":\"zengyingjun@ut.cn\",\"MdfPas\":\"c36725de6a71af4a302b55c0ab4fabcc\",\"LoginerInfo\":\"123\",\"Platform\":\"SkeCloud\"}','Login','999999','2020-06-21 22:49:56','2020-06-21 22:49:56','SkeCloud',201,'登录成功',NULL,3600000,0,'999999','2020-06-21 22:49:56');
/*!40000 ALTER TABLE `uc_login_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uc_users`
--

DROP TABLE IF EXISTS `uc_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
INSERT INTO `uc_users` VALUES (1,'999999','admin','c36725de6a71af4a302b55c0ab4fabcc','zengyingjun','Sunshall',0,'0','0','zengyingjun@ut.cn','0','0',0,'0','0','0','0','0','0',0,'0','2019-11-02 00:00:00','0','2019-11-02 00:00:00',1,'0','2019-11-02 00:00:00','0',0,'2020-06-21 22:49:56'),(4,'999996','test','cebfd1559b68d67688884d7a3d3e8c',NULL,NULL,0,NULL,'123','123',NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL,0,'999999','2020-03-31 21:04:59',NULL,'0001-01-01 00:00:00',1,NULL,'0001-01-01 00:00:00',NULL,0,'2020-05-28 21:29:02');
/*!40000 ALTER TABLE `uc_users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uc_users_setting`
--

DROP TABLE IF EXISTS `uc_users_setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-07-22 22:14:51
