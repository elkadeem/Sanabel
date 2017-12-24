using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.MVCCommon
{
    public class UIMessage
    {
        public UIMessage(string message, MessageType messageType = MessageType.Information)
        {
            this.Message = message;
            this.MessageType = messageType;
        }

        public string Message { get; private  set; }

        public MessageType MessageType { get; private set; }
    }

    public enum MessageType
    {
        Success,
        Error,
        Information,
        Warrning,

    }
}
