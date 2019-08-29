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
using S3.Services.Registration.Subjects.Commands;

namespace S3.Services.Registration
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.docker.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
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

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            //    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq();
            builder.AddDispatchers();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IStartupInitializer initializer,
            IConsulClient consulClient, IRegistrationDbInitialiser dbInitialiser)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
                // Initialise the database
                try
                {
                    if (Configuration.GetSection("seedDatabase").Value == "true")
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
                 //.SubscribeEvent<SchoolCreatedEvent>(@namespace: "registration")
                 .SubscribeCommand<CreateTeacherCommand>(onError: (cmd, ex)
                    => new CreateTeacherRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_create_teacher"))
                .SubscribeCommand<UpdateTeacherCommand>(onError: (cmd, ex)
                    => new UpdateTeacherRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_update_teacher"))
                 .SubscribeCommand<DeleteTeacherCommand>(onError: (cmd, ex)
                    => new DeleteTeacherRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_teacher"))
                //.SubscribeEvent<TeacherCreatedEvent>(@namespace: "registration")
                .SubscribeCommand<CreateStudentCommand>(onError: (cmd, ex)
                    => new CreateStudentRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_create_student"))
                .SubscribeCommand<UpdateStudentCommand>(onError: (cmd, ex)
                    => new UpdateStudentRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_update_student"))
                 .SubscribeCommand<DeleteStudentCommand>(onError: (cmd, ex)
                    => new DeleteStudentRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_student"))
                //.SubscribeEvent<StudentCreatedEvent>(@namespace: "registration")
                .SubscribeCommand<CreateParentCommand>(onError: (cmd, ex)
                    => new CreateParentRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_create_parent"))
                .SubscribeCommand<UpdateParentCommand>(onError: (cmd, ex)
                    => new UpdateParentRejectedEvent(cmd.LastName + " " + cmd.FirstName, ex.Message, "unable_to_update_parent"))
                 .SubscribeCommand<DeleteParentCommand>(onError: (cmd, ex)
                    => new DeleteParentRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_parent"))
                //.SubscribeEvent<ParentCreatedEvent>(@namespace: "registration")
                .SubscribeCommand<CreateClassCommand>(onError: (cmd, ex)
                    => new CreateClassRejectedEvent(cmd.Name, ex.Message, "unable_to_create_class"))
                .SubscribeCommand<UpdateClassCommand>(onError: (cmd, ex)
                    => new UpdateClassRejectedEvent(cmd.Name, ex.Message, "unable_to_update_class"))
                 .SubscribeCommand<DeleteClassCommand>(onError: (cmd, ex)
                    => new DeleteClassRejectedEvent(cmd.Id.ToString(), ex.Message, "unable_to_delete_class"))
                //.SubscribeEvent<ClassCreatedEvent>(@namespace: "registration")
                .SubscribeCommand<CreateSubjectCommand>()
                .SubscribeCommand<UpdateSubjectCommand>()
                 .SubscribeCommand<DeleteSubjectCommand>()
                //.SubscribeEvent<SubjectCreatedEvent>(@namespace: "registration")

            ;

            var serviceId = app.UseConsul();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
                Container.Dispose();
            });

            await initializer.InitializeAsync();
        }
    }
}
