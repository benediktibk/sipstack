using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.Text;
using SipStack.Header;

namespace SipStackTest.Header
{
    [TestClass]
    public class FieldNameTest
    {
        [TestMethod]
        public void IsCustomField_CustomFieldName_True()
        {
            var headerFieldName = new FieldName("blub");

            headerFieldName.IsCustomField.Should().BeTrue();
        }

        [TestMethod]
        public void IsCustomField_StandardisedFieldName_False()
        {
            var headerFieldName = new FieldName(FieldType.AcceptEncoding);

            headerFieldName.IsCustomField.Should().BeFalse();
        }

        [TestMethod]
        public void Type_AcceptEncoding_AcceptEncoding()
        {
            var headerFieldName = new FieldName(FieldType.AcceptEncoding);

            headerFieldName.Type.Should().Be(FieldType.AcceptEncoding);
        }

        [TestMethod]
        public void Type_CustomField_ThrowsException()
        {
            var headerFieldName = new FieldName("asdf");

            Action action = () => { var type = headerFieldName.Type; };

            action.ShouldThrow<Exception>();
        }

        [TestMethod]
        public void ToString_CustomFieldNameBlub_Blub()
        {
            var headerFieldName = new FieldName("blub");

            headerFieldName.ToString().Should().Be("blub");
        }

        [TestMethod]
        public void ToString_AcceptEncoding_AcceptEncoding()
        {
            var headerFieldName = new FieldName(FieldType.AcceptEncoding);

            headerFieldName.ToString().Should().Be("Accept-Encoding");
        }

        [TestMethod]
        public void CanHaveMultipleValues_From_False()
        {
            var headerFieldName = new FieldName(FieldType.From);

            headerFieldName.CanHaveMultipleValues.Should().BeFalse();
        }

        [TestMethod]
        public void CanHaveMultipleValues_Route_True()
        {
            var headerFieldName = new FieldName(FieldType.Route);

            headerFieldName.CanHaveMultipleValues.Should().BeTrue();
        }

        [TestMethod]
        public void IsOfType_RouteAndParamFrom_False()
        {
            var headerFieldName = new FieldName(FieldType.Route);

            headerFieldName.IsOfType(FieldType.From).Should().BeFalse();
        }

        [TestMethod]
        public void IsOfType_RouteAndParamRoute_True()
        {
            var headerFieldName = new FieldName(FieldType.Route);

            headerFieldName.IsOfType(FieldType.Route).Should().BeTrue();
        }

        [TestMethod]
        public void IsOfType_CustomFieldAndParamRoute_False()
        {
            var headerFieldName = new FieldName("asdf");

            headerFieldName.IsOfType(FieldType.Route).Should().BeFalse();
        }

        [TestMethod]
        public void EqualsWithObject_Null_False()
        {
            var headerFieldName = new FieldName("asdf");
            object rhs = null;

            headerFieldName.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void EqualsWithType_Null_False()
        {
            var headerFieldName = new FieldName("asdf");
            FieldName rhs = null;

            headerFieldName.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void EqualsWithObject_WrongType_False()
        {
            var headerFieldName = new FieldName("asdf");
            StringBuilder rhs = null;

            headerFieldName.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_CustomTypeWithRoute_False()
        {
            var lhs = new FieldName("asdf");
            var rhs = new FieldName(FieldType.Route);

            lhs.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_RouteWithCustomType_False()
        {
            var rhs = new FieldName("asdf");
            var lhs = new FieldName(FieldType.Route);

            lhs.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_CustomTypeWithDifferentCustomType_False()
        {
            var lhs = new FieldName("asdf");
            var rhs = new FieldName("aer");

            lhs.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_CustomTypeWithSameCustomType_True()
        {
            var lhs = new FieldName("asdf");
            var rhs = new FieldName("asdf");

            lhs.Equals(rhs).Should().BeTrue();
        }

        [TestMethod]
        public void Equals_RouteWithFrom_False()
        {
            var lhs = new FieldName(FieldType.Route);
            var rhs = new FieldName(FieldType.From);

            lhs.Equals(rhs).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_RouteWithRoute_True()
        {
            var lhs = new FieldName(FieldType.Route);
            var rhs = new FieldName(FieldType.Route);

            lhs.Equals(rhs).Should().BeTrue();
        }
    }
}
