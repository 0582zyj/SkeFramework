using SkeFramework.NetConnectionPool.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.ConnService
{

    /// <summary>
    /// 连接池中的一个连接结构
    /// </summary>
    public class ConnStruct : IDisposable
    {
        #region 私有属性部分
        //--------------------------------------------------------------------
        private bool enable = true;//是否失效
        private bool use = false;//是否正在被使用中
        private bool allot = true;//表示该连接是否可以被分配
        private DateTime createTime = DateTime.Now;//创建时间
        private DateTime lastRunTime = DateTime.Now;//上一次使用时间
        private int useDegree = 0;//被使用次数
        private int repeatNow = 0;//当前连接被重复引用多少
        private bool isRepeat = true;//连接是否可以被重复引用，当被分配出去的连接可能使用事务时，该属性被标识为true
        private ConnTypeEnum connType = ConnTypeEnum.None;//连接类型
        private DbConnection connect = null;//连接对象
        private object obj = null;//连接附带的信息
        private List<string> repeatKeyList = null;//当前使用该链接的Key列表
        #endregion

        #region 公共属性部分
        /// <summary>
        /// 表示该连接是否可以被分配
        /// </summary>
        public bool Allot
        {
            get { return allot; }
            set { allot = value; }
        }
        /// <summary>
        /// 是否失效；false表示失效，只读
        /// </summary>
        public bool Enable
        { get { return enable; } }
        /// <summary>
        /// 是否正在被使用中，只读
        /// </summary>
        public bool IsUse
        { get { return use; } }
        /// <summary>
        /// 创建时间，只读
        /// </summary>
        public DateTime CreateTime
        { get { return createTime; } }
        /// <summary>
        /// 上一次使用时间，
        /// </summary>
        public DateTime LastRunTime
        {
            get { return lastRunTime; }
            set { this.lastRunTime = value; }
        }
        /// <summary>
        /// 被使用次数，只读
        /// </summary>
        public int UseDegree
        { get { return useDegree; } }
        /// <summary>
        /// 当前连接被重复引用多少，只读
        /// </summary>
        public int RepeatNow
        { get { return repeatNow; } }
        /// <summary>
        /// 得到数据库连接状态，只读
        /// </summary>
        public ConnectionState State
        { get { return connect.State; } }
        /// <summary>
        /// 得到该连接，只读
        /// </summary>
        public DbConnection Connection
        { get { return connect; } }
        /// <summary>
        /// 连接是否可以被重复引用
        /// </summary>
        public bool IsRepeat
        {
            get { return isRepeat; }
            set { isRepeat = value; }
        }
        /// <summary>
        /// 连接类型，只读
        /// </summary> 
        public ConnTypeEnum ConnType
        { get { return connType; } }
        /// <summary>
        /// 连接附带的信息
        /// </summary>
        public object Obj
        {
            get { return obj; }
            set { obj = value; }
        }
        /// <summary>
        /// 当前使用该链接的Key列表
        /// </summary>
        public List<string> RepeatKeyList
        {
            get { return repeatKeyList; }
            set { repeatKeyList = value; }
        }
        #endregion

        #region  构造函数
        /// <summary>
        /// 连接池中的连接
        /// </summary>
        /// <param name="dbc">数据库连接</param>
        /// <param name="cte">连接类型</param>
        public ConnStruct(DbConnection dbc, ConnTypeEnum cte)
        {
            createTime = DateTime.Now;
            connect = dbc;
            connType = cte;
            obj = Guid.NewGuid();
            repeatKeyList = new List<string>();
        }
        /// <summary>
        /// 连接池中的连接
        /// </summary>
        /// <param name="dt">连接创建时间</param>
        /// <param name="dbc">数据库连接</param>
        /// <param name="cte">连接类型</param>
        public ConnStruct(DbConnection dbc, ConnTypeEnum cte, DateTime dt)
        {
            createTime = dt;
            lastRunTime = DateTime.Now;
            connect = dbc;
            connType = cte;
            obj = Guid.NewGuid();
            repeatKeyList = new List<string>();
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        { connect.Open(); }
        /// <summary>
        /// 关闭数据库连接 
        /// </summary>
        public void Close()
        { connect.Close(); }
        /// <summary>
        /// 无条件将连接设置为失效
        /// </summary>
        public void SetConnectionLost()
        { enable = false; allot = false; }
        /// <summary>
        /// 被分配出去，线程安全的
        /// </summary>
        public void Repeat(Object RequestKey)
        {
            lock (this)
            {
                if (enable == false)//判断连接是否可用
                    throw new ResLostnExecption();//连接资源已经失效
                if (allot == false)//判断连接是否可以被分配
                    throw new AllotExecption();//连接资源不可以被分配
                if (use == true && isRepeat == false) //判断连接是否已经被分配并且不允许重复引用
                    throw new AllotAndRepeatExecption();//连接资源已经被分配并且不允许重复引用
                repeatNow++;//引用记数+1
                useDegree++;//被使用次数+1
                use = true;//被使用
                if (!repeatKeyList.Contains(RequestKey.ToString()))
                {
                    repeatKeyList.Add(RequestKey.ToString());
                }
            }
        }
        /// <summary>
        /// 被释放回来，线程安全的
        /// </summary>
        public void Remove(Object RequestKey)
        {
            lock (this)
            {
                if (enable == false)//连接可用
                    throw new ResLostnExecption();//连接资源已经失效
                if (repeatNow == 0)
                    throw new RepeatIsZeroExecption();//引用记数已经为0
                repeatNow--;//引用记数-1
                if (repeatNow == 0)
                    use = false;//未使用
                else
                    use = true;//使用中
                if (repeatKeyList.Contains(RequestKey.ToString()))
                {
                    repeatKeyList.Remove(RequestKey.ToString());
                }
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            enable = false;
            connect.Close();
            connect = null;
        }
        /// <summary>
        /// 连接信息
        /// </summary>
        /// <returns></returns>
        public string ToConnString()
        {
            return string.Format(@"Connection Type:{0},Key:{1},enable{2},use{3},allot:{4},createTime:{5},lastRunTime:{6},useDegree:{7},repeatNow:{8},isRepeat:{9}"
                , this.connType.ToString(), this.Obj.ToString(), this.enable.ToString(), this.use.ToString(),
                this.allot.ToString(), this.createTime.ToString(), this.lastRunTime.ToString(),
                this.useDegree.ToString(), this.repeatNow.ToString(), this.isRepeat.ToString());
        }
        #endregion
    }
}
