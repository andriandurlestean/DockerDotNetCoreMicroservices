using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using RawRabbit.vNext.Disposable;
using TestWebApi.Context;
using TestWebApi.Models;

namespace TestWebApi
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
            var connection = @"Server=db;Database=TestWebApi;User=sa;Password=QAZwsx123_longone;";

            services.AddDbContext<TestWebApiDbContext>(
                options => options.UseSqlServer(connection));

            services.AddMvc();

            var busConfig = new RawRabbitConfiguration
            {
                Username = "guest",
                Password = "guest",
                Port = 5672,
                VirtualHost = "/",
                Hostnames = { "rabbitmq" }
            };
            var busClient = BusClientFactory.CreateDefault(busConfig);

            services.AddSingleton<IBusClient>(busClient);

            /*            services.AddSingleton<EventBus.RabbitMQ.Interfaces.IRabbitMQPersistentConnection>(sp =>
                        {
                            var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

                            var factory = new ConnectionFactory()
                            {
                                HostName = "testwebapi-rabbitmq"
                            };

                            var retryCount = 5;

                            return new RabbitMQPersistentConnection(factory, logger, retryCount);
                        });

                        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                        {
                            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                            var retryCount = 5;

                            return new EventBusRabbitMQ(rabbitMQPersistentConnection, iLifetimeScope, eventBusSubcriptionsManager, logger, "TestWebApi", retryCount);
                        });

                        services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();*/
            //services.AddTransient<ProductPriceChangedIntegrationEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TestWebApiDbContext>())
                {
                    context.Database.Migrate();
                    if (!context.Catalogs.Any())
                    {
                        context.Catalogs.Add(new Catalog
                        {
                            Id = Guid.NewGuid(),
                            Name = "Test1",
                            IsComplete = true
                        });
                        context.Catalogs.Add(new Catalog
                        {
                            Id = Guid.NewGuid(),
                            Name = "Test2",
                            IsComplete = false
                        });
                        context.SaveChanges();
                    }
                }
            }

           

            //var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
                //eventBus.Subscribe<OrderStatusChangedToStockConfirmedIntegrationEvent, OrderStatusChangedToStockConfirmedIntegrationEventHandler>();
            }
    }
}
