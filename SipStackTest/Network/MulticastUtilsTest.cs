using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using FluentAssertions;
using SipStack.Network;

namespace SipStackTest.Network
{
    [TestClass]
    public class MulticastUtilsTest
    {
        [TestMethod]
        public void IsMulticast_MulticastIpv4_True()
        {
            var ipAddress = IPAddress.Parse("224.0.2.0");

            var result = MulticastUtils.IsMulticast(ipAddress);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsMulticast_UnicastIpv4_False()
        {
            var ipAddress = IPAddress.Parse("8.8.8.8");

            var result = MulticastUtils.IsMulticast(ipAddress);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsMulticast_MulticastIpv6_True()
        {
            var ipAddress = IPAddress.Parse("FF02:0:0:0:0:0:0:C");

            var result = MulticastUtils.IsMulticast(ipAddress);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsMulticast_UnicastIpv6_False()
        {
            var ipAddress = IPAddress.Parse("0302:0:0:0:0:0:0:C");

            var result = MulticastUtils.IsMulticast(ipAddress);

            result.Should().BeFalse();
        }
    }
}
