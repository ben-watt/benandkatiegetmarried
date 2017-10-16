using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MessageBoardId { get; set; }
        public string Text { get; set; }
        [PetaPoco.ResultColumn]
        public IEnumerable<Guest> SignedBy { get; set; }

    }
}