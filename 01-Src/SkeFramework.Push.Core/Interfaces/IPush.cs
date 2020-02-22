using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Push.Interfaces
{
    /// <summary>
    /// 推送接口
    /// </summary>
   public interface IPush
    {
          void SendMessage(string message);
    }
}
