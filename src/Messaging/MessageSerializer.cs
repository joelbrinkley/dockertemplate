using NATS.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Messaging
{
    public class MessageSerializer
    {
        public static byte[] Serialize<TMessage>(TMessage message) where TMessage : Message
        {
            var json = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(json);
        }
        public static T Deserializer<T>(byte[] messageData)
        {
            var json  = Encoding.UTF8.GetString(messageData);
            var obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

    }
}
