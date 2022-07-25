using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueLib.Models
{
    public class ClientLine : ICollection<Client>
    {
        private List<Client> _clients = new List<Client>();

        public event EventHandler<EventArgs> OnAddingNewClient;

        public Client GetNext()
        {
            if(IsEmpty)
                return null;

            Client client = _clients.First();
            _clients.Remove(client);

            if (Count != 0)
            {
                List<Client> newClients = new List<Client>(Count - 1);
                Array.Copy(_clients.ToArray(), 1, newClients.ToArray(), 0, _clients.Count);
                _clients = newClients; 
            }

            return client;
        }

        public bool IsEmpty => _clients.Count == 0;

        #region ICollection
        public int Count => ((ICollection<Client>)_clients).Count;

        public bool IsReadOnly => ((ICollection<Client>)_clients).IsReadOnly;

        public void Add(Client item)
        {
            ((ICollection<Client>)_clients).Add(item);
            // TODO - optimize insert 
            SortByPriotity();

            OnAddingNewClient(this, new EventArgs());
        }

        public void Clear()
        {
            ((ICollection<Client>)_clients).Clear();
        }

        public bool Contains(Client item)
        {
            return ((ICollection<Client>)_clients).Contains(item);
        }

        public void CopyTo(Client[] array, int arrayIndex)
        {
            ((ICollection<Client>)_clients).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Client> GetEnumerator()
        {
            return ((IEnumerable<Client>)_clients).GetEnumerator();
        }

        public bool Remove(Client item)
        {
            return ((ICollection<Client>)_clients).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_clients).GetEnumerator();
        } 
        #endregion

        private void SortByPriotity()
        {
            _clients = _clients.OrderBy(x => x.Status).ThenBy(x => x.Age).ToList();
        }
    }
}
