using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Generators;
using System.IO;

namespace WordGeneratorTests
{
    public class WordGeneratorTests
    {
        [Fact]
        public void WordGen_ReturnsString()
        {
            var word = WordGenerator.Generate();
            Assert.False(String.IsNullOrEmpty(word));
        }
        [Fact]
        public void WordGen_EachWordIsRandom()
        {
            var word1 = WordGenerator.Generate();
            var word2 = WordGenerator.Generate();
            Assert.NotEqual(word1, word2);
        }

        //Generate words and spit out to file
    }
}
