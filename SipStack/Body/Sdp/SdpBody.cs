using System;

namespace SipStack.Body.Sdp
{
    public class SdpBody : IBody
    {
        #region variables

        #endregion

        #region constructors

        public SdpBody(int protocolVersion, Originator originator)
        {
            Originator = originator;
        }

        #endregion

        #region properties

        public int ProtocolVersion { get; }
        public Originator Originator { get; }

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
