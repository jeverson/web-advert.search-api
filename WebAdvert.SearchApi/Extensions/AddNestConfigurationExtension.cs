using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using WebAdvert.SearchApi.Models;

namespace WebAdvert.SearchApi.Extensions
{
    public static class AddNestConfigurationExtension
    {

        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticSearchUrl = configuration.GetSection("ES").GetValue<string>("url");

            var connSettings = new ConnectionSettings(new Uri(elasticSearchUrl))
                .DefaultIndex("adverts")
                .DefaultMappingFor<AdvertType>(a => a.IdProperty(p => p.Id));

            var client = new ElasticClient(connSettings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
