using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreOptionSample.Extensions;
using NetCoreOptionSample.Models;

namespace NetCoreOptionSample
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
            services.Configure<AuthConfig>(Configuration.GetSection("MyAuth"));

            //services.AddSingleton(p =>
            //{
            //    var config = new AuthConfig();
            //    var section = Configuration.GetSection("MyAuth");
            //    section.Bind(config);
            //    return config;
            //});

            services.AddSingletonConfig<AuthConfig>(Configuration.GetSection("MyAuth"));
            //services.AddScopedConfig<AuthConfig>(Configuration.GetSection("MyAuth"));
            //services.AddTransientConfig<AuthConfig>(Configuration.GetSection("MyAuth"));

            //services.AddConfig<AuthConfig>(Configuration.GetSection("MyAuth"), ServiceLifetime.Singleton);
            //services.AddConfig<AuthConfig>(Configuration.GetSection("MyAuth"), ServiceLifetime.Scoped);
            //services.AddConfig<AuthConfig>(Configuration.GetSection("MyAuth"), ServiceLifetime.Transient);

            services.AddControllersWithViews();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
