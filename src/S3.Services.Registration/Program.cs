﻿using S3.Common.Logging;
using S3.Common.Metrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using S3.Common.Mvc;
using S3.Common.Vault;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;
using System.IO;

namespace S3.Services.Registration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ASP.NET Core 3.0+:
            // The UseServiceProviderFactory call attaches the
            // Autofac provider to the generic hosting mechanism.
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                              //.UseIISIntegration()
                              .UseStartup<Startup>();
            });

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .UseLogging()
        //        .UseVault()
        //        .UseLockbox()
        //        .UseAppMetrics();
    }
}
