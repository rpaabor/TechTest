using Helpers;
using System;

namespace TechTest
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChatService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ChatService.svc or ChatService.svc.cs at the Solution Explorer and start debugging.
    [Serializable()]
    public class ChatService : IChatService
    {
        public static InMemoryCashe _memoryCashe;
        public static Repository _repository;
        private IConnectedClient _client;

        public ChatService()
        {
            if (_memoryCashe == null)
                _memoryCashe = new InMemoryCashe();
            if (_repository == null)
                _repository = new Repository(_memoryCashe);
        }

        private void SetParams(string clientid)
        {
            try
            {
                _client = _repository.GetConnectedClient(clientid);
                if (_client == null)
                    _client = new ConnectedClient(clientid, _repository);
                _repository.AddClientId(clientid);
            }
            catch (Exception e)
            {
                throw ErrorHelper.LogAndWrapInFaultException(e);
            }

        }

        public string AddMessage(string message, string clientId)
        {
            try
            {
                SetParams(clientId);
                return _client.AddMessage(message);
            }
            catch (Exception e)
            {
                throw ErrorHelper.LogAndWrapInFaultException(e);
            }

        }

        public void AlterMessage(string newMessage, string messageId, string clientId)
        {
            try
            {
                SetParams(clientId);
                _client.AlterMessage(messageId, newMessage);
            }
            catch (Exception e)
            {
                throw ErrorHelper.LogAndWrapInFaultException(e);
            }
        }

        public void DeleteMessageById(string messageId, string clientId)
        {
            try
            {
                SetParams(clientId);
                _client.DeleteMessage(Guid.Parse(messageId));
            }
            catch (Exception e)
            {
                throw ErrorHelper.LogAndWrapInFaultException(e);
            }

        }

        public ClientMessage[] GetAllMessagesAllClients()
        {
            try
            {
                return _repository.GetAllMessagesAllClients();
            }
            catch (Exception e)
            {
                throw ErrorHelper.LogAndWrapInFaultException(e);
            }
        }

        public ClientMessage[] GetAllMessagesMadeByClient(string clientId)
        {
            try
            {
                SetParams(clientId);
                return _client.GetAllMessagesForClient();
            }
            catch (Exception e)
            {
                throw ErrorHelper.LogAndWrapInFaultException(e);
            }
        }

        public void ClientDissconnect(string clientId)
        {
            try
            {
                _repository.ClientDissconect(clientId);
            }
            catch (Exception e)
            {
                throw ErrorHelper.LogAndWrapInFaultException(e);
            }
        }
    }
}