using System;
using System.Text.RegularExpressions;

namespace SipStack.Utils
{
    public class PhoneNumber
    {
        #region privateStaticVariables

        private static readonly string _charactersAllowedForPhoneNumber = @"[0-9\+\s\-\/\\]";
        private static readonly string _charactersAllowedForDomain = @"[^\s@]";
        private static readonly string _charactersAllowedForAlphaNumericUser = @"[^()<>]";
        private static readonly string _patternPhoneNumberOnly;
        private static readonly string _patternPhoneNumberWithDisplayNameAfter;
        private static readonly string _patternPhoneNumberWithDisplayNameBefore;
        private static readonly string _patternUriOnly;
        private static readonly string _patternUriWithDisplayNameAfter;
        private static readonly string _patternUriWithDisplayNameBefore;

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

        public PhoneNumber(string user, string domain, string displayName) :
            this(user, domain, displayName, IsNumericUser(user))
        { }

        private PhoneNumber(string user, string domain, string displayName, bool isNumeric)
        {
            IsNumeric = isNumeric;
            User = IsNumeric ? ParseNumericUser(user) : user;
            Domain = domain;
            DisplayName = displayName;
        }

        #endregion

        #region properties

        public string DisplayName { get; }
        public string User { get; }
        public string Domain { get; }
        public bool IsAlphaNumeric => !IsNumeric;
        public bool IsNumeric { get; }
        public bool HasDomain => !string.IsNullOrEmpty(Domain);
        public bool HasDisplayName => !string.IsNullOrEmpty(DisplayName);

        #endregion

        #region public functions

        public override string ToString()
        {
            if (HasDomain)
            {
                if (HasDisplayName)
                    return $"{DisplayName} <{User}@{Domain}>";
                else
                    return $"{User}@{Domain}";
            }
            else
            {
                if (HasDisplayName)
                    return $"{DisplayName} <{User}>";
                else
                    return $"{User}";
            }
        }

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
