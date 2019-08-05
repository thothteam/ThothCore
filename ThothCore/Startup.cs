using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using ThothCore.Domain.Models;
using ThothCore.Domain.Persistence.Contexts;
using ThothCore.Domain.Persistence.Repositories;
using ThothCore.Domain.Repositories;
using ThothCore.Domain.Services;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using JavaScriptEngineSwitcher.V8;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ThothCore.Domain.Middleware;


namespace ThothCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string AllowAllOrigins = "_allowAllOrigins";
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			#region auth
			const string singingSecurityKey = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";
			var signingKey = new SigningSymmetricKey(singingSecurityKey);
			services.AddSingleton<IJwtSigningEncodingKey>(signingKey);

			const string jwtShemeName = "JwtBearer";
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = jwtShemeName;
                options.DefaultChallengeScheme = jwtShemeName;
            }).AddJwtBearer(jwtShemeName, jwtBearerOptions=> {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingDecodingKey.GetKey(),
                    ValidateIssuer = true,
                    ValidIssuer = "ThothCore",
                    ValidateAudience = true,
                    ValidAudience = "ThothCoreClient",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    jwtShemeName,
                    jwtShemeName);
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

			#endregion

			#region cors

			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowSpecificOrigins,
					builder =>
					{
						builder.WithOrigins("https://localhost:44353")
							.AllowAnyMethod()
							.AllowAnyHeader()
							.AllowCredentials();
					});
				options.AddPolicy(AllowAllOrigins,
					builder =>
					{
						builder
							.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader()
							.AllowCredentials();
					});
			});

			#endregion

			#region database

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddScoped<ICoursesService, CoursesService>();
			services.AddScoped<ICoursesRepository, CoursesRepository>();

			#endregion
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
	            app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
            }
			//app.UseDefaultFiles(); эта хренотень нужна только для обслуживания файла по умолчанию если нет home/index
			app.UseStaticFiles();

            app.UseMiddleware<JWTInHeaderMiddleware>();

			app.UseAuthentication();

            app.UseCors(MyAllowSpecificOrigins);

			app.UseHttpsRedirection();

            UpdateDatabase<ApplicationDbContext>(app);

            app.UseMvc(routes =>
            {
	            routes.MapRoute(
		            name: "default",
		            template: "{controller=Home}/{action=Index}/{id?}");
            });

		}

        private static void UpdateDatabase<T>(IApplicationBuilder app) where T:DbContext
        {
            using (var serviceScope = app.ApplicationServices
                                          .GetRequiredService<IServiceScopeFactory>()
                                          .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService <T>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
