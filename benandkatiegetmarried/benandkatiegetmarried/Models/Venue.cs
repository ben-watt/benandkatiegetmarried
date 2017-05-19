using System;

namespace benandkatiegetmarried.Models
{
    public class Venue
    {
        public Guid VenueId { get; set; }
        public Guid WeddingId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostCode { get; set; }

    }
}