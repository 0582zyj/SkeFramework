using MicrosServices.APIGateway.Models;
using MicrosServices.Entities.Common.ApiGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MicrosServices.APIGateway.Services
{
    public interface ITokenHelper
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Token CreateAccessToken(AppToken user);
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Token RefreshToken(ClaimsPrincipal claimsPrincipal);

    }
}
