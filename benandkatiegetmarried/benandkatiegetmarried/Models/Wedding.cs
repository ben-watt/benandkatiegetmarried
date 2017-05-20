using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Models
{
    [PetaPoco.TableName("core.Weddings")]
    public class Wedding : Event
    {
        public Wedding()
        {
            Id = Guid.NewGuid();
        }
        [PetaPoco.Column("EventId")]
        public new Guid Id { get; set; }
        public string Bride { get; set; }
        public string Groom { get; set; }
    }
}
