using System;
using System.Collections.Generic;
using System.Text;

namespace QueueLib
{
    public static class Config
    {
        // Serving Time
        public static int MaxServingTime { get; set; } = 5000;
        public static int MinServingTime { get; set; } = 100;

        // Client Spawn Time
        public static int MaxClientSpawnTime { get; set; } = 5000;
        public static int MinClientSpawnTime { get; set; } = 100;

        // Client Spawn Coordinates

        public static int MaxX { get; set; } = 10;
        public static int MinX { get; set; } = 0;

        public static int MaxY { get; set; } = 10;
        public static int MinY { get; set; } = 0;
    }
}
