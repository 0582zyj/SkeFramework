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
        public const string TreeSplit = "_";
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
            return getTreeLevelVo(rootTreeNo,ParentNo);
        }

        public static string getTreeLevelVo(string parentTreeLevel, long ParentNo)
        {
            return String.Format("{0}_{1}", parentTreeLevel, ParentNo);
        }
        /// <summary>
        /// 提取父节点路径
        /// </summary>
        /// <param name="TreeLevel"></param>
        /// <returns></returns>
        public static List<long> GetParentNos(string TreeLevel)
        {
            string[] treeLevelList =TreeLevel.Split(new string[1] { TreeSplit }, StringSplitOptions.None);
            return treeLevelList.Where(o =>!String.IsNullOrEmpty(o) && o != "-1").Select(o => Convert.ToInt64(o)).ToList();
        }
    }
}
