/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2019/10/11 23:02:48                          */
/*==============================================================*/


drop table if exists authorize_blackip;

drop table if exists log_login;

drop table if exists ps_management;

drop table if exists ps_management_roles;

drop table if exists ps_menu;

drop table if exists ps_menu_roles;

drop table if exists ps_org_roles;

drop table if exists ps_organization;

drop table if exists ps_platform;

drop table if exists ps_roles;

drop table if exists ps_user_org;

drop table if exists ps_user_roles;

drop table if exists users;

drop table if exists users_setting;

/*==============================================================*/
/* Table: authorize_blackip                                     */
/*==============================================================*/
create table authorize_blackip
(
   id                   int(11) not null comment '����',
   Name                 varchar(150) default NULL comment '����',
   Message              varchar(1500) default NULL comment '����',
   AuthorizeType        int(11) default NULL comment '����[0>���ã�1�����Σ�2��ѭ����3��ʱ]',
   AuthorizeCount       varchar(10) default NULL comment '����',
   StartDate            date default NULL comment '��ʼ����',
   EndDate              date default NULL comment '��������',
   RepeatWeek           int(11) default NULL comment '�ظ������ơ�1111111��',
   StartTime            time default '00:00:00' comment '��ʼʱ��',
   EndTime              time default NULL comment '����ʱ��',
   StartIP              varchar(50) default NULL comment '��ʼIP',
   EndIP                varchar(50) default NULL comment '����IP',
   Enabled              int(11) default 0 comment '״̬',
   InputUser            varchar(6) default NULL comment '������',
   InputTime            datetime default NULL comment '����ʱ��',
   primary key (id)
);

/*==============================================================*/
/* Table: log_login                                             */
/*==============================================================*/
create table log_login
(
   id                   int(11) not null comment '����',
   Titile               varchar(150) default NULL comment '��־����',
   Message              varchar(1500) default NULL comment '��־����',
   LogType              varchar(50) default NULL comment '��־����',
   RequestUser          varchar(10) default NULL comment '������',
   RequestTime          datetime default NULL comment '����ʱ��',
   HandleTime           datetime default NULL comment '����ʱ��',
   HandleUser           varchar(10) default NULL comment '������',
   HandleResult         int(11) default 0 comment '������',
   HandleMessage        varchar(500) default NULL comment '������Ϣ',
   Token                varchar(50) default NULL comment '���ʿ���',
   ExpiresIn            double default NULL comment '����ʱ��',
   Status               int(11) default 0 comment '״̬',
   InputUser            varchar(6) default NULL comment '������',
   InputTime            datetime default NULL comment '����ʱ��',
   primary key (id)
);

/*==============================================================*/
/* Table: ps_management                                         */
/*==============================================================*/
create table ps_management
(
   id                   bigint not null auto_increment comment 'ID',
   ManagementNo         bigint,
   ParentNo             bigint default NULL comment '���ڵ�',
   Name                 varchar(120) default NULL comment '����',
   Description          varchar(512),
   Value                bigint default NULL comment 'Ȩ��ֵ',
   Sort                 int(11) default NULL comment '����',
   PlatformNo           int comment 'ƽ̨���',
   Enabled              int default NULL comment '����״̬',
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime comment '����ʱ��',
   primary key (id)
);

alter table ps_management comment 'Ȩ�ޱ�';

/*==============================================================*/
/* Table: ps_management_roles                                   */
/*==============================================================*/
create table ps_management_roles
(
   id                   bigint not null auto_increment comment 'ID',
   RolesNo              bigint default NULL comment '��ɫ���',
   ManagementNo         bigint default NULL comment 'Ȩ�ޱ��',
   ManagementValue      bigint comment 'Ȩ��ֵ',
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime comment '����ʱ��',
   primary key (id)
);

alter table ps_management_roles comment '�û���ɫ��ϵ��';

/*==============================================================*/
/* Table: ps_menu                                               */
/*==============================================================*/
create table ps_menu
(
   id                   bigint not null auto_increment comment 'ID',
   MenuNo               bigint,
   ParentNo             bigint default NULL comment '���ڵ�',
   Name                 varchar(120) default NULL comment '����',
   Value                bigint default NULL comment 'Ȩ��ֵ',
   icon                 varchar(120) default NULL comment 'ͼ��',
   url                  varchar(120) default NULL comment '��ת��ַ',
   Sort                 int(11) default NULL comment '����',
   PlatformNo           int,
   Enabled              int default NULL,
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime,
   primary key (id)
);

alter table ps_menu comment '�˵���';

/*==============================================================*/
/* Table: ps_menu_roles                                         */
/*==============================================================*/
create table ps_menu_roles
(
   id                   bigint not null auto_increment comment 'ID',
   Name                 varchar(120) default NULL comment '��ɫ����',
   Description          varchar(512) default NULL comment '����',
   MenuValue            bigint default NULL comment 'Ȩ��ֵ',
   Enabled              int default NULL comment '����״̬',
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime,
   primary key (id)
);

/*==============================================================*/
/* Table: ps_org_roles                                          */
/*==============================================================*/
create table ps_org_roles
(
   id                   bigint not null auto_increment comment 'ID',
   RolesNo              bigint default NULL comment '��ɫ���',
   OrgNo                bigint default NULL comment '��֯���',
   ManagementValue      bigint comment 'Ȩ��ֵ',
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime comment '����ʱ��',
   primary key (id)
);

alter table ps_org_roles comment '��֯��ɫ��ϵ��';

