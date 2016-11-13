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

        public SdpBody(OriginatorLine originator, SessionNameLine sessionName, DescriptionLine sessionDescription, UriLine uri)
        {
            if (sessionName == null)
                throw new ArgumentNullException("sessionName");

            Originator = originator;
            SessionName = sessionName;
            Description = sessionDescription;
            Uri = uri;
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
