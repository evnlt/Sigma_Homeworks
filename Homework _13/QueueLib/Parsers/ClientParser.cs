using QueueLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QueueLib.Parsers
{
    internal static class ClientParser
    {
        public static Client Parse(string line)
        {
            if(line == null) return null;

            string[] data = line.Split(' ');

            string name;
            string status;
            int age;
            TimeSpan time;

            try
            {
                name = data[0];
                status = data[1];
                age = int.Parse(data[2]);
                time = TimeSpan.Parse(data[3]);
            }
            catch (Exception)
            {
                throw;
            }

            return new Client(name, age, time, status);
        }
    }
}
