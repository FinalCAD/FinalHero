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
            #endregion

            #region BaseServices
            services.AddTransient<IBaseService<City>, BaseService<City, IBaseRepository<City>>>();
            #endregion


            #region Repositories        
            services.AddTransient<ICityRepository, CityRepository>();
            #endregion

            #region Services
            services.AddTransient<ICityService, CityService>();
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


            services.AddAutoMapper(typeof(CityMapper));

            RegisterServicesAndRepositories(services);
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
