using System.Net.Http;
using Mashup.Clients;
using Mashup.Factories;
using Mashup.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mashup
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddHttpClient();
            services.AddSingleton(s => s.GetRequiredService<IHttpClientFactory>().CreateClient());
            
            services.AddScoped<ISerializerFactory, SerializerFactory>();

            services.AddScoped<IMusicbrainzClient, MusicbrainzClient>();
            services.AddScoped<IWikidataClient, WikidataClient>();
            services.AddScoped<IWikipediaClient, WikipediaClient>();
            services.AddScoped<ICoverartArchiveClient, CoverartArchiveClient>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            }
 
            app.ConfigureExceptionHandler(_logger);
            app.UseMvc();
            
        }
    }
}
