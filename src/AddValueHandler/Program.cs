using System;
using System.Threading;
using Core;
using Core.Logging;
using Logging;
using Messaging;
using Microsoft.EntityFrameworkCore;
using NATS.Client;
using Persistance;

namespace AddValueHandler {
    class Program {
        private static ManualResetEvent _shutdown = new ManualResetEvent(false);
        private static string _brokerUrl = "nats://template-mq:4222";

        private static ILog _log = new ConsoleLogger();

        private static IValuesRepository _valueRepository;

        static void Main(string[] args) {
            using(var connection = new ConnectionFactory().CreateConnection(Config.BROKER_URL)) {
                var subscription = connection.SubscribeAsync(AddValueCommand._subject, "add-value-handler");

                subscription.MessageHandler += AddValue;

                subscription.Start();

                Console.WriteLine("Subscription started.");

                _shutdown.WaitOne();
            }
        }

        private static void AddValue(object sender, MsgHandlerEventArgs e) {
            var message = MessageSerializer.Deserializer<AddValueCommand>(e.Message.Data);

            var value = message.Value;

            var optionsBuilder = new DbContextOptionsBuilder<TemplateContext>();
            optionsBuilder.UseSqlServer(Config.CONNECTION_STRING);

            using(var context = new TemplateContext(optionsBuilder.Options)) {
                _valueRepository = new ValueRepository(context, _log);
                var valueToInsert = new Value() { Description = value };
                var insertTask = _valueRepository.InsertAsync(valueToInsert);
                insertTask.Wait();
                if (insertTask.IsCompletedSuccessfully) {
                    _log.LogInfo($"Processed message: {message.CorrelationId} with value: {value}");
                } else {
                    _log.LogInfo($"Failed to process message: {message.CorrelationId} with value: {value}");
                }
            }

        }
    }
}