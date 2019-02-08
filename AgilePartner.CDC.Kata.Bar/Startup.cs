using AgilePartner.CDC.Kata.Bar;
using AgilePartner.CDC.Kata.Bar.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AgilePartner.CDC.Kata
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.RespectBrowserAcceptHeader = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IBarService, BarService>()
                    .AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new Info { Title = "Bar API", Version = "v1" });
                    });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bar API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            RegisterMiddlewares(app);
            app.UseMvc();
        }

        public virtual void RegisterMiddlewares(IApplicationBuilder app)
        {

        }
    }
}
