﻿using System;
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
using S3.Common.Authentication;
using S3.Common.Mvc;
using S3.Common.RabbitMq;
using S3.Common.Redis;
using S3.Common.RestEase;
using S3.Common.Swagger;
using OpenTracing;
using S3.Services.Registration.Schools.Commands;
using S3.Services.Registration.Schools.Events;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using S3.Services.Registration.Schools.Commands.Validators;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Teachers.Commands;
using S3.Services.Registration.Teachers.Events;
using S3.Services.Registration.Students.Commands;
using S3.Services.Registration.Students.Events;
using AutoMapper;
using S3.Services.Registration.Utility;
using S3.Services.Registration.Parents.Commands;
using S3.Services.Registration.Parents.Events;
using S3.Services.Registration.Classes.Commands;
using S3.Services.Registration.Classes.Events;
using S3.Services.Registration.ExternalEvents;
using S3.Services.Registration.ScoresEntryTasks.Commands;
using Microsoft.Extensions.Hosting;

namespace S3.Services.Registration
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        //public IContainer Container { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
           
            // Add DbContext using SQL Server Provider
            services.AddDbContext<RegistrationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("registration-service-db")));
            services.AddCustomMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()); // Enable fluent validation;
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJwt();
            services.AddOpenTracing();
            services.AddRedis();
            //services.AddJaeger(); // Throwing exception
            //services.RegisterServiceForwarder<IOrdersService>("orders-service");
            services.AddScoped<IRegistrationDbInitialiser, RegistrationDbInitialiser>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
            //        .AsImplementedInterfaces();
            ////builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            ////    .AsImplementedInterfaces();
            //builder.Populate(services);
            //builder.AddRabbitMq();
            //builder.AddDispatchers();

            //Container = builder.Build();

            //return new AutofacServiceProvider(Container);
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            //builder.RegisterModule(new MyApplicationModule());

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                   .AsImplementedInterfaces();
            builder.AddRabbitMq();
            builder.AddDispatchers();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IStartupInitializer initializer,
            IConsulClient consulClient, IRegistrationDbInitialiser dbInitialiser)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
                // Initialise the database
                try
                {
                    if (Configuration.GetValue<string>("database:seed").ToLowerInvariant() == "true")
                        dbInitialiser.Initialise();
                }
                catch (Exception)
                {
                    //Log.Error("Error, {Name}", ex.ToString(), LogEventLevel.Error);
                }
            }

            app.UseMvc();
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAccessTokenValidator();
            app.UseServiceId();
            app.UseRabbitMq()
                .SubscribeCommand<CreateSchoolCommand>(onError: (cmd, ex)
                    => new CreateSchoolRejectedEvent(cmd.Name, ex.Message, "school_name_already_exists"))
                .SubscribeCommand<UpdateSchoolCommand>(onError: (cmd, ex)
                    => new UpdateSchoolRejectedEvent(cmd.Name, ex.Message, "school_name_already_exists"))
                 .SubscribeCommand<DeleteSchoolCommand>(onError: (cmd, ex)
                    => new DeleteSchoolRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_school"))
                 .SubscribeCommand<CreateTeacherCommand>(onError: (cmd, ex)
                    => new CreateTeacherRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_create_teacher"))
                .SubscribeCommand<UpdateTeacherCommand>(onError: (cmd, ex)
                    => new UpdateTeacherRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_update_teacher"))
                 .SubscribeCommand<DeleteTeacherCommand>(onError: (cmd, ex)
                    => new DeleteTeacherRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_teacher"))
                .SubscribeCommand<CreateStudentCommand>(onError: (cmd, ex)
                    => new CreateStudentRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_create_student"))
                .SubscribeCommand<UpdateStudentCommand>(onError: (cmd, ex)
                    => new UpdateStudentRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_update_student"))
                 .SubscribeCommand<DeleteStudentCommand>(onError: (cmd, ex)
                    => new DeleteStudentRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_student"))
                .SubscribeCommand<CreateParentCommand>(onError: (cmd, ex)
                    => new CreateParentRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_create_parent"))
                .SubscribeCommand<UpdateParentCommand>(onError: (cmd, ex)
                    => new UpdateParentRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_update_parent"))
                 .SubscribeCommand<DeleteParentCommand>(onError: (cmd, ex)
                    => new DeleteParentRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_parent"))
                .SubscribeCommand<CreateClassCommand>(onError: (cmd, ex)
                    => new CreateClassRejectedEvent(cmd.Name, ex.Message, "unable_to_create_class"))
                .SubscribeCommand<UpdateClassCommand>(onError: (cmd, ex)
                    => new UpdateClassRejectedEvent(cmd.Name, ex.Message, "unable_to_update_class"))
                 .SubscribeCommand<DeleteClassCommand>(onError: (cmd, ex)
                    => new DeleteClassRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_class"))
                .SubscribeCommand<CreateScoresEntryTaskCommand>()
                .SubscribeCommand<UpdateScoresEntryTaskCommand>()
                .SubscribeCommand<DeleteScoresEntryTaskCommand>()
                 .SubscribeEvent<SignedUpEvent>(@namespace: "identity")
                .SubscribeEvent<SignUpRemovedEvent>(@namespace: "identity")
                .SubscribeEvent<UserRolesUpdatedEvent>(@namespace: "identity")

            ;

            var serviceId = app.UseConsul();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
                //Container.Dispose();
            });

            await initializer.InitializeAsync();
        }
    }
}
