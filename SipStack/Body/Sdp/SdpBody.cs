using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class SdpBody : IBody
    {
        #region constructors

        public SdpBody(int protocolVersion, Originator originator, string sessionName, string sessionDescription, Uri sessionUri, EmailAddress emailAddress, PhoneNumber phoneNumber)
        {
            Originator = originator;
            SessionName = sessionName;
            SessionDescription = sessionDescription;
            SessionUri = sessionUri;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }

        #endregion

        #region properties

        public int ProtocolVersion { get; }
        public Originator Originator { get; }
        public string SessionName { get; }
        public string SessionDescription { get; }
        public Uri SessionUri { get; }
        public EmailAddress EmailAddress { get; }
        public PhoneNumber PhoneNumber { get; }

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
