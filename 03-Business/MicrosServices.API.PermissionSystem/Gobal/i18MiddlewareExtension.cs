using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MicrosServices.API.PermissionSystem.Gobal
{
    public static class I18MiddlewareExtension
    {

      public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app)
     {      
         return app.UseMiddleware<I181ExceptionHandler>();
     }
  
     public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app, StatusCodePagesOptions options)
     {
         return app.UseMiddleware<I181ExceptionHandler>(Options.Create(options));
     }  
     
     public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app, Func<StatusCodeContext, Task> handler)
     {       
         return app.UseStatusCodePages(new StatusCodePagesOptions
         {
             HandleAsync = handler
         });
     }
    }
}
