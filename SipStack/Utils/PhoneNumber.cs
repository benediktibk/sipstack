using System;
using System.Text.RegularExpressions;

namespace SipStack.Utils
{
    public class PhoneNumber
    {
        #region privateStaticVariables

        private static readonly string _charactersAllowedForPhoneNumber = @"[0-9\+ \-\/\\]";
        private static readonly string _charactersAllowedForDomain = @"[^ @]";
        private static readonly string _charactersAllowedForAlphaNumericUser = @"[^ ()<>]";
        private static readonly string _patternPhoneNumberOnly;
        private static readonly string _patternPhoneNumberWithDomain;
        private static readonly string _patternPhoneNumberWithDomainAndDisplayNameAfter;
        private static readonly string _patternPhoneNumberWithDomainAndDisplayNameBefore;
        private static readonly string _patternAlphaNumericOnly;
        private static readonly string _patternAlphaNumericWithDisplayNameAfter;
        private static readonly string _patternAlphaNumericWithDisplayNameBefore;

        #endregion

        #region private variables

        private readonly string _displayName;
        private readonly string _user;
        private readonly string _domain;
        private readonly bool _isAlphaNumeric;

        #endregion

        #region constructors

        static PhoneNumber()
        {
            _patternPhoneNumberOnly = $@"^({_charactersAllowedForPhoneNumber}*)$";
            _patternPhoneNumberWithDomain = $@"^({_charactersAllowedForPhoneNumber}*)@({_charactersAllowedForDomain}*)$";
            _patternPhoneNumberWithDomainAndDisplayNameAfter = $@"^({_charactersAllowedForPhoneNumber}*)@({_charactersAllowedForDomain}*) \((.*)\)$";
            _patternPhoneNumberWithDomainAndDisplayNameBefore = $@"^(.*) <({_charactersAllowedForPhoneNumber}*)@({_charactersAllowedForDomain}*)>$";
            _patternAlphaNumericOnly = $@"^({_charactersAllowedForAlphaNumericUser}*)@({_charactersAllowedForDomain}*)$";
            _patternAlphaNumericWithDisplayNameAfter = $@"^({_charactersAllowedForAlphaNumericUser}*)@({_charactersAllowedForDomain}*) \((.*)\)$";
            _patternAlphaNumericWithDisplayNameBefore = $@"^(.*) <({_charactersAllowedForAlphaNumericUser}*)@({_charactersAllowedForDomain}*)>$";
        }

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
            var matchPhoneNumberOnly = Regex.Match(data, _patternPhoneNumberOnly);

            if (matchPhoneNumberOnly.Success)
            {
                var user = ParseNumericUser()
            }
        }

        #endregion

        #region private static functions

        private static string ParseNumericUser(string data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
