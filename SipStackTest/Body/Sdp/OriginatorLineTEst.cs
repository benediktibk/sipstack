﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;
using SipStack.Network;
using System.Net;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class OriginatorLineTest
    {
        [TestMethod]
        public void Parse_ValidExample_AllValuesAreCorrect()
        {
            var line = OriginatorLine.Parse(@"jdoe 2890844526 2890842807 IN IP4 10.47.16.5");

            var originatorLine = line.Result as OriginatorLine;
            originatorLine.Originator.Username.Should().Be("jdoe");
            originatorLine.Originator.SessionId.Should().Be(2890844526);
            originatorLine.Originator.SessionVersion.Should().Be(2890842807);
            originatorLine.Originator.NetType.Should().Be(NetType.Internet);
            originatorLine.Originator.AddressType.Should().Be(AddressType.Ipv4);
            var ipAddress = IPAddress.Parse("10.47.16.5");
            originatorLine.Originator.IpAddress.Should().Be(ipAddress);
        }

        [TestMethod]
        public void Parse_ValidExampleWithIpv6_IpIsCorrect()
        {
            var line = OriginatorLine.Parse(@"jdoe 2890844526 2890842807 IN IP6 2001:0db8:0000:0000:0000:ff00:0042:8329");

            var originatorLine = line.Result as OriginatorLine;
            originatorLine.Originator.AddressType.Should().Be(AddressType.Ipv6);
            var ipAddress = IPAddress.Parse("2001:db8::ff00:42:8329");
            originatorLine.Originator.IpAddress.Should().Be(ipAddress);
        }
    }
}
