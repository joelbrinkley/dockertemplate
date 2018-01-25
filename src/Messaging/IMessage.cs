using System;
using System.Collections.Generic;
using System.Text;

namespace Messaging
{
    public abstract class Message
    {
        public string CorrelationId { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public abstract string Subject { get; }
        public Message()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.CorrelationId = Guid.NewGuid().ToString();
        }
    }
}
