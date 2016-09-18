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
        private static readonly string _patternPhoneNumberWithDisplayNameAfter;
        private static readonly string _patternPhoneNumberWithDisplayNameBefore;
        private static readonly string _patternUriOnly;
        private static readonly string _patternUriWithDisplayNameAfter;
        private static readonly string _patternUriWithDisplayNameBefore;

        #endregion

        #region private variables

        private readonly string _displayName;
        private readonly string _user;
        private readonly string _domain;
        private readonly bool _isNumeric;

        #endregion

        #region constructors

        static PhoneNumber()
        {
            _patternPhoneNumberOnly = $@"^({_charactersAllowedForPhoneNumber}*)$";
            _patternPhoneNumberWithDisplayNameAfter = $@"^({_charactersAllowedForPhoneNumber}*) \((.*)\)$";
            _patternPhoneNumberWithDisplayNameBefore = $@"^(.*) <({_charactersAllowedForPhoneNumber}*)>$";
            _patternUriOnly = $@"^({_charactersAllowedForAlphaNumericUser}*)@({_charactersAllowedForDomain}*)$";
            _patternUriWithDisplayNameAfter = $@"^({_charactersAllowedForAlphaNumericUser}*)@({_charactersAllowedForDomain}*) \((.*)\)$";
            _patternUriWithDisplayNameBefore = $@"^(.*) <({_charactersAllowedForAlphaNumericUser}*)@({_charactersAllowedForDomain}*)>$";
        }

        public PhoneNumber(string user, string domain, string displayName)
        {
            _isNumeric = IsNumericUser(user);
            _user = _isNumeric ? user : ParseNumericUser(user);
            _domain = domain;
            _displayName = displayName;
        }

        private PhoneNumber(string user, string domain, string displayName, bool isNumeric)
        {
            _isNumeric = isNumeric;
            _user = _isNumeric ? user : ParseNumericUser(user);
            _domain = domain;
            _displayName = displayName;
        }

        #endregion

        #region properties

        public string DisplayName => _displayName;
        public string User => _user;
        public string Domain => _domain;
        public bool IsAlphaNumeric => !IsNumeric;
        public bool IsNumeric => _isNumeric;
        public bool HasDomain => !string.IsNullOrEmpty(_domain);

        #endregion

        #region public static functions

        public static ParseResult<PhoneNumber> Parse(string data)
        {
            var match = Regex.Match(data, _patternPhoneNumberOnly);

            if (match.Success)
                return new ParseResult<PhoneNumber>(new PhoneNumber(match.Groups[1].Value, "", "", true));

            match = Regex.Match(data, _patternPhoneNumberWithDisplayNameAfter);

            if (match.Success)
                return new ParseResult<PhoneNumber>(new PhoneNumber(match.Groups[1].Value, "", match.Groups[2].Value, true));

            match = Regex.Match(data, _patternPhoneNumberWithDisplayNameBefore);

            if (match.Success)
                return new ParseResult<PhoneNumber>(new PhoneNumber(match.Groups[2].Value, "", match.Groups[1].Value, true));

            match = Regex.Match(data, _patternUriOnly);

            if (match.Success)
                return new ParseResult<PhoneNumber>(new PhoneNumber(match.Groups[1].Value, match.Groups[2].Value, ""));

            match = Regex.Match(data, _patternUriWithDisplayNameAfter);

            if (match.Success)
                return new ParseResult<PhoneNumber>(new PhoneNumber(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value));

            match = Regex.Match(data, _patternUriWithDisplayNameBefore);

            if (match.Success)
                return new ParseResult<PhoneNumber>(new PhoneNumber(match.Groups[2].Value, match.Groups[3].Value, match.Groups[1].Value));

            return new ParseResult<PhoneNumber>($"the phone number '{data}' is invalid");
        }

        #endregion

        #region private static functions

        private static string ParseNumericUser(string data)
        {
            return Regex.Replace(data, @"[\s\-\/\\]+", "");
        }

        private static bool IsNumericUser(string user)
        {
            return Regex.Match(user, _patternPhoneNumberOnly).Success;
        }

        #endregion
    }
}
