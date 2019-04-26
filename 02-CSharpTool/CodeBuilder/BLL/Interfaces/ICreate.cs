using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.BLL.Interfaces
{
   public interface ICreate
    {
       /// <summary>
       /// 代码生成方法
       /// </summary>
       /// <param name="TableName">表名</param>
       /// <param name="Namespace">项目名</param>
       /// <returns></returns>
       string CreateMethod( string TableName, string Namespace = "SkeFramework");
       /// <summary>
       /// 保存文件
       /// </summary>
       /// <param name="Path">输出路径</param>
       /// <param name="TableName">表名</param>
       /// <param name="Content">文件内容</param>
       /// <param name="Namespace">项目名</param>
       bool SaveFileString(string Path, string TableName, string Content, string Namespace = "SkeFramework");
       /// <summary>
       /// 一键生成
       /// </summary>
       /// <param name="Path">输出路径</param>
       /// <param name="TableName">表名</param>
       /// <param name="Namespace">项目名</param>
       /// <returns></returns>
       bool GenerationCode(string Path, string TableName, string Namespace = "SkeFramework");
    }
}
