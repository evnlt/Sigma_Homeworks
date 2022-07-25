using QueueLib.Models;
using QueueLib.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QueueLib.Factories
{
    public class PeopleFromFileFactory
    {
        private int _lineCount = 0;
        public string FilePath { get; set; }
        public PeopleFromFileFactory(string filePath)
        {
            FilePath = filePath;
        }
        public Client? CreateInstance()
        {
            Client person = null;
            using(StreamReader sr = new StreamReader(FilePath))
            {
                int count = 0;
                while (_lineCount != count)
                {
                    count++;
                    sr.ReadLine();
                }
                    
                _lineCount++;

                try
                {
                    person = ClientParser.Parse(sr.ReadLine());
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException($"Could not parse data on line {_lineCount}", ex);
                }

            }
            return person;
        }
    }
}
