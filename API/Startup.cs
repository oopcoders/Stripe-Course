using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Data.Entities;
using API.Data.Repositories;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;

namespace API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			StripeConfiguration.ApiKey = Configuration.GetValue<string>("StripeSettings:PrivateKey");
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite(Configuration.GetConnectionString("Default")));

			//If you are using sqlServer
			//services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("Default")));

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
			});

			services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
			services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
			services.Configure<StripeSettings>(Configuration.GetSection("StripeSettings"));

			services.AddAuthentication(cfg =>
			{
				cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
					ValidIssuer = Configuration["Token:Issuer"],
					ValidateIssuer = true,
					ValidateAudience = false,
				};
			});

			services.AddCors(opt =>
			{
				opt.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
			}

			app.UseHttpsRedirection();

			app.UseCors("CorsPolicy");

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();


			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
