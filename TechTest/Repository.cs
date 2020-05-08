using System;
using System.Collections.Generic;
using System.Linq;

namespace TechTest
{
    public class Repository : IRepository
    {
        public InMemoryCashe memoryCashe;
        private const string AllClientsKey = "AllClients";

        internal Repository(InMemoryCashe cashe)
        {
            memoryCashe = cashe;
        }
        public void SaveConnectedClient(ConnectedClient client)
        {
            memoryCashe.Add(client.ClientId.ToString(), client);
            
        }
        public string GetMessageById(string messageId,ConnectedClient client)
        {
            string returnMessage = "";
            try
            {
                var storedClient = (ConnectedClient)memoryCashe.Get(client.ClientId.ToString());
                if (storedClient != null)
                {
                    returnMessage = storedClient.Messages.First(c => c.Id == Guid.Parse(messageId)).Message;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find any message by this id");
            }
            
            return returnMessage;
        }
        public void UppdateConnectedClient(ConnectedClient client)
        {
            memoryCashe.Delete(client.ClientId.ToString());
            memoryCashe.Add(client.ClientId.ToString(), client);
        }
        public ConnectedClient GetConnectedClient(string clientId)
        {
            return (ConnectedClient)memoryCashe.Get(clientId);
        }
        private List<string> GetAllClientIds()
        {
            var connectedClients = (List<string>)memoryCashe.Get(AllClientsKey);
            if (connectedClients != null)
                return connectedClients;
            return new List<string>();
        }
        private void DeleteClientIdFromConnectedClients(string clientId)
        {
            var clients = GetAllClientIds();
            clients.Remove(clientId);
            memoryCashe.Delete(AllClientsKey);
            memoryCashe.Add(AllClientsKey, clients);
        }
        public ClientMessage[] GetAllMessagesAllClients()
        {
            var allClientIds = GetAllClientIds();
            var messages = new List<ClientMessage>();
            foreach (var clienId in allClientIds)
            {
                var client = (ConnectedClient)memoryCashe.Get(clienId);
                if (client != null)
                    messages.AddRange(client.Messages);
            }
            return messages.ToArray();
        }
        public void AddClientId(string clientid)
        {
            var clients = GetAllClientIds();
            if (!clients.Contains(clientid))
                clients.Add(clientid);
            memoryCashe.Add(AllClientsKey, clients);
        }
        public void ClientDissconect(string clientId)
        {
            memoryCashe.Delete(clientId);
            DeleteClientIdFromConnectedClients(clientId);
        }
    }
}