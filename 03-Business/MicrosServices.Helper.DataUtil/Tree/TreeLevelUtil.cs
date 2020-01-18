using MicrosServices.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.DataUtil.Tree
{
    /// <summary>
    /// 树节点工具类
    /// </summary>
   public  class TreeLevelUtil
    {
        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ParentNo"></param>
        /// <param name="rootTreeNo"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetTreeLevelNo<T>(T t, long ParentNo, string FieldName= "TreeLevelNo")
        {
            string rootTreeNo = "";
            if (ParentNo != ConstData.DefaultNo)
            {
                if (t != null)
                {
                    Type Ts = t.GetType();
                    object o = Ts.GetProperty(FieldName).GetValue(t, null);
                    string Value = Convert.ToString(o);
                    if (!string.IsNullOrEmpty(Value))
                    {
                        rootTreeNo = Value;
                    }               
                }
            }
            return String.Format("{0}_{1}", rootTreeNo, ParentNo);
        }
    }
}
