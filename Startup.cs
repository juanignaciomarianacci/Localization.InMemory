using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Localizatio.InMemory
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                builder => builder.WithOrigins("https://localhost:4201")
                .AllowAnyHeader()
                .AllowAnyMethod());
            });

            services.AddControllers();
            services.AddDbContext<LocalizationContext>(opt => opt.UseInMemoryDatabase("localizations"));
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsApi");

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Localization POC");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<LocalizationContext>();
                AddLanguagesData(context);
            }
            
        }

        private static void AddLanguagesData(LocalizationContext context)
        {
            var label1 = new Localizations { Language = "en", Key = "test", Value = "Test" };
            var label2 = new Localizations { Language = "es", Key = "test", Value = "Prueba" };
            context.Localizations.Add(label1);
            context.Localizations.Add(label2);

            context.SaveChanges();
        }
    }
}
