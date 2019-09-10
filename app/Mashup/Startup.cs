using System.Net.Http;
using Mashup.Clients;
using Mashup.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mashup
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
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
        }
    }
}
