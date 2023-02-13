using System.Text.RegularExpressions;

namespace Sweaj.Patterns.Data.ValueObjects
{
    public sealed partial class Address
    {
        private readonly string _street;
        private readonly string _city;
        private readonly string _state;
        private readonly string _zip;
        private readonly string _country;

        public string Street => _street;
        public string City => _city;
        public string State => _state;
        public string Zip => _zip;
        public string Country => _country;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private Address(string street, string city, string state, string zip, string country)
        {
            _street = street;
            _city = city;
            _state = state;
            _zip = zip;
            _country = country;
        }

        public static Address Create(string street, string city, string state, string zip, string country)
        {
            //validate the address format using regular expressions
            if (!Regex.IsMatch(Guard.Against.NullOrWhiteSpace(zip), @"^\d{5}(?:[-\s]\d{4})?$"))
            {
                throw new ArgumentException("Invalid Zip code format");
            }
            return new Address(Guard.Against.NullOrWhiteSpace(street), Guard.Against.NullOrWhiteSpace(city), Guard.Against.NullOrWhiteSpace(state), zip, Guard.Against.NullOrWhiteSpace(country));
        }

        public static bool ValidatePostalCode(string postalCode)
        {
            var postalCodeRegex = PostalCodeValidationRegex();
            return postalCodeRegex.IsMatch(postalCode);
        }

        public void Geocode()
        {
            // use a geocoding library or API to convert the address
            // into latitude and longitude coordinates
        }

        public bool IsInCountry(string country)
        {
            return Country.Equals(country, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            var address = obj as Address;
            if (address is null)
            {
                return false;
            }

            return _street == address._street &&
                   _city == address._city &&
                   _state == address._state &&
                   _zip == address._zip &&
                   _country == address._country;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_street, _city, _state, _zip, _country);
        }

        public override string ToString()
        {
            return GetFormattedAddress();
        }

        public string ToString(AddressFormat addressFormat)
        {
            return GetFormattedAddress(addressFormat);
        }

        private string GetFormattedAddress()
        {
            return $"{_street}, {_city}, {_state}, {_zip}, {_country}";
        }

        private string GetFormattedAddress(AddressFormat format)
        {
            switch (format)
            {
                case AddressFormat.Short:
                    return $"{_city}, {_state}";
                case AddressFormat.Long:
                    return $"{_street}, {_city}, {_state}, {_zip}, {_country}";
                default:
                    return GetFormattedAddress();
            }
        }

        [GeneratedRegex("^\\d{5}-\\d{4}|\\d{5}|[A-Z]\\d[A-Z] \\d[A-Z]\\d$")]
        private static partial Regex PostalCodeValidationRegex();
    }
}