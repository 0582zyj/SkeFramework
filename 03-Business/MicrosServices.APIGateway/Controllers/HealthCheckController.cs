using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicrosServices.APIGateway.Controllers
{
    /// <summary>
    /// 心跳检查
    /// </summary>
    [Produces("application/json")]
    [Route("api/HealthCheck/[action]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            //Logger nlog = LogManager.GetCurrentClassLogger();
            //LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, "consul", "心跳检测");
            //nlog.Log(logEvent);
            return Ok();
        }
    }
}