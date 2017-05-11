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

        [Fact]
        public void Guest_MustHaveAFirstNameThatIsNotEmptyToBeValid()
        {
            Guest g = new Guest();
            Assert.Equal(false, g.IsValid());
        }
        [Fact]
        public void Guest_MustHave_FirstName_Type_ToBeValid()
        {
            var g = new Guest();
            g.FirstName = "Ben";
            g.Type = "Groom";
            Assert.True(g.IsValid());
        }
    }
}
