using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarriedTests
{
    public class TestRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Path.GetFullPath(Path.Combine("..", "..", "..", "benandkatiegetmarried"));
        }
    }
}
