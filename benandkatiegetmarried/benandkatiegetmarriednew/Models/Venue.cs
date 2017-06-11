using System;

namespace benandkatiegetmarried.Models
{
    public class Venue
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }

    }
}