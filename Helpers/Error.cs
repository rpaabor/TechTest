using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    [DataContract]
    public class Error
    {
        [DataMember]
        public string Message { get; set; }

        public Error(Exception e)
        {
            Message = e.Message;
        }
    }
}
