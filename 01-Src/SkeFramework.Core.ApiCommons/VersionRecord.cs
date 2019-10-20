using System;

namespace SkeFramework.Core.ApiCommons
{
    public class VersionRecord
    {
        //(1) 异常/错误处理
        //(2) HTTP 严格传输安全协议
        //(3) HTTPS 重定向
        //(4) 静态文件服务器
        //(5) Cookie 策略实施
        //(6) 身份验证
        //(7) 会话
        //(8) MVC
        //(1) Use[Middleware] 中间件负责调用管道中的下一个中间件，也可使管道短路（即不调用 next 请求委托)。
        //(2) Run[Middleware] 是一种约定，一些中间件组件可能会公开在管道末端运行的Run[Middleware] 方法。
        //(3) Map扩展用作约定来创建管道分支, Map* 创建请求管道分支是基于给定请求路径的匹配项
    }
}
