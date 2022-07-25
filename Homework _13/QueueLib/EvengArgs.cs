using QueueLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueueLib
{
    public class ClientSpawnEvengArgs : EventArgs
    {
        public Client Client { get; set; }

        public Coordinates SpawnCoordinates { get; set; }

        public ClientSpawnEvengArgs(Client client, Coordinates spawnCoordinates)
        {
            Client = client;
            SpawnCoordinates = spawnCoordinates;
        }
    }
}
