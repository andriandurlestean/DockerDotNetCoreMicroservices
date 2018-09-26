using System;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using RawRabbit.vNext.Disposable;

namespace TestWebApi2
{
    public class Startup
    {
        private static IConnection copnnection;
        private static IModel channel;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var busConfig = new RawRabbitConfiguration
            {
                Username = "guest",
                Password = "guest",
                Port = 5672,
                VirtualHost = "/",
                Hostnames = { "rabbitmq" },
                
            };
            var busClient = BusClientFactory.CreateDefault(busConfig);

            services.AddSingleton<IBusClient>(busClient);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();


            //app.
            var client = provider.GetService<RawRabbit.vNext.Disposable.IBusClient>();


            client.RespondAsync<BasicMessage, BasicResponse>(
                async (msg, context) =>
                    new BasicResponse {Result = "TEST " + msg.Prop + " " + DateTime.UtcNow.ToShortTimeString()},
                ctx =>
                {
                    ctx.WithExchange(ex => ex.WithName("testwebapi").WithAutoDelete(false).WithDurability(true));
                    ctx.WithQueue(queue =>
                        queue.WithName("testwebapiqueue").WithExclusivity(false).WithDurability(true)
                            .WithAutoDelete(false));
                });
        }
    }

}
