using System;
using System.Collections.Generic;

namespace SipStack.Body.Sdp
{
    public class SdpBody : IBody
    {
        #region variables
        #endregion

        #region constructors
        #endregion

        #region properties

        public int ContentLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region public functions

        public static ParseResult<SdpBody> CreateFrom(IList<ILine> lines)
        {
            throw new NotImplementedException();
        }

        public void AddTo(MessageBuilder messageBuilder)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
