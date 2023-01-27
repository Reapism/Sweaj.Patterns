using System.Text.RegularExpressions;

namespace Sweaj.Patterns.Data.ValueObjects
{
    public sealed partial class Email
    {
        private readonly string _email;

        private Email(string email)
        {
            _email = email;
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
            return email._email;
        }

        public static explicit operator Email(string email)
        {
            return Create(email);
        }

        public override string ToString()
        {
            return _email;
        }

        public override bool Equals(object obj)
        {
            try
            {
                var addr = (Email)obj;
                return addr._email == _email;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return _email.GetHashCode();
        }

        [GeneratedRegex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
        private static partial Regex ValidateEmailRegex();
    }
}