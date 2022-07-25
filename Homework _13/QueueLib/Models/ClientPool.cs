using QueueLib.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueLib.Models
{
    public class ClientPool
    {
        private static Random _random = new Random();
        private Queue<Client> _clients = new Queue<Client>();

        public event EventHandler<ClientSpawnEvengArgs> OnNewClient;

        public ClientPool()
        {

        }

        public void ReadFromFile(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();

            foreach (var line in lines)
            {
                try
                {
                    _clients.Enqueue(ClientParser.Parse(line));
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException($"Could not parse data on line {lines.IndexOf(line)}", ex);
                }
            }
        }

        internal void SpawnClient()
        {
            while (_clients.Any())
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Thread.Sleep(_random.Next(Config.MinClientSpawnTime, Config.MaxClientSpawnTime));
                stopwatch.Stop();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Client spawned in " + stopwatch.ElapsedMilliseconds);
                Console.ForegroundColor = ConsoleColor.White;

                OnNewClient(this, new ClientSpawnEvengArgs(_clients.Dequeue(), Coordinates.GetRandom()));
            }
        }
    }
}
