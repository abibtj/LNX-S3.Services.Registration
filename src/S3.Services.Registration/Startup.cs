using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using S3.Common;
using S3.Common.Consul;
using S3.Common.Dispatchers;
using S3.Common.Jaeger;
using S3.Common.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using S3.Common.Mvc;
using S3.Common.RabbitMq;
using S3.Common.RestEase;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Metrics;
using S3.Services.Registration.Services;
using OpenTracing;
using S3.Services.Registration.Schools.Commands;
using S3.Services.Registration.Schools.Events;

namespace S3.Services.Registration
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.AddConsul();
            services.AddJaeger();
            services.RegisterServiceForwarder<IOrdersService>("orders-service");
            services.AddTransient<IMetricsRegistry, MetricsRegistry>();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddMongo();
            builder.AddMongoRepository<School>("Schools");
            builder.AddRabbitMq();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IStartupInitializer initializer,
            IConsulClient consulClient)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            initializer.InitializeAsync();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateSchoolCommand>(onError: (cmd, ex)
                    => new CreateSchoolRejectedEvent(cmd.Name, ex.Message, "school_name_already_exists"))
                //.SubscribeEvent<CustomerCreated>(@namespace: "customers")
                //.SubscribeEvent<OrderCompleted>(@namespace: "orders")
                ;
            var serviceId = app.UseConsul();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
                Container.Dispose();
            });
        }
    }
}
