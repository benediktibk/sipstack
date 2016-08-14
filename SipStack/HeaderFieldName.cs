using System;

namespace SipStack
{
    public class HeaderFieldName
    {
        private HeaderFieldType _type;
        private bool _isCustomField;
        private string _customFieldName;

        public HeaderFieldName(string fieldName)
        {
            _isCustomField = !HeaderFieldTypeUtils.TryParse(fieldName, out _type);
            _customFieldName = fieldName;
        }

        public HeaderFieldName(HeaderFieldType type)
        {
            _isCustomField = false;
            _type = type;
        }

        public bool IsCustomField => _isCustomField;

        public HeaderFieldType Type
        {
            get
            {
                if (!IsCustomField)
                    throw new InvalidOperationException("a custom field does not have a specified type");

                return _type;
            }
        }

        public override string ToString()
        {
            if (_isCustomField)
                return _customFieldName;

            return _type.ToString();
        }

        public bool CanHaveMultipleValues()
        {
            if (_isCustomField)
                return false;

            return HeaderFieldTypeUtils.CanHaveMultipleValues(_type);
        }
    }
}
