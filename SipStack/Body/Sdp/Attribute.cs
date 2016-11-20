namespace SipStack.Body.Sdp
{
    public class Attribute
    {
        public Attribute(string name)
        {
            Name = name;
            Value = "";
        }

        public Attribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }
}
