using API.Mappers;
using AutoMapper;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DAL.Context;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Middlewares;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void RegisterServicesAndRepositories(IServiceCollection services)
        {
            services.AddTransient<ICityService , CityService>();
            services.AddTransient<IHeroService , HeroService>();
            services.AddTransient<IPowerService, PowerService>();
            services.AddTransient<IHeroPowerService, HeroPowerService>();
            services.AddTransient<ICityRepository , CityRepository>();
            services.AddTransient<IHeroRepository , HeroRepository>();
            services.AddTransient<IPowerRepository, PowerRepository>();
            services.AddTransient<IHeroPowerRepository, HeroPowerRepository>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppContext>(o =>
            {
                var databaseConnectionString = Configuration["Database"];
                o.UseNpgsql(databaseConnectionString);
            });
            services.AddAutoMapper(typeof(CityMapper),
                                    typeof(HeroMapper),
                                    typeof(PowerMapper));
            RegisterServicesAndRepositories(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseMiddleware(typeof(ErrorMiddleware));
            }
            else
            {
                app.UseMiddleware(typeof(ErrorMiddleware));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
