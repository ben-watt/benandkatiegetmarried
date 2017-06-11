using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid MessageBoardId { get; set; }
        public string Text { get; set; }
        public IEnumerable<Guest> SignedBy { get; set; }

    }
}