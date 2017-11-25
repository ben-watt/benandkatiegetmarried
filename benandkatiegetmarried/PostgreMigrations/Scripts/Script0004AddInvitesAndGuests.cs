using DbUp.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Generators;
using System.IO;
using benandkatiegetmarried.Common.Security;
using benandkatiegetmarried.Models;
using System.Linq.Expressions;
using System.Collections;
using System.Reflection;

namespace PostgreMigrations.Scripts
{
    public class Script0003AddInvitesAndGuests : IScript
    {
        public string ProvideScript(Func<IDbCommand> dbCommandFactory)
        {

            var generator = new WordGeneratorBuilder()
                .WordFilter(x => x.Length > 2 && x.Length < 5)
                .Build();


            var EVENT_ID = Guid.Parse("50a3e288-4332-40b3-aff7-8f5db6e8783d");
            var OUTPUT_FILE = "C:\\Users\\ben.watt\\Desktop\\invites.csv";
            var INPUT_FILE = @"Content\guests.csv";

            var invites = new InviteIdGenerator();

            var rows = File.ReadAllLines(INPUT_FILE)
                    .GroupBy(row => new InviteGroup(row.GetId(), row.Split(',')[3]))
                    .TeeEach<IEnumerable<IGrouping<InviteGroup, string>>, IGrouping<InviteGroup, string>>(group => {
                        invites.Add(group.Key.Id, new Invite()
                        {
                            Id = Guid.NewGuid(),
                            EventId = EVENT_ID,
                            SecurityCode = generator.Generate(),
                            Password = generator.Generate(),
                            Type = group.Key.Type
                        });
                    })
                    .SelectMany(x => x)
                    .Select(row => new {
                        Id = Guid.NewGuid(),
                        EventId = EVENT_ID,
                        InviteId = invites.Get(row.Split(',')[0]).Id,
                        FirstName = row.Split(',')[1],
                        LastName = row.Split(',')[2],
                        IsFeatured = false,
                        Type = row.Split(',')[3]
                    });


            File.CreateText(OUTPUT_FILE)
                .Tee((sw) => sw.WriteLine(invites.First().Value.PropsToString(", ", (prop, i) => prop.Name)))
                .AppendSequence(invites, (sw, i) =>
                {
                    sw.WriteLine(i.Value.PropsToString(", ", (prop, obj) => prop.GetValue(obj).ToString()));
                    return sw;
                }).Close();

            var sqlScript = new StringBuilder()
                .AppendSequence(invites,
                (sb, invite) => sb.AppendFormatLine(2,
                    @"INSERT INTO core.Invites (Id, EventId, SecurityCode, Password, Greeting, Type)  
                        VALUES ('{0}', '{1}', '{2}', '{3}', null , '{4}');",
                      invite.Value.Id, invite.Value.EventId, invite.Value.SecurityCode, invite.Value.Password.EncryptPassword(), "day"))
               .AppendSequence(rows, (sb, row) =>
                    sb.AppendFormatLine(2,
                    @"INSERT INTO core.Guests (Id, EventId, InviteId, FirstName, LastName, IsFeatured) 
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}'); ",
                     row.Id, row.EventId, row.InviteId, row.FirstName, row.LastName, row.IsFeatured));

            return sqlScript.ToString();

        }
    }


    public class InviteGroup
    {
        public string Id { get; }
        public string Type { get; }
        public InviteGroup(string id, string type)
        {
            Id = id;
            Type = type;
        }
    }

    public class Invite
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string SecurityCode { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
    }

    public class InviteIdGenerator : IEnumerable<KeyValuePair<string, Invite>>
    {

        private IDictionary<string, Invite> _list { get; } = new Dictionary<string, Invite>();
        public void Add(string key, Invite value)
        {
            if (!_list.ContainsKey(key))
                _list.Add(key, value);
        }

        public Invite Get(string key)
        {
            return _list[key];
        }

        public IEnumerator<KeyValuePair<string, Invite>> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    public static class Extensions
    {

        public static string GetId(this string str)
        {
            return str.Split(',')[0];
        }

        public static string PropsToString(this object self, string delimiter, Func<PropertyInfo, object, string> fn)
        {
            var props = self.GetType().GetProperties().Select((x) => fn(x, self));
            return new StringBuilder()
                .AppendSequence(props, (str, p) => str.Append(p + delimiter)).ToString();

        }

        public static StringBuilder AppendFormatLine(
            this StringBuilder sb,
            int lines,
            string format,
            params object[] args) => sb.AppendFormat(format, args).AppendLine(lines);

        public static StringBuilder AppendFormatLine(
            this StringBuilder sb,
            string format,
            params object[] args) => sb.AppendFormat(format, args).AppendLine();

        public static StringBuilder AppendLine(
            this StringBuilder sb,
            int lines) => sb.AppendSequence(Enumerable.Range(0, lines), (self, line) => self.AppendLine());

        public static T Tee<T>(this T self, Action<T> act)
        {
            act(self);
            return self;
        }

        public static T TeeEach<T, U>(this T self, Action<U> act) where T : IEnumerable<U>
        {
            foreach (var i in self)
            {
                act(i);
            }
            return self;
        }

        public static TSource AppendSequence<TSource, T>(
            this TSource self,
            IEnumerable<T> seq,
            Func<TSource, T, TSource> fn) => seq.Aggregate(self, fn);
    }
}
