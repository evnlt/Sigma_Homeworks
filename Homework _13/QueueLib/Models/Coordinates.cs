using System;
using System.Collections.Generic;
using System.Text;

namespace QueueLib.Models
{
    public struct Coordinates
    {
        public double X { get; set; }

        public double Y { get; set; }

        public static double CalculateDistance(Coordinates c1, Coordinates c2)
        {
            return Math.Sqrt(Math.Pow((c1.X - c2.X), 2) + Math.Pow((c1.Y - c2.Y), 2));
        }

        public static Coordinates GetRandom()
        {
            Random random = new Random();
            Coordinates result = new Coordinates
            {
                X = random.Next(Config.MinX, Config.MaxX),
                Y = random.Next(Config.MinY, Config.MaxY)
            };
            return result;
        }
    }
}
