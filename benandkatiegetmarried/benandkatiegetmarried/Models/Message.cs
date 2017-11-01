using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MessageBoardId { get; set; }
        public string Text { get; set; }    
        public DateTime Date { get; set; }
        public string Hierarchy { get; set; }
        [PetaPoco.ResultColumn]
        public int HierarchyLevel { get; set; }
        [PetaPoco.Ignore]
        public IList<MessageGuest> Likes { get; set; } = new List<MessageGuest>();
        [PetaPoco.Ignore]
        public IList<MessageGuest> Attributions { get; set; } = new List<MessageGuest>();

    }
}