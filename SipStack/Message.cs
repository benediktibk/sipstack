using System;

namespace SipStack
{
    public class Message
    {
        public Message(IHeader header, IBody body)
        {
            if (header == null)
                throw new ArgumentNullException("header");

            if (body == null)
                throw new ArgumentNullException("body");

            Header = header;
            Body = body;

            if (Header.ContentLength != body.ContentLength)
                throw new ArgumentException("header", "value of field content length doesn't match the real content length");
        }

        public IHeader Header { get; private set; }
        public IBody Body { get; private set; }

        public override string ToString()
        {
            var messageBuilder = new MessageBuilder();
            Header.AddTo(messageBuilder);
            Body.AddTo(messageBuilder);
            return messageBuilder.ToString();
        }
    }
}
