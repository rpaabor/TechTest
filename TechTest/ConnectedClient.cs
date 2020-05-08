using System;
using System.Collections.Generic;
using System.Linq;

namespace TechTest
{
    public class ConnectedClient : IConnectedClient
    {
        internal Guid ClientId { get;private set; }
        internal List<ClientMessage> Messages { get; private set; } = new List<ClientMessage>();
        private IRepository _repository;

        public ConnectedClient(string clientid,Repository repository)
        {
            _repository = repository;
            ClientId = Guid.Parse(clientid);
        }
        public string AddMessage(string message)
        {
            try
            {
                var clientMessage = new ClientMessage(message);
                Messages.Add(clientMessage);
                _repository.SaveConnectedClient(this);
                return clientMessage.Id.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong sending message to server, please try again.");
            }
            
        }
        public void AlterMessage(string messageId,string NewMessage)
        {
            var message = Messages.FirstOrDefault(c => c.Id == Guid.Parse(messageId));
            if (message == null)
                throw new NullReferenceException("No message, check message id!");
            message.AlterMessage(NewMessage);
            _repository.UppdateConnectedClient(this);
        }
        public string GetMessageById(string messageId)
        {

            return _repository.GetMessageById(messageId, this);
        }
        public void DeleteMessage(Guid messageId)
        {
            try
            {
                Messages.Remove(Messages.First(c => c.Id == messageId));
            }
            catch (Exception ex)
            {
                throw new NullReferenceException("Message not found!");
            }
        }

        public ClientMessage[] GetAllMessagesForClient()
        {
            return Messages.ToArray();
        }
    }
}