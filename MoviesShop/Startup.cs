using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MoviesShop.Models;
using MoviesShop.Repository;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace MoviesShop
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
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            services.AddAutoMapper(typeof(Startup));
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MoviesShopContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MoviesShopContext")));

            services.AddScoped<UserRepository>();
            services.AddScoped<UserFilm>();
            services.AddScoped<Countrys>();
            services.AddScoped<FilmRepository>();
            services.AddScoped<ActorRepository>();
            services.AddScoped<GenreRepository>();
            services.AddScoped<FilmActor>();
           // services.AddScoped<ServiceUserFilms>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Values Api", Version = "v1" });
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static IMapper ConfigureAutoMapper()
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<DTOUserSmall, User>()
            //             .ForMember(x => x.Cars, c => c.Ignore()).ReverseMap();
            //    cfg.CreateMap<DTOCarSmall, Car>().ReverseMap();
            //});
            //return config.CreateMapper();
            return null;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Values Api V1");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
