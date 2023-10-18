using System.Text.RegularExpressions;

namespace Sweaj.Patterns.Data.ValueObjects
{
    public sealed partial class Email
    {
        private readonly string emailAddress;

        private Email(string email)
        {
            this.emailAddress = email;
        }

        public static Email Create(string email)
        {
            if (!IsValid(email))
            {
                throw new ArgumentException("Invalid email address", nameof(email));
            }

            return new Email(email);
        }

        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var emailRegex = ValidateEmailRegex();
            return emailRegex.IsMatch(email);
        }

        public static implicit operator string(Email email)
        {
            return email.emailAddress;
        }

        public static explicit operator Email(string email)
        {
            return Create(email);
        }

        public override string ToString()
        {
            return this.emailAddress;
        }

        public override bool Equals(object obj)
        {
            try
            {
                var addr = (Email)obj;
                return addr.emailAddress == this.emailAddress;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.emailAddress.GetHashCode();
        }

        [GeneratedRegex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
        private static partial Regex ValidateEmailRegex();
    }
}