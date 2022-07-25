using QueueLib.Factories;
using QueueLib.Models;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string clientFile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework _13\QueueLib\people.txt";

            ClientPool pool = new ClientPool();
            pool.ReadFromFile(clientFile);

            List<Terminal> terminals = new List<Terminal> { 
                new Terminal(Coordinates.GetRandom(), 0),
                new Terminal(Coordinates.GetRandom(), 1),
                new Terminal(Coordinates.GetRandom(), 2),
                new Terminal(Coordinates.GetRandom(), 3)
            };

            Director director = new Director(terminals, pool);

            director.Start();
        }
    }
}