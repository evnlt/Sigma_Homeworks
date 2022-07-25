using System;
using System.Collections.Generic;
using System.Text;

namespace QueueLib.Models
{
    public class Client
    {

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Age { get; set; }
        /// <summary>
        /// Time the client is willing to spend waiting in queue
        /// </summary>
        public TimeSpan Time { get; set; }
        public string Status { get; }

        // client only needs spawn coordinates
        //public Coordinates Coordinate { get; set; }

        public Client(string name, int age, TimeSpan time, string status)
        {
            Name = name;
            Age = age;
            Time = time;
            Status = status;
        }
    }
}
