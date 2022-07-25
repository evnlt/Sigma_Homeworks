using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace QueueLib.Models
{
    public class Director
    {
        private Random _random = new Random();
        private List<Terminal> _terminals;
        private List<ClientLine> _lines;
        private ClientPool _clientPool;

        public Director(List<Terminal> terminals, ClientPool clientPool)
        {
            _terminals = new List<Terminal>(terminals);
            _lines = new List<ClientLine>();

            foreach (var terminal in terminals)
            {

                ClientLine line = new ClientLine();
                terminal.AllocateLine(line);
                _lines.Add(line);
            }

            _clientPool = clientPool;
            _clientPool.OnNewClient += ClientPool_OnNewClient;
        }

        public void Start()
        {
            var thread = new Thread(() => _clientPool.SpawnClient());
            thread.IsBackground = false;
            thread.Start();
        }

        private void ClientPool_OnNewClient(object sender, ClientSpawnEvengArgs e)
        {
            AppointClient(e.Client, e.SpawnCoordinates);
        }

        private void AppointClient(Client client, Coordinates spawnCoordinates)
        {
            int index = GetQueueIndex();
            Console.WriteLine("Appointed to terminal " + index);
            _lines[index].Add(client);
        }

        private int GetQueueIndex()
        {
            // TODO - come up with algorithm
            return _random.Next(_lines.Count - 1);
        }
    }
}
