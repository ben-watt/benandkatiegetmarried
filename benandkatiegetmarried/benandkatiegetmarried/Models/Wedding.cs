using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Models
{
    [PetaPoco.TableName("core.Weddings")]
    public class Wedding
    {
        public Wedding() { }
        private Wedding(string bride, string groom)
        {
            this.Bride = bride;
            this.Groom = groom;
            this.Id = Guid.NewGuid();
        }
        public static Wedding Create(string bride , string groom)
        {
            return new Wedding(bride, groom);
        }
        public Guid Id { get; set; }
        public string Bride { get; set; }
        public string Groom { get; set; }
        public string EventName { get; set; }
        public Guid VenueId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [PetaPoco.Ignore]
        public IList<Guest> Guests { get; set; }

        public void SetDates(DateTime Start, DateTime End)
        {
            if(Start <= DateTime.Now
                || End < Start)
            {
                throw new ArgumentException("StartDate Cannot be earlier than the current time");
            }
        }

    }
}
