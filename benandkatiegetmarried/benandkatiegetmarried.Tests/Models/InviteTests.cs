using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using benandkatiegetmarried.Models;
using benandkatiegetmarried.Common;
using Moq;

namespace benandkatiegetmarriedTests.Models
{
    public class InviteTests
    {
        [Fact]
        public void Invite_OnCreationWillCreateUniqueIdAndPassword()
        {
            Invite i = new Invite();
            Assert.NotEqual(Guid.Empty, i.Id);
        }
        [Fact]
        public void Invite_GuestsGetAssociatedCorrectly()
        {
            var g = new Guest() { FirstName = "Ben"
                , LastName = "Watt"
                , Type = "Groom"};

            var i = new Invite();
            i.AssociateGuest(g);

            Assert.True(i.Guests.Count() == 1);
            Assert.Equal("Ben", i.Guests.FirstOrDefault().FirstName);
            Assert.Equal("Watt", i.Guests.FirstOrDefault().LastName);
        }
        [Fact]
        public void Invite_CanOnlyContainOneTypeOfGuest()
        {
            var g1 = new Guest() { FirstName = "Ben", Type = "Type1" };
            var g2 = new Guest() { FirstName = "Ben", Type = "Type2" };
            var i = new Invite();

            i.AssociateGuest(g1);

            Action func = () => i.AssociateGuest(g2);

            Assert.Throws<ArgumentException>(func);          
        }        
    }
}
