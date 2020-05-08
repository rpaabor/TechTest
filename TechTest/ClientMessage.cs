using System;
using System.Runtime.Serialization;

namespace TechTest
{
    [DataContract()]
    public class ClientMessage
    {
        [DataMember]
        public string Message { get; private set; }
        [DataMember]
        public DateTime Ts { get; private set; }
        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public DateTime UpdTs { get; private set; }

        public ClientMessage(string message)
        {
            var time = DateTime.Now;
            Message = message;
            Ts = time;
            Id = Guid.NewGuid();
            UpdTs = time;
        }
        public void AlterMessage(string NewMessage)
        {
            Message = NewMessage;
            UpdTs = DateTime.Now;
        }
    }
}