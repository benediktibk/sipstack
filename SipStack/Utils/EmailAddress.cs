using System;
using System.Text.RegularExpressions;

namespace SipStack.Utils
{
    public class EmailAddress
    {
        #region constructors

        public EmailAddress(string localPart, string domain, string displayName)
        {
            if (!IsValidDomain(domain))
                throw new ArgumentOutOfRangeException("domain");

            if (!IsValidLocalPart(localPart))
                throw new ArgumentOutOfRangeException("localPart");

            if (!IsValidDisplayName(displayName))
                throw new ArgumentOutOfRangeException("displayName");

            LocalPart = localPart;
            Domain = domain;
            DisplayName = displayName;
        }

        #endregion

        #region properties

        public string LocalPart { get; }
        public string Domain { get; }
        public string DisplayName { get; }
        public bool HasDomain => !string.IsNullOrEmpty(Domain);
        public bool HasDisplayName => !string.IsNullOrEmpty(DisplayName);

        #endregion

        #region public static functions

        public static bool IsValidDomain(string domain)
        {
            var pattern = @"^[^@ <>()]*$";
            return Regex.Match(domain, pattern).Success;
        }

        public static bool IsValidLocalPart(string localPart)
        {
            var pattern = @"^[^ <>()]*$";
            return Regex.Match(localPart, pattern).Success;
        }

        public static bool IsValidDisplayName(string displayName)
        {
            var pattern = @"^[^<>()]*$";
            return Regex.Match(displayName, pattern).Success;
        }

        public static ParseResult<EmailAddress> Parse(string data)
        {
            var patternOne = @"^([^ ]*)@([^@ ]*)$"; // j.doe@example.com
            var patternTwo = @"^([^ ]*)@([^@ ]*) \((.*)\)$"; // j.doe@example.com (Jane Doe)
            var patternThree = @"^(.*) <([^ ]*)@([^@ ]*)>$"; // Jane Doe <j.doe@example.com>
            var matchesOne = Regex.Matches(data, patternOne);

            if (matchesOne.Count == 1)
            {
                var groups = matchesOne[0].Groups;
                return CreateAndCheck(groups[1].Value, groups[2].Value, "");
            }

            var matchesTwo = Regex.Matches(data, patternTwo);

            if (matchesTwo.Count == 1)
            {
                var groups = matchesTwo[0].Groups;
                return CreateAndCheck(groups[1].Value, groups[2].Value, groups[3].Value);
            }

            var matchesThree = Regex.Matches(data, patternThree);

            if (matchesThree.Count == 1)
            {
                var groups = matchesThree[0].Groups;
                return CreateAndCheck(groups[2].Value, groups[3].Value, groups[1].Value);
            }

            return new ParseResult<EmailAddress>($"email address '{data}' is malformed");
        }

        #endregion

        #region public functions

        public override string ToString()
        {
            if (HasDomain)
            {
                if (HasDisplayName)
                    return $"{DisplayName} <{LocalPart}@{Domain}>";
                else
                    return $"{LocalPart}@{Domain}";
            }
            else
            {
                if (HasDisplayName)
                    return $"{DisplayName} <{LocalPart}>";
                else
                    return $"{LocalPart}";
            }
        }

        #endregion

        #region private static functions

        private static ParseResult<EmailAddress> CreateAndCheck(string localPart, string domain, string displayName)
        {
            if (!IsValidDisplayName(displayName))
                return new ParseResult<EmailAddress>($"the display name '{displayName}' contains invalid characters");

            if (!IsValidLocalPart(localPart))
                return new ParseResult<EmailAddress>($"the local part '{localPart}' contains invalid characters");

            if (!IsValidDomain(domain))
                return new ParseResult<EmailAddress>($"the domain '{domain}' contains invalid characters");

            return new ParseResult<EmailAddress>(new EmailAddress(localPart, domain, displayName));
        }

        #endregion
    }
}
