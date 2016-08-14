using System.Collections.Generic;

namespace SipStack
{
    public class HeaderField
    {
        public HeaderField()
        {
            Name = "";
            Values = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
}
