using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Models
{
    [PetaPoco.TableName("core.RSVPs")]
    public class RSVP
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid InviteId { get; set; }
        public Guid GuestId { get; set; }
        public bool Response { get; set; }
        public string ResponseMessage { get; set; }
        public IEnumerable<Meal> MealChoises { get; set; }
        public string DietaryRequirements { get; set; }
        public string SongLink { get; set; }
    }
}
