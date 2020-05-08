using System;

namespace TechTest
{
    public interface IConnectedClient
    {
        string AddMessage(string message);
        void AlterMessage(string messageId, string NewMessage);
        string GetMessageById(string messageId);
        void DeleteMessage(Guid messageId);
        ClientMessage[] GetAllMessagesForClient();
    }
}
