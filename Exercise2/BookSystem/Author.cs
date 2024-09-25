/*
 *  File Name: Author.cs
 *  Description: The class file for Author which contains the characteristics for an author.
 *  Version: 1.0
 *  
 *  Author: Youfang Yao
 *  Date: 2024-09-16
 * 
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookSystem
{
    /*
     * Class Name: Author
     * Description: This class contains the characteristics for an author.
     *      FirstName (e.g.: "Lowand") - Identifies the author's first name.
     *      Lastname (e.g: "Behold") - Identifies the author's last name.
     *      ContactUrl (e.g. "LowandBehold.fantasy.ca") - Identifies the web site of the author.
     *      ResidentCity - Current city author is residing.
     *      ResidentCountry - Current country author is residing
     * 
     **/
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
                // The regex pattern for a valid url string
                const string REGEX_PATTERN_URL = @"(https?://www)?[a-zA-Z0-9]+\.\w{2,}(?!\.)";

                // A contact url can't be empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Contact URL is required.");
                }

                // A contact url must match the URL regex pattern
                Regex regex = new Regex(REGEX_PATTERN_URL);
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
        // Read-only properties
        public string AuthorName => $"{LastName}, {FirstName}";
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
