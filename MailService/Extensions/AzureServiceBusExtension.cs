using MailService.Messaging;
using System.Runtime.CompilerServices;

namespace MailService.Extensions
{
    public static class AzureServiceBusExtension
    {

        public static IAzureServiceBusConsumer azureServiceBusConsumer { get; set; }

        public static IApplicationBuilder userAzure(this IApplicationBuilder app) 
        {
            //know about the consumer service and a lot about app lifetime
            azureServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();

            var HostLifeTime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            HostLifeTime.ApplicationStarted.Register(OnAppStart);
            HostLifeTime.ApplicationStopping.Register(OnAppStopping);

            return app;

        }

        private static void OnAppStopping()
        {
            throw new NotImplementedException();
        }

        private static void OnAppStart()
        {
            throw new NotImplementedException();
        }
    }
}
