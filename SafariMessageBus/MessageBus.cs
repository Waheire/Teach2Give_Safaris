﻿using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariMessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly string connectionString = "Endpoint=sb://t2g-safaribus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cT/WaVUbkfA+Fd/hmst9EeVxPQZ9imsLm+ASbBzB3yc=";
        public async Task PublishMessage(object message, string Topic_Queue_Name)
        {
            //Create a client
            var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(Topic_Queue_Name);

            //convert to Json
            var body = JsonConvert.SerializeObject(message);

            ServiceBusMessage theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(body))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            //send the message
            await sender.SendMessageAsync(theMessage);

            //free the resources 
            await sender.DisposeAsync();
         
        }
    }
}
