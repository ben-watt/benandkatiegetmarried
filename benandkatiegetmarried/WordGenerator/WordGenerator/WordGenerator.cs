using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WordGenerator.Properties;
using System.Linq.Expressions;

namespace Generators
{
    public class WordGenerator
    {
        private Random NumberGen = new Random();
        public string[] dictionary;

        public string Generate()
        {
            var wordIndex = NumberGen.Next(dictionary.Length);
            return dictionary[wordIndex];
        }
    }

    public class WordGeneratorBuilder
    {
        public WordGeneratorBuilder()
        {
            _generator = new WordGenerator();
            _generator.dictionary = GetDictionaryFile();
        }

        public WordGenerator _generator;
        public WordGeneratorBuilder WithWords(string[] words)
        {
            _generator.dictionary = words;
            return this;
        }     
        public WordGeneratorBuilder WordFilter(Func<string, bool> wordLength)
        {
            _generator.dictionary = _generator.dictionary.Where(wordLength).ToArray();
            return this;
        }
        public WordGenerator Build()
        {
            return _generator;
        }
        private string[] GetDictionaryFile()
        {
            var fileLocation = Settings.Default.DictionaryFile;
            return File.ReadAllLines(fileLocation);
        }
    }
}
