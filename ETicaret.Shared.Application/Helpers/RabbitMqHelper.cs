using ETicaret.Shared.Application.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Helpers
{
    public class RabbitMqHelper
    {
        private readonly ConnectionFactory _factory;
        private readonly IModel _channel;
        private readonly IEmailSender _emailSender;

        public RabbitMqHelper(IEmailSender emailSender  )
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = _factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "mail", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _emailSender = emailSender;
        }
        public async Task SendMail(MailRequest mail)
        {
            string message = System.Text.Json.JsonSerializer.Serialize(mail);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish("", routingKey: "mail", basicProperties: null, body: body);


         await  ConsumeMail(mail);



        }

        public async Task ConsumeMail (MailRequest mail)
        {
            var consumer = new EventingBasicConsumer(_channel);

            _channel.BasicConsume("mail", false, consumer);

            consumer.Received += async (object sender, BasicDeliverEventArgs e) =>
            {
                var mes = Encoding.UTF8.GetString(e.Body.ToArray());

                var model = JsonConvert.DeserializeObject<MailRequest>(mes);
                await _emailSender.SendAsync(model);
            };

        }








    }
}
