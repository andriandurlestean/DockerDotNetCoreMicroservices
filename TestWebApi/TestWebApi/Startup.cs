using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        }
    }
}
