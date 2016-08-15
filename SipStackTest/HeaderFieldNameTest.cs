using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using FluentAssertions;
using System;
using System.Text;

namespace SipStackTest
{
    [TestClass]
    public class HeaderFieldNameTest
    {
        [TestMethod]
        public void IsCustomField_CustomFieldName_True()
        {
            var headerFieldName = new HeaderFieldName("blub");

            headerFieldName.IsCustomField.Should().BeTrue();
        }

        [TestMethod]
        public void IsCustomField_StandardisedFieldName_False()
        {
            var headerFieldName = new HeaderFieldName(HeaderFieldType.AcceptEncoding);

            headerFieldName.IsCustomField.Should().BeFalse();
        }

        [TestMethod]
        public void Type_AcceptEncoding_AcceptEncoding()
        {
            var headerFieldName = new HeaderFieldName(HeaderFieldType.AcceptEncoding);

            headerFieldName.Type.Should().Be(HeaderFieldType.AcceptEncoding);
        }

        [TestMethod]
        public void Type_CustomField_ThrowsException()
        {
            var headerFieldName = new HeaderFieldName("asdf");

            Action action = () => { var type = headerFieldName.Type; };

            action.ShouldThrow<Exception>();
        }

        [TestMethod]
        public void ToString_CustomFieldNameBlub_Blub()
        {
            var headerFieldName = new HeaderFieldName("blub");

            headerFieldName.ToString().Should().Be("blub");
        }

        [TestMethod]
        public void ToString_AcceptEncoding_AcceptEncoding()
        {
            var headerFieldName = new HeaderFieldName(HeaderFieldType.AcceptEncoding);

            headerFieldName.ToString().Should().Be("Accept-Encoding");
        }

        [TestMethod]
        public void CanHaveMultipleValues_From_False()
        {
            var headerFieldName = new HeaderFieldName(HeaderFieldType.From);

            headerFieldName.CanHaveMultipleValues.Should().BeFalse();
        }

        [TestMethod]
        public void CanHaveMultipleValues_Route_True()
        {
            var headerFieldName = new HeaderFieldName(HeaderFieldType.Route);

            headerFieldName.CanHaveMultipleValues.Should().BeTrue();
        }

        [TestMethod]
        public void IsOfType_RouteAndParamFrom_False()
        {
            var headerFieldName = new HeaderFieldName(HeaderFieldType.Route);

            headerFieldName.isOfType(HeaderFieldType.From).Should().BeFalse();
        }

        [TestMethod]
        public void IsOfType_RouteAndParamRoute_True()
        {
            var headerFieldName = new HeaderFieldName(HeaderFieldType.Route);

            headerFieldName.isOfType(HeaderFieldType.Route).Should().BeTrue();
        }

        [TestMethod]
        public void IsOfType_CustomFieldAndParamRoute_False()
        {
            var headerFieldName = new HeaderFieldName("asdf");

            headerFieldName.isOfType(HeaderFieldType.Route).Should().BeFalse();
        }

        [TestMethod]
        public void EqualsWithObject_Null_False()
        {
            var headerFieldName = new HeaderFieldName("asdf");
            object rhs = null;

            headerFieldName.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void EqualsWithType_Null_False()
        {
            var headerFieldName = new HeaderFieldName("asdf");
            HeaderFieldName rhs = null;

            headerFieldName.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void EqualsWithObject_WrongType_False()
        {
            var headerFieldName = new HeaderFieldName("asdf");
            StringBuilder rhs = null;

            headerFieldName.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_CustomTypeWithRoute_False()
        {
            var lhs = new HeaderFieldName("asdf");
            var rhs = new HeaderFieldName(HeaderFieldType.Route);

            lhs.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_RouteWithCustomType_False()
        {
            var rhs = new HeaderFieldName("asdf");
            var lhs = new HeaderFieldName(HeaderFieldType.Route);

            lhs.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_CustomTypeWithDifferentCustomType_False()
        {
            var lhs = new HeaderFieldName("asdf");
            var rhs = new HeaderFieldName("aer");

            lhs.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_CustomTypeWithSameCustomType_True()
        {
            var lhs = new HeaderFieldName("asdf");
            var rhs = new HeaderFieldName("asdf");

            lhs.Equals(rhs).Should().BeTrue();
        }

        [TestMethod]
        public void Equals_RouteWithFrom_False()
        {
            var lhs = new HeaderFieldName(HeaderFieldType.Route);
            var rhs = new HeaderFieldName(HeaderFieldType.From);

            lhs.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_RouteWithRoute_True()
        {
            var lhs = new HeaderFieldName(HeaderFieldType.Route);
            var rhs = new HeaderFieldName(HeaderFieldType.Route);

            lhs.Equals(rhs).Should().BeTrue();
        }
    }
}
