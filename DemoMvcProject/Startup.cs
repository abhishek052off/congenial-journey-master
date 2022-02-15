using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMvcProject.DAL;
using DemoMvcProject.Models;
using EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoMvcProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment environment {get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) //Dependency 
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            string conn;
            if(environment.IsDevelopment()){
                conn=Configuration.GetConnectionString("schoolConnectionDev");
            }
            else{   
                conn = Configuration.GetConnectionString("schoolConnection");
            }
           
            services.AddDbContext<SchoolContext>(options => options.UseMySql(conn  , new MySqlServerVersion(new Version(8, 0, 27)    )));
            var emailConfig = Configuration
                                    .GetSection("EmailConfiguration")
                                    .Get<EmailConfiguration>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton(emailConfig);
            services.AddIdentity<User, IdentityRole>(opt =>
                        {
                            opt.Password.RequiredLength = 7;
                            opt.Password.RequireDigit = false;
                            opt.Password.RequireUppercase = false;
                            opt.User.RequireUniqueEmail = true;
                            opt.SignIn.RequireConfirmedEmail = true;
                        }).AddEntityFrameworkStores<SchoolContext>()
                         .AddDefaultTokenProviders(); ;
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                        opt.TokenLifespan = TimeSpan.FromHours(2));
            services.AddAutoMapper(typeof(Startup));

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Student}/{action=Index}/{id?}");
            });
        }
    }

    
}
