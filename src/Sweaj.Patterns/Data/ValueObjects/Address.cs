using System.Text.RegularExpressions;

namespace Sweaj.Patterns.Data.ValueObjects
{
    public enum AddressFormat
    {
        Short,
        Long
    }

    public sealed partial class Address
    {
        private readonly string street;
        private readonly string city;
        private readonly string state;
        private readonly string zip;
        private readonly string country;

        public string Street => this.street;
        public string City => this.city;
        public string State => this.state;
        public string Zip => this.zip;
        public string Country => this.country;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private Address(string street, string city, string state, string zip, string country)
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.country = country;
        }

        public static Address Create(string street, string city, string state, string zip, string country)
        {
            //validate the address format using regular expressions
            if (!ZipCodeRegex().IsMatch(Guard.Against.NullOrWhiteSpace(zip)))
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

            return this.street == address.street &&
                   this.city == address.city &&
                   this.state == address.state &&
                   this.zip == address.zip &&
                   this.country == address.country;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.street, this.city, this.state, this.zip, this.country);
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
            return $"{this.street}, {this.city}, {this.state}, {this.zip}, {this.country}";
        }

        private string GetFormattedAddress(AddressFormat format)
        {
            switch (format)
            {
                case AddressFormat.Short:
                    return $"{this.city}, {this.state}";
                case AddressFormat.Long:
                    return $"{this.street}, {this.city}, {this.state}, {this.zip}, {this.country}";
                default:
                    return GetFormattedAddress();
            }
        }

        [GeneratedRegex("^\\d{5}-\\d{4}|\\d{5}|[A-Z]\\d[A-Z] \\d[A-Z]\\d$")]
        private static partial Regex PostalCodeValidationRegex();
        [GeneratedRegex("^\\d{5}(?:[-\\s]\\d{4})?$")]
        private static partial Regex ZipCodeRegex();
    }
}