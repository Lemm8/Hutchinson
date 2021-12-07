using AuthApiExample.Interfaces;
using AuthApiExample.Repositories;
using AuthApiExample.Services;
using AuthApiExample.Sistema6sData;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApiExample
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
            services.AddHttpContextAccessor();
            services.AddCors();

            services.AddCors();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<Sistema6SContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Hutchinson"))
            );


            var jwtConfiguration = Configuration.GetSection("JwtConfiguration");
            services.Configure<JwtConfiguration>(jwtConfiguration);

            var jwt = jwtConfiguration.Get<JwtConfiguration>();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Secret));

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
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                };

                x.Events = new JwtBearerEvents
                {
             
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddMvc();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuditoriasRepository, AuditoriasRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());


            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
