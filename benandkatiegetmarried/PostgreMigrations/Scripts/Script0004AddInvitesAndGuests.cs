using DbUp.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Generators;
using System.IO;

namespace PostgreMigrations.Scripts
{
    public class Script0003AddInvitesAndGuests : IScript
    {
        public string ProvideScript(Func<IDbCommand> dbCommandFactory)
        {
            var script = new StringBuilder();
            var eventId = "50a3e288-4332-40b3-aff7-8f5db6e8783d";
            var guests = File.ReadAllLines(@"Content\guests.csv");
            var generator = new WordGeneratorBuilder()
                .WordFilter(x => x.Length > 2 && x.Length < 5)
                .Build();

            foreach (var guest in guests)
            {
                var inviteId = Guid.Empty;
                var guestArray = guest.Split(',');
                var inviteGroup = guestArray[0];
                var firstName = guestArray[1];
                var lastName = guestArray[2];
                var isFeatured = guestArray[3];
                var lastInviteGroup = -1;

                if(lastInviteGroup.ToString() != inviteGroup)
                {
                    inviteId = Guid.NewGuid();
                    
                    script.Append(@"INSERT INTO core.Invites (Id, EventId, SecurityCode, Password, Greeting, Type) ");
                    script.AppendFormat(@"VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}'); ",
                        inviteId, eventId, generator.Generate(), generator.Generate(), null, "Day");
                }

                script.Append(@"INSERT INTO core.Guests (Id, EventId, InviteId, 
                    FirstName, LastName, IsFeatured) ");
                script.AppendFormat(@"VALUES('{0}','{1}','{2}','{3}','{4}','{5}'); ",
                    Guid.NewGuid(), eventId, inviteId, firstName, lastName, isFeatured);
            }

            return script.ToString();
        }
    }
}
