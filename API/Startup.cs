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
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FinalHero API",
                    Description = "Here a very decriptive description",
                    Contact = new OpenApiContact
                    {
                        Name = ": vincent.chhim@finalcad.com",
                        Email = "vincent.chhim@finalcad.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });



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

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "FinalHero API V1");
            });

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
