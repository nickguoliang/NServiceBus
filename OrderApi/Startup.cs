using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NServiceBus;

namespace OrderApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var endpointConfiguration = new EndpointConfiguration(endpointName: "OrderAPI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Order");
            IEndpointInstance endpointInstance = Endpoint.Start(endpointConfiguration).Result;
            builder.RegisterInstance(endpointInstance).As<IEndpointInstance>().PropertiesAutowired();
            //TODO: Register        
            this.ApplicationContainer = builder.Build();
            
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
