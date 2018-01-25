using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Messaging;
using Autofac;
using NATS.Client;
using Core;
using Persistance;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var brokerUrl = this.Configuration["BrokerUrl"]?.ToString();
            
            if (string.IsNullOrEmpty(brokerUrl)) throw new Exception("Missing broker url");
            builder.RegisterType<ConnectionFactory>().AsSelf();
            builder.RegisterType<NatsBus>().AsSelf().WithParameter("brokerUrl", brokerUrl);
            builder.RegisterType<Logging.Logger>().As<Core.Logging.ILog>();

            var templateConnectionString = this.Configuration["ConnectionStrings:TemplateContext"]?.ToString();
            var optionsBuilder = new DbContextOptionsBuilder<TemplateContext>();

            optionsBuilder.UseSqlServer(templateConnectionString);

            builder.Register(c => new TemplateContext(optionsBuilder.Options));

            builder.RegisterType<ValueRepository>().As<IValuesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}