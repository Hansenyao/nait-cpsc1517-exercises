using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem
{
    public class Reviewer
    {
        #region Data Members
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty; 
        private string _organization = string.Empty;
        #endregion //Data Members

        #region Properties
        public string Email 
        { 
            get { return _email; } 
            set 
            {
                _email = value; 
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
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
            get { return _firstName + " " + _lastName; }
        }
        #endregion //Propertes

        #region Constructors
        public Reviewer(string firstName, string lastName, string email, string organization)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Organization = organization;
        }
        #endregion //Constructors

        #region Methods
        public override string ToString()
        {
            return "";
        }
        #endregion //Methods
    }
}
