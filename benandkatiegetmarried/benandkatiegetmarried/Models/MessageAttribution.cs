using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Models
{
    public class MessageAttribution
    {
        public MessageAttribution() { }
        public MessageAttribution(Guid MessageId, Guid GuestId)
        {
            this.MessageId = MessageId;
            this.GuestId = GuestId;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MessageId { get; set; }
        public Guid GuestId { get; set; }
    }
}
