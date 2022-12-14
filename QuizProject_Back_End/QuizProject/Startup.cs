using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizProject.Models;
using QuizProject.Models.AppData;
using QuizProject.Services;
using QuizProject.Servieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appData.json");
            AppConfig = builder.Build();
            App = AppConfig.Get<App>();

            Configuration = configuration;
        }
        public App App { get; }

        public IConfiguration Configuration { get; }
        public IConfiguration AppConfig { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<App>(AppConfig);
            services.AddDbContext<QuizContext>(op => op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequiredLength = 5;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<QuizContext>().AddDefaultTokenProviders();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //TODO: see my comment in EmailService
                    
                    ValidIssuer = App.Jwt.Issuer,
                    ValidAudience = App.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(App.Jwt.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITestLogic, TestLogic>();
            services.AddScoped<IDataTransferServise, DataTransferService>();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddCors(c => c.AddPolicy("AllowOrigin", opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, QuizContext context)
        {
            

            //TODO: If Db not exists inti here db migration to create it and fill with data
            //DONE
            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (!context.Database.CanConnect())
            {
                context.Database.Migrate();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
