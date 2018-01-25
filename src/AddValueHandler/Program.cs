using System;
using System.Threading;
using NATS.Client;
using Messaging;

namespace AddValueHandler
{
    class Program
    {
        private static ManualResetEvent _shutdown = new ManualResetEvent(false);
        private static string _brokerUrl = "nats://template-mq:4222";

        static void Main(string[] args)
        {
            using (var connection = new ConnectionFactory().CreateConnection(_brokerUrl))
            {
                var subscription = connection.SubscribeAsync(AddValueCommand._subject, "add-value-handler");

                subscription.MessageHandler += AddValue;

                subscription.Start();
                
                Console.WriteLine("Subscription started.");

                _shutdown.WaitOne();
            }
        }

        private static void AddValue(object sender, MsgHandlerEventArgs e)
        {
            var message = MessageSerializer.Deserializer<AddValueCommand>(e.Message.Data);

            var value = message.Value;

            Console.WriteLine($"Processed message: {message.CorrelationId} with value: {value}");

        }
    }
}
