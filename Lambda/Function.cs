using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApp.Core;
using TestApp.Core.Services.Interfaces;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda
{
    public class Function
    {
        private ServiceCollection _serviceCollection;

        public Function()
        {
            ConfigureServices();
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest apigProxyEvent,
            ILambdaContext context)
        {
            if (!apigProxyEvent.RequestContext.Http.Method.Equals(HttpMethod.Get.Method))
            {
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    Body = "Only GET allowed",
                    StatusCode = (int)HttpStatusCode.MethodNotAllowed,
                };
            }

            context.Logger.LogLine($"Received {apigProxyEvent}");

            var city = apigProxyEvent.PathParameters["city"];

            using (ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider())
            {
                var weatherService = serviceProvider.GetService<IWeatherService>();

                var result = await weatherService.GetData(city, TestApp.Core.Clients.ClientType.OpenWeather);

                return new APIGatewayHttpApiV2ProxyResponse
                {
                    Body = JsonSerializer.Serialize(result),
                    StatusCode = 200,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
            
        }

        private void ConfigureServices()
        {
            var configuration = new ConfigurationBuilder()
                .AddSecretsManager(configurator: opts =>
                {
                     opts.KeyGenerator = (secret, name) => name.Replace("__", ":");
                }).Build();

            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddTestAppCore(configuration);
        }
    }
}