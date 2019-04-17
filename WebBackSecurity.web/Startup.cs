using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebBackSecurity.web.Data;
using WebBackSecurity.web.Data.Repositories;

namespace WebBackSecurity.web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                // Enabled with app.UseCookiePolicy();
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default2"))
            );

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default2"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("WebBackSecurity.web")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                //.AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TodoPolicyCanEdit", policy => policy.RequireClaim("CanEdit"));
                options.AddPolicy("TodoPolicyCanDelete", policy => policy.RequireClaim("CanDelete"));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "WebBackSecurityCookie";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = new TimeSpan(0, 20, 0);
            });

            services.AddMvc(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.Filters.Add(new RequireHttpsAttribute());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddTransient<ITodoRepository, TodoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseAuthentication();

            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();
        }
    }
}