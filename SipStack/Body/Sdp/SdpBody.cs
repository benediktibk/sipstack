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

        public SdpBody()
        { }
        #endregion

        #region properties

        public OriginatorLine Originator { get; private set; }
        public SessionNameLine SessionName { get; private set; }
        public DescriptionLine Description { get; private set; }
        public UriLine Uri { get; private set; }

        public int ContentLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region public functions

        public static ParseResult<SdpBody> Parse(IList<ILine> lines)
        {
            var sdpBody = new SdpBody();
            /*var linesLeftOver = lines.Select(x => new Tuple<bool, >)

            if (originatorNode == null)
                return new ParseResult<SdpBody>("the originator line is missing");

            sdpBody.Originator = originatorNodes[0].*/

            throw new NotImplementedException();

            return new ParseResult<SdpBody>(sdpBody);
        }

        public void AddTo(MessageBuilder messageBuilder)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
