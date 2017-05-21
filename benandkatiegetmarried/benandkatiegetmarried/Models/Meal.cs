using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Models
{
    [PetaPoco.TableName("core.Meals")]
    public class Meal
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string MealGroup { get; set; }
    }
}
