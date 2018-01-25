using NATS.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Logging;

namespace Messaging
{
    public class NatsBus
    {
        private readonly string brokerUrl;
        private readonly ConnectionFactory _natsConnectionFactory;
        private readonly ILog _log;

        public NatsBus(string brokerUrl, ConnectionFactory natsConnectionFactory, ILog log)
        {
            this.brokerUrl = brokerUrl;
            this._natsConnectionFactory = natsConnectionFactory;
            this._log = log;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : Message
        {
            using (var connection = _natsConnectionFactory.CreateConnection(brokerUrl))
            {
                var payload = MessageSerializer.Serialize(message);
                connection.Publish(message.Subject, payload);
            }
        }

        public Msg Send<TMessage>(TMessage message) where TMessage : Message
        {
            _log.LogInfo($"NATs: sending message");

            using (var connection = _natsConnectionFactory.CreateConnection(brokerUrl))
            {
                var payload = MessageSerializer.Serialize(message);

                _log.LogInfo($"Message Payload: \r\n {payload.ToString()}");

                var response = connection.Request(message.Subject, payload, 1);

                return response;
            }
        }
    }
}
