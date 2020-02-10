using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.APIGateway.Global;
using MicrosServices.APIGateway.Services;

namespace MicrosServices.APIGateway.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : Controller
    {
        private ITokenHelper tokenHelper = null;
        public TokenController(ITokenHelper _tokenHelper)
        {
            tokenHelper = _tokenHelper;
        }
        [HttpGet]
        public IActionResult Get(string code, string pwd)
        {
            TokenApp user = TemporaryData.GetUser(code);
            if (null != user && user.Password.Equals(pwd))
            {
                return Ok(tokenHelper.CreateToken(user));
            }
            return BadRequest();
        }
    }
}