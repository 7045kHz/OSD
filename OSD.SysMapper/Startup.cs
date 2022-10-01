using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OSD.RazorData.Context;
using OSD.RazorData.Repositories.SysMapper.Views;
using OSD.RazorData.Repositories.SysMapper.Tables;
using OSD.RazorData.Repositories.SysMapper.Programmability;
using OSD.SysMapper.Areas.Identity;
using OSD.SysMapper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSD.SysMapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SysmapperDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddScoped<IViewCategoryRepository, ViewCategoryRepository>();
            services.AddScoped<IViewOuRepository, ViewOuRepository>();
            services.AddScoped<IViewLifeCycleRepository, ViewLifeCycleRepository>();
            services.AddScoped<IViewTagRepository, ViewTagRepository>();
            services.AddScoped<IViewLabelTypeRepository, ViewLabelTypeRepository>();
            services.AddScoped<IViewLabelMapRepository, ViewLabelMapRepository>();
            services.AddScoped<IViewResourceMapRepository, ViewResourceMapRepository>();
            services.AddScoped<IViewResourceTypeRepository, ViewResourceTypeRepository>();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ILifeCycleRepository, LifeCycleRepository>();
            services.AddScoped<IStoredProcedures, StoredProcedures>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
