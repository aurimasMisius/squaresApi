using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mongo;
using MongoDB.Driver;
using Services;

namespace SquaresApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SquresApi", Version = "v1" });
            });

            IConfigurationSection mongoSection = Configuration.GetSection("Persistance:Mongo");
            IConfigurationSection connectionStringSection = mongoSection.GetSection("ConnectionString");
            IConfigurationSection databaseTitleSection = mongoSection.GetSection("DatabaseTitle");
            MongoClientSettings clientSettings = MongoClientSettings.FromUrl(new MongoUrl(connectionStringSection.Value));
            services.AddSingleton(new MongoClient(clientSettings).GetDatabase(databaseTitleSection.Value));

            services.AddTransient(s => new PointsRepository(s.GetRequiredService<IMongoDatabase>(), CollectionNames.Points));
            services.AddTransient(s => new SquaresRepository(s.GetRequiredService<IMongoDatabase>(), CollectionNames.Squares));

            services.AddTransient<IPointsService>(s => new PointsService(s.GetRequiredService<PointsRepository>()));
            services.AddTransient<ISquaresService>(s => new SquaresService(s.GetRequiredService<SquaresRepository>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SquresApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
