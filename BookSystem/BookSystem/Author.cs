using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookSystem
{
    public class Author
    {
        #region Data Members
        private string _contactUrl = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _residentCity = string.Empty;
        private string _residentCountry = string.Empty;
        #endregion  //Data Members

        #region Properties
        public string ContactUrl
        {
            get { return _contactUrl; }
            set
            {
                const string REGEX_PATTERN = @"(https?://www)?[a-zA-Z0-9]+\.\w{2,}(?!\.)";

                // A contact url can't be empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Contact URL is required.");
                }

                // A contact url must match the URL pattern
                Regex regex = new Regex(REGEX_PATTERN);
                if (!regex.IsMatch(value.Trim()))
                {
                    throw new ArgumentNullException("Contact URL is not an acceptable url pattern.");
                }
                
                _contactUrl = value.Trim();
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Author first name is required.");
                }
                _firstName = value.Trim();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Author last name is required.");
                }
                _lastName = value.Trim();
            }
        }
        public string AuthorName
        {
            get { return $"{LastName}, {FirstName}"; }
        }
        public string ResidentCity
        {
            get { return _residentCity; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Resident city is required.");
                }
                _residentCity = value.Trim();
            }
        }
        public string ResidentCountry
        {
            get { return _residentCountry; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Resident country is required.");
                }
                _residentCountry = value.Trim();
            }
        }
        #endregion  //Properties

        #region Constructors
        public Author(string firstName, string lastName, string contactUrl, string city, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactUrl = contactUrl;
            ResidentCity = city;
            ResidentCountry = country;
        }
        #endregion  //Constructors

        #region Methods
        public override string ToString()
        {
            return $"{FirstName},{LastName},{ContactUrl},{ResidentCity},{ResidentCountry}";
        }
        #endregion  //Methods
    }
}
