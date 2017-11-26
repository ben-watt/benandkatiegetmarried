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
        private IList<KeyValuePair<string, string>> _previouPairs = new List<KeyValuePair<string, string>>();
        private Random NumberGen = new Random();
        public string[] dictionary;
        public string[] uniqueDictionary;

        public string Generate()
        {
            return GetItem(dictionary);
        }

        public KeyValuePair<string,string> GeneratePair()
        {
            var pair = new KeyValuePair<string, string>(GetItem(uniqueDictionary), GetItem(dictionary));
            uniqueDictionary = uniqueDictionary.Where(x => x != pair.Key).ToArray();
            return pair;
        }

        private string GetItem(string[] dic)
        {
            if (dic.Length == 0)
                throw new AccessViolationException("No more values available in this dictionary");
            return dic[RandomNumer(dic.Length - 1)];
        }

        private int RandomNumer(int max) => NumberGen.Next(max);
    }


    public class WordGeneratorBuilder
    {
        public WordGeneratorBuilder()
        {
            _generator = new WordGenerator();
            _generator.dictionary = GetDictionaryFile();
            _generator.uniqueDictionary = _generator.dictionary;
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
