﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicrosServices.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            //var options = new ServiceDiscoveryOptions();
            //configuration.Bind("ServiceDiscovery", options);
            //var url = options.Service.GetUrl();
            //return Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
            //        .ConfigureAppConfiguration((hostingContext, builder) =>
            //        {
            //            builder.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            //            .AddJsonFile("configuration.json", false, true);
            //        })
            //        .UseStartup<Startup>()
            //        .UseUrls(options.Service.GetUrl())
            //        .Build();
            return null;
        }
    }
}
