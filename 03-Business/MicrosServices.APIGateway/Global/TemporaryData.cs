using MicrosServices.APIGateway.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosServices.APIGateway.Global
{
    /// <summary>
    /// 虚拟数据，模拟从数据库中读取用户
    /// </summary>
    public static class TemporaryData
    {
        private static List<TokenApp> Users = new List<TokenApp>() { new TokenApp { Code = "001", Name = "张三", Password = "111111" }, new TokenApp { Code = "002", Name = "李四", Password = "222222" } };

        public static TokenApp GetUser(string code)
        {
            return Users.FirstOrDefault(m => m.Code.Equals(code));
        }
    }
}
