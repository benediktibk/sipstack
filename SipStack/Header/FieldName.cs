﻿using System;
using System.Collections.Generic;

namespace SipStack.Header
{
    public class FieldName
    {
        private FieldType _type;
        private bool _isCustomField;
        private string _customFieldName;

        public FieldName(string fieldName)
        {
            _isCustomField = !HeaderFieldTypeUtils.TryParse(fieldName, out _type);
            _customFieldName = fieldName;
        }

        public FieldName(FieldType type)
        {
            _isCustomField = false;
            _type = type;
        }

        public bool IsCustomField => _isCustomField;

        public FieldType Type
        {
            get
            {
                if (IsCustomField)
                    throw new InvalidOperationException("a custom field does not have a specified type");

                return _type;
            }
        }

        public override string ToString()
        {
            if (IsCustomField)
                return _customFieldName;

            return _type.ToFriendlyString();
        }

        public bool CanHaveMultipleValues
        {
            get
            {
                if (IsCustomField)
                    return true;

                return HeaderFieldTypeUtils.CanHaveMultipleValues(_type);
            }
        }

        public bool IsOfType(FieldType type)
        {
            if (IsCustomField)
                return false;

            return _type == type;
        }

        public bool IsContainedIn(HashSet<FieldType> types)
        {
            if (IsCustomField)
                return false;

            return types.Contains(_type);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is FieldName))
                return false;

            var rhs = obj as FieldName;
            return EqualsInternal(rhs);
        }

        public bool Equals(FieldName rhs)
        {
            if (rhs == null)
                return false;

            return EqualsInternal(rhs);
        }

        public override int GetHashCode()
        {
            if (IsCustomField)
                return _customFieldName.GetHashCode();
            else
                return _type.GetHashCode();
        }

        private bool EqualsInternal(FieldName rhs)
        {
            if (IsCustomField != rhs.IsCustomField)
                return false;

            if (IsCustomField)
                return _customFieldName == rhs._customFieldName;
            else
                return _type == rhs._type;

        }
    }
}
