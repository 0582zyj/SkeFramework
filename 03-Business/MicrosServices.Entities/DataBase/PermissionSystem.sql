/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2019/12/13 15:09:31                          */
/*==============================================================*/


drop table if exists ps_management;

drop table if exists ps_management_roles;

drop table if exists ps_menu;

drop table if exists ps_menu_management;

drop table if exists ps_org_roles;

drop table if exists ps_organization;

drop table if exists ps_platform;

drop table if exists ps_roles;

drop table if exists ps_user_org;

drop table if exists ps_user_roles;

drop table if exists uc_authorize_blackip;

drop table if exists uc_login_log;

drop table if exists uc_users;

drop table if exists uc_users_setting;

drop table if exists vc_record_type;

drop table if exists vc_version_record;

drop table if exists vc_version_type;

/*==============================================================*/
/* Table: ps_management                                         */
/*==============================================================*/
create table ps_management
(
   id                   bigint not null auto_increment comment 'ID',
   ManagementNo         bigint,
   ParentNo             bigint default NULL comment '父节点',
   TreeLevelNo          varchar(500) comment '树节点编号',
   Name                 varchar(120) default NULL comment '名称',
   Description          varchar(512),
   Value                bigint default NULL comment '权限值',
   Type                 int comment '权限类型【0默认权限，1普通权限，2菜单权限】',
   Sort                 int(11) default NULL comment '排序',
   PlatformNo           int comment '平台编号',
   Enabled              int default NULL comment '启用状态',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table ps_management comment '权限表';

/*==============================================================*/
/* Table: ps_management_roles                                   */
/*==============================================================*/
create table ps_management_roles
(
   id                   bigint not null auto_increment comment 'ID',
   RolesNo              bigint default NULL comment '角色编号',
   ManagementNo         bigint default NULL comment '权限编号',
   ManagementValue      bigint comment '权限值',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table ps_management_roles comment '用户角色关系表';

/*==============================================================*/
/* Table: ps_menu                                               */
/*==============================================================*/
create table ps_menu
(
   id                   bigint not null auto_increment comment 'ID',
   MenuNo               bigint comment '菜单编号',
   ParentNo             bigint default NULL comment '父节点',
   TreeLevelNo          varchar(500) comment '树节点编号',
   Name                 varchar(120) default NULL comment '名称',
   Value                varchar(120) default NULL comment '权限值',
   icon                 varchar(120) default NULL comment '图标',
   url                  varchar(120) default NULL comment '跳转地址',
   Sort                 int default NULL comment '排序',
   PlatformNo           bigint,
   Enabled              int default NULL,
   InputUser            varchar(120) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime,
   primary key (id)
);

alter table ps_menu comment '菜单表';

/*==============================================================*/
/* Table: ps_menu_management                                    */
/*==============================================================*/
create table ps_menu_management
(
   id                   bigint not null auto_increment comment 'ID',
   Name                 varchar(120) default NULL comment '角色名称',
   Description          varchar(512) default NULL comment '描述',
   MenuNo               bigint default NULL comment '权限值',
   ManagementNo         bigint,
   Enabled              int default NULL comment '启用状态',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime,
   primary key (id)
);

alter table ps_menu_management comment '菜单权限关系表';

/*==============================================================*/
/* Table: ps_org_roles                                          */
/*==============================================================*/
create table ps_org_roles
(
   id                   bigint not null auto_increment comment 'ID',
   RolesNo              bigint default NULL comment '角色编号',
   OrgNo                bigint default NULL comment '组织编号',
   ManagementValue      bigint comment '权限值',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table ps_org_roles comment '组织角色关系表';

/*==============================================================*/
/* Table: ps_organization                                       */
/*==============================================================*/
create table ps_organization
(
   id                   bigint not null auto_increment comment 'ID',
   OrgNo                bigint default NULL comment '组织编号',
   ParentNo             bigint default NULL comment '父节点',
   TreeLevelNo          varchar(500) comment '树节点编号',
   Name                 varchar(120) default NULL comment '组织名称',
   Description          varchar(512) comment '描述',
   Category             varchar(50) default NULL comment '分类【集团、公司、部门、工作组】',
   PlatformNo           bigint comment '平台编号',
   Enabled              int default NULL comment '启用状态',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table ps_organization comment '权限组织机构';

/*==============================================================*/
/* Table: ps_platform                                           */
/*==============================================================*/
create table ps_platform
(
   id                   bigint not null auto_increment comment 'ID',
   PlatformNo           bigint default NULL comment '平台编号',
   Name                 varchar(120) default NULL comment '平台名称',
   DefaultUserName      varchar(120) comment '超级管理员名称',
   DefaultUserNo        varchar(120) comment '超级管理员账号',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table ps_platform comment '平台表';

/*==============================================================*/
/* Table: ps_roles                                              */
/*==============================================================*/
create table ps_roles
(
   id                   bigint not null auto_increment comment 'ID',
   RolesNo              bigint comment '角色编号',
   ParentNo             bigint comment '父节点',
   TreeLevelNo          varchar(500) comment '树节点编号',
   Name                 varchar(120) default NULL comment '角色名称',
   Description          varchar(512) default NULL comment '描述',
   ManagementValue      bigint default NULL comment '权限值',
   PlatformNo           int comment '平台编号',
   Enabled              int default NULL comment '启用状态',
   InputUser            varchar(45) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table ps_roles comment '角色表';

/*==============================================================*/
/* Table: ps_user_org                                           */
/*==============================================================*/
create table ps_user_org
(
   id                   bigint not null auto_increment comment 'ID',
   OrgNo                bigint default NULL comment '组织编号',
   UserNo               bigint default NULL comment '用户编号',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table ps_user_org comment '用户组织关系表';

/*==============================================================*/
/* Table: ps_user_roles                                         */
/*==============================================================*/
create table ps_user_roles
(
   id                   bigint not null auto_increment comment 'ID',
   RolesNo              bigint default NULL comment '角色编号',
   UserNo               bigint default NULL comment '用户编号',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table ps_user_roles comment '用户角色关系表';

/*==============================================================*/
/* Table: uc_authorize_blackip                                  */
/*==============================================================*/
create table uc_authorize_blackip
(
   id                   int(11) not null comment '主键',
   Name                 varchar(150) default NULL comment '标题',
   Message              varchar(1500) default NULL comment '内容',
   AuthorizeType        int(11) default NULL comment '类型[0>永久；1》单次；2》循环；3限时]',
   AuthorizeCount       varchar(10) default NULL comment '次数',
   StartDate            date default NULL comment '开始日期',
   EndDate              date default NULL comment '结束日期',
   RepeatWeek           int(11) default NULL comment '重复星期制【1111111】',
   StartTime            time default '00:00:00' comment '开始时间',
   EndTime              time default NULL comment '结束时间',
   StartIP              varchar(50) default NULL comment '开始IP',
   EndIP                varchar(50) default NULL comment '结束IP',
   Enabled              int(11) default 0 comment '状态',
   InputUser            varchar(6) default NULL comment '输入人',
   InputTime            datetime default NULL comment '输入时间',
   primary key (id)
);

alter table uc_authorize_blackip comment '用户黑名单表';

/*==============================================================*/
/* Table: uc_login_log                                          */
/*==============================================================*/
create table uc_login_log
(
   id                   int(11) not null comment '主键',
   Titile               varchar(150) default NULL comment '日志标题',
   Message              varchar(1500) default NULL comment '日志内容',
   LogType              varchar(50) default NULL comment '日志类型',
   RequestUser          varchar(10) default NULL comment '请求者',
   RequestTime          datetime default NULL comment '请求时间',
   HandleTime           datetime default NULL comment '处理时间',
   HandleUser           varchar(10) default NULL comment '处理人',
   HandleResult         int(11) default 0 comment '处理结果',
   HandleMessage        varchar(500) default NULL comment '处理消息',
   Token                varchar(50) default NULL comment '访问口令',
   ExpiresIn            double default NULL comment '过期时间',
   Status               int(11) default 0 comment '状态',
   InputUser            varchar(6) default NULL comment '输入人',
   InputTime            datetime default NULL comment '输入时间',
   primary key (id)
);

alter table uc_login_log comment '用户登录记录表';

/*==============================================================*/
/* Table: uc_users                                              */
/*==============================================================*/
create table uc_users
(
   id                   int(11) not null auto_increment comment '主键\n\n            ',
   UserNo               varchar(6) default NULL comment '用户账号',
   UserName             varchar(20) default NULL comment '用户名',
   Password             varchar(50) default NULL comment '登录密码',
   FullName             varchar(20) default NULL comment '真实名称',
   NickName             varchar(20) default NULL comment '用户昵称',
   IsExpire             bit(1) default NULL comment '是否过期',
   IdentityCard         varchar(18) default NULL comment '身份证号码',
   Phone                varchar(20) default NULL comment '电话',
   Email                varchar(50) default NULL comment '邮箱',
   Address              varchar(200) default NULL comment '居住地址',
   WorkAddress          varchar(200) default NULL comment '工作地址',
   Gender               int(11) default NULL comment '性别【0,1,2来表示，未知,男,女】',
   Birthday             varchar(20) default NULL comment '生日',
   QQ                   varchar(20) default NULL comment 'QQ',
   WeChat               varchar(20) default NULL comment 'WeChat',
   Signature            varchar(200) default NULL comment '个性签名',
   ImageHead            varchar(20) default NULL comment '头像',
   Note                 varchar(200) default NULL comment '备注',
   StatusAudit          int(11) default NULL comment '审核状态',
   InputUser            varchar(6) default NULL comment '创建人',
   InputTime            datetime default NULL comment '创建时间',
   UpdateUser           varchar(6) default NULL comment '更新人',
   UpdateTime           datetime default NULL comment '更新时间',
   Enabled              int(11) default NULL comment '是否启用',
   LastLoginIP          varchar(20) default NULL comment '上次登录IP',
   LastLoginTime        datetime default NULL comment '上次登录时间',
   LastLoginMac         varchar(20) default NULL comment '上次登录MAC地址',
   FailedLoginCount     int(11) default NULL comment '错误登录次数',
   primary key (id)
);

alter table uc_users comment '用户信息表';

/*==============================================================*/
/* Table: uc_users_setting                                      */
/*==============================================================*/
create table uc_users_setting
(
   id                   int(11) not null auto_increment comment 'ID',
   UserNo               varchar(6) default NULL comment '描述',
   AppId                varchar(45) default NULL comment '用户ID',
   AppSecret            varchar(45) default NULL comment '用户密钥',
   ManagementId         int(11) default NULL comment '权限角色ID',
   ManagementValue      double default NULL comment '权限值',
   OrgNo                char(10) default NULL comment '用户所属组织',
   Enabled              tinyint(1) default NULL comment '启用状态',
   InputUser            varchar(45) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   primary key (id)
);

alter table uc_users_setting comment '用户设定表';

/*==============================================================*/
/* Table: vc_record_type                                        */
/*==============================================================*/
create table vc_record_type
(
   id                   bigint not null auto_increment comment 'ID',
   RecordNo             bigint default NULL comment '版本号记录编号',
   ParentRecordNo       bigint default NULL comment '所属版本号',
   TypeNo               bigint default NULL comment '版本类型编号',
   Enabled              int default NULL comment '启用状态',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table vc_record_type comment '版本号映射表';

/*==============================================================*/
/* Table: vc_version_record                                     */
/*==============================================================*/
create table vc_version_record
(
   id                   bigint not null auto_increment comment 'ID',
   RecordNo             bigint default NULL comment '版本号记录编号',
   TypeNo               bigint default NULL comment '类型编码',
   Version              varchar(120) default NULL comment '版本号',
   Description          varchar(512) comment '描述',
   Enabled              int default NULL comment '启用状态',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table vc_version_record comment '版本号记录';

/*==============================================================*/
/* Table: vc_version_type                                       */
/*==============================================================*/
create table vc_version_type
(
   id                   bigint not null auto_increment comment 'ID',
   TypeNo               bigint default NULL comment '组织编号',
   Name                 varchar(120) default NULL comment '类型名称',
   Code                 varchar(50) default NULL comment '类型值',
   Description          varchar(512) comment '描述',
   Enabled              int default NULL comment '启用状态',
   InputUser            varchar(10) default NULL comment '操作员',
   InputTime            datetime default NULL comment '操作时间',
   UpdateTime           datetime comment '更新时间',
   primary key (id)
);

alter table vc_version_type comment '版本类型';

