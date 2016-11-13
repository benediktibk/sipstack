using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class SdpBody : IBody
    {
        #region variables

        #endregion

        #region constructors

        public SdpBody(OriginatorLine originator)
        {
            Originator = originator;
        }

        #endregion

        #region properties

        public OriginatorLine Originator { get; }
        public SessionNameLine SessionName { get; }
        public DescriptionLine Description { get; }
        public UriLine Uri { get; }

        public int ContentLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region public functions

        public void AddTo(MessageBuilder messageBuilder)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
