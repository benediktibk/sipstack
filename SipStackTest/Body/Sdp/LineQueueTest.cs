using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;
using System;
using SipStack.Utils;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class LineQueueTest
    {
        [TestMethod]
        public void IsEmpty_NoLines_True()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[] { });

            lineQueue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_OneLine_True()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                new Tuple<char, string>('a', "asdf")
            });

            lineQueue.IsEmpty.Should().BeFalse();
        }

        [TestMethod]
        public void ParseMandatoryLine_Empty_ErrorResult()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[] { });

            var parseResult = lineQueue.ParseMandatoryLine('a', LineParsers.Attribute);
            
            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMandatoryLine_CorrectLineIsNext_SuccessResult()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[] 
            {
                new Tuple<char, string>('a', "asdf")
            });

            var parseResult = lineQueue.ParseMandatoryLine('a', LineParsers.Attribute);

            parseResult.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMandatoryLine_CorrectLineIsNext_CorrectAttribute()
        {
            var attribute = new Tuple<char, string>('a', "asdf");
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                attribute
            });

            var parseResult = lineQueue.ParseMandatoryLine('a', LineParsers.Attribute);

            parseResult.Result.Should().Be(attribute);
        }

        [TestMethod]
        public void ParseMandatoryLine_CorrectLineIsNext_IsEmpty()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                new Tuple<char, string>('a', "asdf")
            });

            var parseResult = lineQueue.ParseMandatoryLine('a', LineParsers.Attribute);

            lineQueue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMandatoryLine_WrongLineNext_ErrorResult()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                new Tuple<char, string>('u', "http://asdf.asdffd"),
                new Tuple<char, string>('a', "asdf")
            });

            var parseResult = lineQueue.ParseMandatoryLine('a', LineParsers.Attribute);

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMandatoryLine_TwoLinesInARow_BothSucceed()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                new Tuple<char, string>('u', "http://asdf.asdffd"),
                new Tuple<char, string>('a', "asdf")
            });

            var parseResultOne = lineQueue.ParseMandatoryLine('u', LineParsers.HttpUri);
            var parseResultTwo = lineQueue.ParseMandatoryLine('a', LineParsers.Attribute);

            parseResultOne.IsSuccess.Should().BeTrue();
            parseResultTwo.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void ParseOptionalLine_Empty_Null()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[] { });

            var result = lineQueue.ParseOptionalLine('a', LineParsers.Attribute);

            result.Should().BeNull();
        }

        [TestMethod]
        public void ParseOptionalLine_WrongLineTypeNext_Null()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                new Tuple<char, string>('u', "http://asdf.asdffd"),
                new Tuple<char, string>('a', "asdf")
            });

            var result = lineQueue.ParseOptionalLine('a', LineParsers.Attribute);

            result.Result.Should().BeNull();
        }

        [TestMethod]
        public void ParseOptionalLine_CorrectLineTypeNext_CorrectLine()
        {
            var uriLine = new Tuple<char, string>('u', "http://asdf.asdffd");
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                uriLine,
                new Tuple<char, string>('a', "asdf")
            });

            var result = lineQueue.ParseOptionalLine('u', LineParsers.HttpUri);

            result.Should().Be(uriLine);
        }

        [TestMethod]
        public void ParseOptionalLine_TwoLinesInARow_CorrectLinesAndEmpty()
        {
            var uriLine = new Tuple<char, string>('u', "http://asdf.asdffd");
            var attributeLine = new Tuple<char, string>('a', "asdf");
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                uriLine,
                attributeLine
            });

            var resultOne = lineQueue.ParseOptionalLine('u', LineParsers.HttpUri);
            var resultTwo = lineQueue.ParseOptionalLine('a', LineParsers.Attribute);

            resultOne.Should().Be(uriLine);
            resultTwo.Should().Be(attributeLine);
            lineQueue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMultipleOptionalLines_EmptyQueue_EmptyList()
        {
            var lineQueue = new LineQueue(new Tuple<char, string>[] { });

            var result = lineQueue.ParseMultipleOptionalLines('u', LineParsers.HttpUri);

            result.Result.Count.Should().Be(0);
        }

        [TestMethod]
        public void ParseMultipleOptionalLines_TwoLinesInARow_CorrectLinesAndEmpty()
        {
            var uriLineOne = new Tuple<char, string>('u', "http://asdf.asdffd");
            var uriLineTwo = new Tuple<char, string>('u', "https://qwer.asdffd");
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                uriLineOne,
                uriLineTwo
            });

            var result = lineQueue.ParseMultipleOptionalLines('u', LineParsers.HttpUri);

            result.Result.Count.Should().Be(2);
            result.Result[0].Should().Be(uriLineOne);
            result.Result[1].Should().Be(uriLineTwo);
            lineQueue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void ParseMultipleOptionalLines_TwoLinesInARowWithOtherLineAfter_CorrectLinesAndNotEmpty()
        {
            var uriLineOne = new Tuple<char, string>('u', "http://asdf.asdffd");
            var uriLineTwo = new Tuple<char, string>('u', "https://qwer.asdffd");
            var lineQueue = new LineQueue(new Tuple<char, string>[]
            {
                uriLineOne,
                uriLineTwo,
                new Tuple<char, string>('a', "asdf")
            });

            var result = lineQueue.ParseMultipleOptionalLines('u', LineParsers.HttpUri);

            result.Result.Count.Should().Be(2);
            result.Result[0].Should().Be(uriLineOne);
            result.Result[1].Should().Be(uriLineTwo);
            lineQueue.IsEmpty.Should().BeFalse();
        }
    }
}
