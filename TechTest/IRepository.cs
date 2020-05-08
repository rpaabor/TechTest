namespace TechTest
{
    public interface IRepository
    {
        void SaveConnectedClient(ConnectedClient client);
        string GetMessageById(string messageId, ConnectedClient client);
        void UppdateConnectedClient(ConnectedClient client);
        ConnectedClient GetConnectedClient(string clientId);
        ClientMessage[] GetAllMessagesAllClients();
        void AddClientId(string clientid);
        void ClientDissconect(string clientId);
    }
}
