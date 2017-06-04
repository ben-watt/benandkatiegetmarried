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
    }
}
