using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prueba
{

    public class Customer
    {
        private string recordNumber;
        private string firstName;
        private string lastName;
        private string gender;
        private string streetName;
        private string city;
        private string state;
        public Customer(string recordNumber, string firstName, string lastName, string gender, string streetName, string city, string state)
        {
            this.recordNumber = recordNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.streetName = streetName;
            this.city = city;
            this.state = state;
        }
        public string RecordNumber
        {
            get
            {
                return recordNumber;
            }

            set
            {
                recordNumber = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public string Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
            }
        }

        public string StreetName
        {
            get
            {
                return streetName;
            }

            set
            {
                streetName = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public string State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }        
    }
}