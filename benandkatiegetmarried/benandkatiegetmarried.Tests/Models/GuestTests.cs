using benandkatiegetmarried.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace benandkatiegetmarriedTests.Models
{
    public class GuestTests
    {
        [Fact]
        public void Guest_OnCreation_UniqueIdIsCreated()
        {
            Guest g = new Guest();
            Assert.NotEqual(Guid.Empty, g.Id);
        }
    }
}
