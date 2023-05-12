using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
//using JobEngine.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using MockWebServiceRepositories.Repositories;
using PriceUpdateRepository;
using PriceUpdateRepository.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Workers.Config;
//using Workers.Jobs;
//using SalesOrderWebService.Repositories;
//using SalesOrderWebService.Config;
using Core.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using PriceUpdateRepository.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Logging;
using ArasPLMWebAp.Models;
using DocumentFormat.OpenXml.Spreadsheet;
namespace ArasPLMWebAp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected UserDTO _user { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<ArasApiConfig>(Configuration.GetSection("ArasApiConfig"));
            //services.Configure<SalesOrderWebServiceConfig>(Configuration.GetSection("SalesOrderWebServiceConfig"));
            services.Configure<ViewOrderConfig>(Configuration.GetSection("ViewOrderConfig"));
            services.Configure<DestinationBlobConfig>(Configuration.GetSection("DestinationBlobConfig"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews()
                .AddSessionStateTempDataProvider()
                .AddMicrosoftIdentityUI();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            IdentityModelEventSource.ShowPII = true;

            //services.AddTransient<IOrderRepository, SalesOrderRepository>();
            //services.AddTransient<IAlertRepository, SalesOrderRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, AppAuthenticationHandler>();
            services.AddTransient<IUsersAdminServises, UsersAdminServises>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IPasswordHasher<PriceUpdateRepository.DataModels.UserModel>, PasswordHasher<PriceUpdateRepository.DataModels.UserModel>>();

            services.AddTransient<TokenService, TokenService>();
            var azureAdConfiguration = Configuration.GetSection("AzureAd").Get<AzureAdConfigOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                })
                .AddMicrosoftIdentityWebApp(
                options =>
                {
                    Configuration.Bind("AzureAd", options);
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Events.OnRedirectToIdentityProvider = async (redirectContext) =>
                    {
                        //if (!redirectContext.Request.Path.Value.ToLower().Contains("Home/AzureADAuth".ToLower()))
                        //{
                        //    redirectContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //    redirectContext.HttpContext.Response.Headers["Location"] = "Home/Index";
                        //    redirectContext.HandleResponse();
                        //}
                    };
                    options.SaveTokens = true;
                });


            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme,
                    OpenIdConnectDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
            services.Configure<FormOptions>(x => x.ValueCountLimit = int.MaxValue);
            //services.AddHostedService<JobsService>();
            services.AddDbContext<Context>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            //services.AddDbContext<ExternalDbContext>(
            //   options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection1"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/UsersRegistration/AccountIsBlocked");
            });
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Processor}/{action=Process}");
            });
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    _user = HttpContext.GetUserDTO();
            //    if (_user == null)
            //    {
            //        _user = _userService.GetCurrentUser();
            //        HttpContext.AddUserDTO(_user);

            //        if (_user != null)
            //        {
            //            HttpContext.Session.SetString("UserId", _user.Id.ToString());
            //            HttpContext.Session.SetString("UserName", _user.UserName);
            //            HttpContext.Session.SetString("AcceptedFlag", _user.Accepted);
            //            HttpContext.Session.SetString("Language", _user.Language.ToString());
            //            HttpContext.Session.SetString("UserRole", _user.Role.ToString());
            //            HttpContext.Session.SetString("FirstName", _user.FirstName.ToString());
            //            HttpContext.Session.SetString("LastName", _user.LastName.ToString());
            //            //HttpContext.Session.SetString("GenericID", _user.GenericID);

            //        }

            //    }
            //}
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
                context.Database.Migrate();
                var adminUsers = new List<PriceUpdateRepository.DataModels.UserModel>()
                {
                    new PriceUpdateRepository.DataModels.UserModel {FirstName="Manikandan",SecondName = "Chellakani" , Email="mchellakani@armstrongfluidtechnology.com",UserName="mchellakani@armstrongfluidtechnology.com",UserRole=Core.Enums.UserRoles.Admin,Accepted="Not_Accepted", Language =Core.Enums.Language.English, GenericID = "arascanada@armstrongfluidtechnology.com"},
                    new PriceUpdateRepository.DataModels.UserModel {FirstName="Manikandan",SecondName = "Chellakani" , Email="mani@shrimantech.com",UserName="mani@shrimantech.com",UserRole=Core.Enums.UserRoles.Admin,Accepted="Not_Accepted", Language =Core.Enums.Language.English, GenericID = "arascanada@armstrongfluidtechnology.com"},
                    new PriceUpdateRepository.DataModels.UserModel {FirstName="Balaji",SecondName = "TGI" , Email="shadowwsbalajik@gmail.com",UserName="shadowwsbalajik@gmail.com",UserRole=Core.Enums.UserRoles.Admin,Accepted="Not_Accepted",Language =Core.Enums.Language.English, GenericID = "shadowwsbalajik@gmail.com"}
                };
                var adminUsernames = adminUsers.Select(x => x.UserName);
                var existingUsers = context.Users.Where(x => adminUsernames.Any(y => y == x.UserName)).ToList();
                foreach (var adminUser in adminUsers)
                {
                    if (!existingUsers.Any(x => x.UserName == adminUser.UserName))
                    {
                        //context.Users.Add(adminUser);
                    }
                }

                var adminArasLoginDetails = new List<PriceUpdateRepository.DataModels.ArasLoginDetailsModel>()
                {
                    new PriceUpdateRepository.DataModels.ArasLoginDetailsModel { ArasUserID="arascanada@armstrongfluidtechnology.com", ArasUserName = "arascanada", ArasPassword="Armstrong@123$", BRDSpec="1001 – SAA", DateTime = DateTime.Now}
                };

                context.SaveChanges();
            }
        }
    }
}
