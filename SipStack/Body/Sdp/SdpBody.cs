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
            var linesWithUsage = lines.Select(x => new LineWithUsage(x)).ToList();

            var originatorLines = linesWithUsage.Where(x => x.Line is OriginatorLine).ToList();

            if (originatorLines.Count() != 1)
                return new ParseResult<SdpBody>("there must be exactly one originator line");

            var originatorLine = originatorLines[0];

            sdpBody.Originator = originatorLine.Line as OriginatorLine;
            originatorLine.MarkAsUsed();

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
