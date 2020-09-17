using API.Infrastructure.Mappers;
using AutoMapper;
using BusinessLogic.DTOs.Responses;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            #region Base Repositories        
            services.AddTransient<IBaseRepository<City>, BaseRepository<AppContext, City>>();
            services.AddTransient<IBaseRepository<Hero>, BaseRepository<AppContext, Hero>>();
            services.AddTransient<IBaseRepository<HeroPower>, BaseRepository<AppContext, HeroPower>>();
            services.AddTransient<IBaseRepository<Power>, BaseRepository<AppContext, Power>>();
            #endregion

            #region BaseServices
            services.AddTransient<IBaseService<City>, BaseService<City, IBaseRepository<City>>>();
            services.AddTransient<IBaseService<Hero>, BaseService<Hero, IBaseRepository<Hero>>>();
            services.AddTransient<IBaseService<HeroPower>, BaseService<HeroPower, IBaseRepository<HeroPower>>>();
            services.AddTransient<IBaseService<Power>, BaseService<Power, IBaseRepository<Power>>>();
            #endregion


            #region Repositories        
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IHeroRepository, HeroRepository>();
            services.AddTransient<IHeroPowerRepository, HeroPowerRepository>();
            services.AddTransient<IPowerRepository, PowerRepository>();

            #endregion

            #region Services
            services.AddTransient<IHeroService, HeroService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IPowerService, PowerService>();
            services.AddTransient<IHeroPowerService, HeroPowerService>();
            #endregion
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

            // automapper
            services.AddAutoMapper(
                typeof(CityMapper),
                typeof(HeroMapper),
                typeof(HeroPowerMapper),
                typeof(PowerMapper)
                );


            RegisterServicesAndRepositories(services);


            //versioning
            services.AddApiVersioning(conf =>
            {
                conf.DefaultApiVersion = new ApiVersion(1, 0);
                conf.AssumeDefaultVersionWhenUnspecified = true;
                conf.ReportApiVersions = true;

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
