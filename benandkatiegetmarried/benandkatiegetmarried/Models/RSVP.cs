using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Models
{
    public class Rsvp
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EventId { get; set; }
        public Guid InviteId { get; set; }
        public string DietaryRequirements { get; set; }
        public string SongLink { get; set; }

        [PetaPoco.Ignore]
        public IEnumerable<RsvpResponse> Responses { get; set; } = new List<RsvpResponse>();
        public void LinkResponses()
        {
            for (int i = 0; i < Responses.Count(); i++)
            {
                Responses.ElementAt(i).RsvpId = this.Id;
            }
        }
    }

    public class RsvpResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RsvpId { get; set; }
        public Guid GuestId { get; set; }
        public bool Response { get; set; }
        public string MealChoice { get; set; }

    }
  
}
