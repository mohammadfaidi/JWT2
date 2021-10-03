using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.models;
using WebApplication1.Repo;
using AutoMapper;
using WebApplication1.Configration;
using Microsoft.AspNetCore.Http;
using WebApplication1.MiddleWare;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.JWT;

namespace WebApplication1
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
            /*
            services.AddControllers();


            services.AddSwaggerGen();
            //object singltion  durning all one object post / delete / put -> one object all times  will work 
          services.AddScoped<IUserServ, UserServ>();
            
            //one object each client will delete now work , l2nh ..... 
            // services.AddScoped<IUser, UserServices>();
            //  services.AddTransient<IUser, UserServices>();
            // services.AddAutoMapper(typeof(Startup));
            */
            /*
            services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //   services.AddAutoMapper(typeof(MappingProfile));
            */
            Services.Addservices(services);
            Services.AddDbContext(services, Configuration);
            var tokenKey = Configuration.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(tokenKey));
            // services.AddAutoMapper(typeof(Startup));

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
            app.UseAuthentication();
            app.UseAuthorization();
            //handling exception build in 
            //app.ConfigureBuildInExceptionHandler();

            //handling exception with custom middelware
            app.ConfigureCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); 
            /*
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from the middleware component.");
            });
            */
            //Excetoion Handling
            
            //app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
