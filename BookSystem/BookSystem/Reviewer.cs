/*
 *  File Name: Reviewer.cs
 *  Description: The class file for Reviewer which contains the characteristics for a reviewer.
 *  Version: 1.0
 *  
 *  Author: Youfang Yao
 *  Date: 2024-09-17
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
     * Class Name: Reviewer
     * Description: This class contains the characteristics for a reviewer.
     *      FirstName (e.g.: "Bork") - Identifies the review's first name.
     *      Lastname (e.g: "Wormm") - Identifies the review's last name.
     *      Email (e.g. "BorkWormm@treepress.ca") - Identifies the email address of the reviewer.
     *      Organization (e.g. TreePress publications) - Organization of the reviewer (optional). Reviewer can be independent.
     * 
     **/
    public class Reviewer
    {
        #region Data Members
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty; 
        private string? _organization = null;   // optional
        #endregion //Data Members

        #region Properties
        public string Email 
        { 
            get { return _email; } 
            set
            {
                // The regex pattern for a valid email address
                const string REGEX_PATTERN_EMAIL = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                // Email address can't be empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Email address is required.");
                }

                // Email address must match the Email regex pattern
                Regex regex = new Regex(REGEX_PATTERN_EMAIL);
                if (!regex.IsMatch(value.Trim()))
                {
                    throw new ArgumentNullException("Email is not an acceptable email address pattern.");
                }

                _email = value.Trim(); 
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Reviewer first name is required.");
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
                    throw new ArgumentNullException("Reviewer last name is required.");
                }
                _lastName = value.Trim();
            }
        }
        public string Organization
        {
            get { return _organization; }
            set
            {
                _organization = value;
            }
        }
        public string ReviewerName
        {
            get { return $"{_lastName}, {_firstName}"; }
        }
        #endregion //Propertes

        #region Constructors
        public Reviewer(string firstName, string lastName, string email, string? organization = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Organization = organization; // Orgnaizaiton is null in default case
        }
        #endregion //Constructors

        #region Methods
        public override string ToString()
        {
            return $"{FirstName},{LastName},{Email},{Organization}";
        }
        #endregion //Methods
    }
}
