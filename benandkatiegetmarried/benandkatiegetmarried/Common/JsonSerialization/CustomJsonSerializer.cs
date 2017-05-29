using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Nancy.Serialization.JsonNet;
using Newtonsoft.Json;

namespace benandkatiegetmarried.Common.JsonSerialization
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            this.Formatting = Formatting.Indented;
        }
    }
}
