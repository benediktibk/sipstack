using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class LineQueueTest
    {
        [TestMethod]
        public void IsEmpty_NoLines_True()
        {
            var lineQueue = new LineQueue(new ILine[] { });

            lineQueue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_OneLine_True()
        {
            var lineQueue = new LineQueue(new ILine[]
            {
                new AttributeLine("asdf")
            });

            lineQueue.IsEmpty.Should().BeFalse();
        }

        [TestMethod]
        public void ParseMandatoryLine_Empty_ErrorResult()
        {
            var lineQueue = new LineQueue(new ILine[] { });

            var parseResult = lineQueue.ParseMandatoryLine<AttributeLine>();
            
            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMandatoryLine_CorrectLineIsNext_SuccessResult()
        {
            var lineQueue = new LineQueue(new ILine[] 
            {
                new AttributeLine("asdf")
            });

            var parseResult = lineQueue.ParseMandatoryLine<AttributeLine>();

            parseResult.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMandatoryLine_CorrectLineIsNext_CorrectAttribute()
        {
            var attribute = new AttributeLine("asdf");
            var lineQueue = new LineQueue(new ILine[]
            {
                attribute
            });

            var parseResult = lineQueue.ParseMandatoryLine<AttributeLine>();

            parseResult.Result.Should().Be(attribute);
        }

        [TestMethod]
        public void ParseMandatoryLine_CorrectLineIsNext_IsEmpty()
        {
            var lineQueue = new LineQueue(new ILine[]
            {
                new AttributeLine("asdf")
            });

            var parseResult = lineQueue.ParseMandatoryLine<AttributeLine>();

            lineQueue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMandatoryLine_WrongLineNext_ErrorResult()
        {
            var lineQueue = new LineQueue(new ILine[]
            {
                new UriLine(new System.Uri("http://asdf.asdffd")),
                new AttributeLine("asdf")
            });

            var parseResult = lineQueue.ParseMandatoryLine<AttributeLine>();

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMandatoryLine_TwoLinesInARow_BothSucceed()
        {
            var lineQueue = new LineQueue(new ILine[]
            {
                new UriLine(new System.Uri("http://asdf.asdffd")),
                new AttributeLine("asdf")
            });

            var parseResultOne = lineQueue.ParseMandatoryLine<UriLine>();
            var parseResultTwo = lineQueue.ParseMandatoryLine<AttributeLine>();

            parseResultOne.IsSuccess.Should().BeTrue();
            parseResultTwo.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void ParseOptionalLine_Empty_Null()
        {
            var lineQueue = new LineQueue(new ILine[] { });

            var result = lineQueue.ParseOptionalLine<AttributeLine>();

            result.Should().BeNull();
        }

        [TestMethod]
        public void ParseOptionalLine_WrongLineTypeNext_Null()
        {
            var lineQueue = new LineQueue(new ILine[]
            {
                new UriLine(new System.Uri("http://asdf.asdffd")),
                new AttributeLine("asdf")
            });

            var result = lineQueue.ParseOptionalLine<AttributeLine>();

            result.Should().BeNull();
        }

        [TestMethod]
        public void ParseOptionalLine_CorrectLineTypeNext_CorrectLine()
        {
            var uriLine = new UriLine(new System.Uri("http://asdf.asdffd"));
            var lineQueue = new LineQueue(new ILine[]
            {
                uriLine,
                new AttributeLine("asdf")
            });

            var result = lineQueue.ParseOptionalLine<UriLine>();

            result.Should().Be(uriLine);
        }

        [TestMethod]
        public void ParseOptionalLine_TwoLinesInARow_CorrectLinesAndEmpty()
        {
            var uriLine = new UriLine(new System.Uri("http://asdf.asdffd"));
            var attributeLine = new AttributeLine("asdf");
            var lineQueue = new LineQueue(new ILine[]
            {
                uriLine,
                attributeLine
            });

            var resultOne = lineQueue.ParseOptionalLine<UriLine>();
            var resultTwo = lineQueue.ParseOptionalLine<AttributeLine>();

            resultOne.Should().Be(uriLine);
            resultTwo.Should().Be(attributeLine);
            lineQueue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMultipleOptionalLines_EmptyQueue_EmptyList()
        {
            var lineQueue = new LineQueue(new ILine[] { });

            var result = lineQueue.ParseMultipleOptionalLines<UriLine>();

            result.Count.Should().Be(0);
        }

        [TestMethod]
        public void ParseMultipleOptionalLines_TwoLinesInARow_CorrectLinesAndEmpty()
        {
            var uriLineOne = new UriLine(new System.Uri("http://asdf.asdffd"));
            var uriLineTwo = new UriLine(new System.Uri("https://qwer.asdffd"));
            var lineQueue = new LineQueue(new ILine[]
            {
                uriLineOne,
                uriLineTwo
            });

            var result = lineQueue.ParseMultipleOptionalLines<UriLine>();

            result.Count.Should().Be(2);
            result[0].Should().Be(uriLineOne);
            result[1].Should().Be(uriLineTwo);
            lineQueue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMultipleOptionalLines_TwoLinesInARowWithOtherLineAfter_CorrectLinesAndNotEmpty()
        {
            var uriLineOne = new UriLine(new System.Uri("http://asdf.asdffd"));
            var uriLineTwo = new UriLine(new System.Uri("https://qwer.asdffd"));
            var lineQueue = new LineQueue(new ILine[]
            {
                uriLineOne,
                uriLineTwo,
                new AttributeLine("asdf")
            });

            var result = lineQueue.ParseMultipleOptionalLines<UriLine>();

            result.Count.Should().Be(2);
            result[0].Should().Be(uriLineOne);
            result[1].Should().Be(uriLineTwo);
            lineQueue.IsEmpty.Should().BeFalse();
        }
    }
}
