using System;

namespace SipStack.Utils
{
    public class PhoneNumber
    {
        #region private variables

        private readonly string _displayName;
        private readonly string _user;
        private readonly string _domain;
        private readonly bool _isAlphaNumeric;

        #endregion

        #region constructors

        #endregion

        #region properties

        public string DisplayName => _displayName;
        public string User => _user;
        public string Domain => _domain;
        public bool IsAlphaNumeric => _isAlphaNumeric;
        public bool HasDomain => !string.IsNullOrEmpty(_domain);

        #endregion

        #region public static functions

        public static ParseResult<PhoneNumber> Parse(string data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
