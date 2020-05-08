using System.ServiceModel;

namespace TechTest
{
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        string AddMessage(string message, string clientId);
        [OperationContract]
        void AlterMessage(string newMessage, string messageId, string clientId);
        [OperationContract]
        void DeleteMessageById(string messageId, string clientId);
        [OperationContract]
        ClientMessage[] GetAllMessagesMadeByClient(string clientId);
        [OperationContract]
        ClientMessage[] GetAllMessagesAllClients();
        [OperationContract]
        void ClientDissconnect(string clientId);
    }
}