/*==============================================================*/
/* Table: ps_organization                                       */
/*==============================================================*/
create table ps_organization
(
   id                   bigint not null auto_increment comment 'ID',
   OrgNo                bigint default NULL comment '��֯���',
   ParentNo             bigint default NULL comment '���ڵ�',
   Name                 varchar(120) default NULL comment '��֯����',
   Description          varchar(512) comment '����',
   Category             varchar(50) default NULL comment '���ࡾ���š���˾�����š������顿',
   PlatformNo           bigint comment 'ƽ̨���',
   Enabled              int default NULL comment '����״̬',
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime comment '����ʱ��',
   primary key (id)
);

alter table ps_organization comment 'Ȩ����֯����';

/*==============================================================*/
/* Table: ps_platform                                           */
/*==============================================================*/
create table ps_platform
(
   id                   bigint not null auto_increment comment 'ID',
   PlatformNo           bigint default NULL comment '��ɫ���',
   Name                 varchar(120) default NULL comment 'Ȩ�ޱ��',
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime comment '����ʱ��',
   primary key (id)
);

alter table ps_platform comment 'ƽ̨��';

/*==============================================================*/
/* Table: ps_roles                                              */
/*==============================================================*/
create table ps_roles
(
   id                   bigint not null auto_increment comment 'ID',
   RolesNo              bigint comment '��ɫ���',
   ParentNo             char(10) comment '���ڵ�',
   Name                 varchar(120) default NULL comment '��ɫ����',
   Description          varchar(512) default NULL comment '����',
   ManagementValue      bigint default NULL comment 'Ȩ��ֵ',
   PlatformNo           int comment 'ƽ̨���',
   Enabled              int default NULL comment '����״̬',
   InputUser            varchar(45) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime comment '����ʱ��',
   primary key (id)
);

alter table ps_roles comment '��ɫ��';

/*==============================================================*/
/* Table: ps_user_org                                           */
/*==============================================================*/
create table ps_user_org
(
   id                   bigint not null auto_increment comment 'ID',
   OrgNo                bigint default NULL comment '��֯���',
   UserNo               bigint default NULL comment '�û����',
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime comment '����ʱ��',
   primary key (id)
);

alter table ps_user_org comment '�û���֯��ϵ��';

/*==============================================================*/
/* Table: ps_user_roles                                         */
/*==============================================================*/
create table ps_user_roles
(
   id                   bigint not null auto_increment comment 'ID',
   RolesNo              bigint default NULL comment '��ɫ���',
   UserNo               bigint default NULL comment '�û����',
   InputUser            varchar(10) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateTime           datetime comment '����ʱ��',
   primary key (id)
);

alter table ps_user_roles comment '�û���ɫ��ϵ��';

/*==============================================================*/
/* Table: users                                                 */
/*==============================================================*/
create table users
(
   id                   int(11) not null auto_increment comment '����\n\n            ',
   UserNo               varchar(6) default NULL comment '�û��˺�',
   UserName             varchar(20) default NULL comment '�û���',
   Password             varchar(50) default NULL comment '��¼����',
   FullName             varchar(20) default NULL comment '��ʵ����',
   NickName             varchar(20) default NULL comment '�û��ǳ�',
   IsExpire             bit(1) default NULL comment '�Ƿ����',
   IdentityCard         varchar(18) default NULL comment '���֤����',
   Phone                varchar(20) default NULL comment '�绰',
   Email                varchar(50) default NULL comment '����',
   Address              varchar(200) default NULL comment '��ס��ַ',
   WorkAddress          varchar(200) default NULL comment '������ַ',
   Gender               int(11) default NULL comment '�Ա�0,1,2����ʾ��δ֪,��,Ů��',
   Birthday             varchar(20) default NULL comment '����',
   QQ                   varchar(20) default NULL comment 'QQ',
   WeChat               varchar(20) default NULL comment 'WeChat',
   Signature            varchar(200) default NULL comment '����ǩ��',
   ImageHead            varchar(20) default NULL comment 'ͷ��',
   Note                 varchar(200) default NULL comment '��ע',
   StatusAudit          int(11) default NULL comment '���״̬',
   InputUser            varchar(6) default NULL comment '������',
   InputTime            datetime default NULL comment '����ʱ��',
   UpdateUser           varchar(6) default NULL comment '������',
   UpdateTime           datetime default NULL comment '����ʱ��',
   Enabled              int(11) default NULL comment '�Ƿ�����',
   LastLoginIP          varchar(20) default NULL comment '�ϴε�¼IP',
   LastLoginTime        datetime default NULL comment '�ϴε�¼ʱ��',
   LastLoginMac         varchar(20) default NULL comment '�ϴε�¼MAC��ַ',
   FailedLoginCount     int(11) default NULL comment '�����¼����',
   primary key (id)
);

/*==============================================================*/
/* Table: users_setting                                         */
/*==============================================================*/
create table users_setting
(
   id                   int(11) not null auto_increment comment 'ID',
   UserNo               varchar(6) default NULL comment '����',
   AppId                varchar(45) default NULL comment '�û�ID',
   AppSecret            varchar(45) default NULL comment '�û���Կ',
   ManagementId         int(11) default NULL comment 'Ȩ�޽�ɫID',
   ManagementValue      double default NULL comment 'Ȩ��ֵ',
   OrgNo                char(10) default NULL comment '�û�������֯',
   Enabled              tinyint(1) default NULL comment '����״̬',
   InputUser            varchar(45) default NULL comment '����Ա',
   InputTime            datetime default NULL comment '����ʱ��',
   primary key (id)
);

