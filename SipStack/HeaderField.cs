using System.Collections.Generic;

namespace SipStack
{
    public class HeaderField
    {
        public HeaderField()
        {
            Values = new List<string>();
        }

        public HeaderFieldName Name { get; set; }
        public IList<string> Values { get; set; }
    }
}
