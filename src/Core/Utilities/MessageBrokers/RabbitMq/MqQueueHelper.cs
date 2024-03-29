﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Core.Utilities.MessageBrokers.RabbitMq
{
    public class MqQueueHelper : IMessageBrokerHelper
    {
        public IConfiguration Configuration { get; }
        private readonly MessageBrokerOptions _brokerOptions;
        public MqQueueHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _brokerOptions = Configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
        }
        public void QueueMessage(string queueName, object messageText)
        {
            var factory = new ConnectionFactory
            {
                HostName = _brokerOptions.HostName,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = JsonConvert.SerializeObject(messageText);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
