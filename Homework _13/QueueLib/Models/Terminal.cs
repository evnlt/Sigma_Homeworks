using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueLib.Models
{
    public class Terminal
    {
        private static Random _randomTime = new Random();
        private ClientLine _currentLine;
        private Client _currentClient;
        private Thread _thread;

        public int Id { get; }
        public Coordinates Coordinates { get; }

        #region ctors

        public Terminal(Coordinates coordinates, int id)
        {
            Coordinates = coordinates;
            Id = id;
        }
        public Terminal(Coordinates coordinates, ClientLine clientLine)
        {
            Coordinates = coordinates;

            AllocateLine(clientLine);
        } 
        #endregion

        public void AllocateLine(ClientLine line)
        {
            if(line == null)
                throw new ArgumentNullException("line");

            _currentLine = line;
            _currentLine.OnAddingNewClient += _currentLine_OnAddingNewClient;
        }

        private void _currentLine_OnAddingNewClient(object sender, EventArgs e)
        {
            if (_currentClient == null)
            {
                _currentClient = (sender as ClientLine).GetNext();

                _thread = new Thread(() =>
                    ServeNewClient(_currentClient));
                _thread.IsBackground = false;
                _thread.Start();
            }
        }

        public void ServeNewClient(Client client)
        {
            if(client == null)
                throw new ArgumentNullException("Client is null");

            _currentClient = client;

            _thread = new Thread(() => ServeClient());
            _thread.IsBackground = false;
            _thread.Start();

            LetGoClient();

            if(!_currentLine.IsEmpty)
                ServeNewClient(_currentLine.GetNext());
        }

        private void ServeClient()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread.Sleep(_randomTime.Next(Config.MinServingTime, Config.MaxServingTime));
            stopwatch.Stop();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Terminal " + Id + " served a client in " + stopwatch.ElapsedMilliseconds);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void LetGoClient()
        {
            _currentClient = null;
        }
    }
}
