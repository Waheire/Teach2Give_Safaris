
using Azure.Messaging.ServiceBus;
using MailService.Model.Dtos;
using Microsoft.Azure.Amqp.Framing;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;


namespace MailService.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ServiceBusProcessor _emailProcessor;

        public AzureServiceBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("AzureConnectionString");
            _queueName = _configuration.GetValue<string>("QueueAndTopics: registerusers");

            var client = new ServiceBusClient(_connectionString);
            _emailProcessor = client.CreateProcessor(_queueName);

        }

        public async Task Start()
        {
            _emailProcessor.ProcessMessageAsync += OnRegisterUser;
            _emailProcessor.ProcessErrorAsync += ErrorHandler;

            await _emailProcessor.StartProcessingAsync();
        }

       

        public async Task Stop()
        {
            await _emailProcessor.StopProcessingAsync();
            await _emailProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            //send email to admin
            return Task.CompletedTask;
        }

        private async Task OnRegisterUser(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);//read as string
            var user = JsonConvert.DeserializeObject<UserMessageDto>(body); //string to userMessageDto
            try 
            {
                // To send email
                //say that you are done
                await args.CompleteMessageAsync(args.Message);

            } 
            catch (Exception ex) 
            {
                throw;
                //send email to Admin
            }
        }
    }
}
