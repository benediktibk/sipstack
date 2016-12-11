using System;

namespace SipStack
{
    public class Message
    {
        public Message(Header.Header header, IBody body)
        {
            if (header == null)
                throw new ArgumentNullException("header");

            if (body == null)
                throw new ArgumentNullException("body");

            Header = header;
            Body = body;
        }

        public Header.Header Header { get; }
        public IBody Body { get; }

        public override string ToString()
        {
            var messageBuilder = new MessageBuilder();
            Header.AddTo(messageBuilder);
            Body.AddTo(messageBuilder);
            return messageBuilder.ToString();
        }
    }
}
