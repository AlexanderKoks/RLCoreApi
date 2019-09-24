using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RocketLoopCoreApi.Middlewares;
using RocketLoopCoreApi.Models;
using RocketLoopCoreApi.Repositories;
using RocketLoopCoreApi.Repositories.Interfaces;
using RocketLoopCoreApi.Services;
using RocketLoopCoreApi.Services.Interfaces;

namespace RocketLoopCoreApi
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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<IEnumerable<User>>(new User[] {
                new User { Id = 1, Name = "rl1" },
                new User { Id = 2, Name = "rl2" },
                new User { Id = 3, Name = "rl3" },
                new User { Id = 4, Name = "rl4" }
            });

            services.AddMvc().AddControllersAsServices().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ReplaceMiddleware>();

            app.ApplicationServices.GetServices<IDisposable>();
            app.UseMvc();

        }
    }
}
