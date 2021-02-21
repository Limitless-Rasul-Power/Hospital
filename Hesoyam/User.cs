using System;

namespace Hesoyam
{
    public class User : Person
    {

        private string _email;
        private string _phoneNumber;
        public User(in string name, in string surname, in string email, in string phoneNumber)
            : base(name, surname)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }


        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = Verify.IsValidEmail(value) ? value : throw new InvalidOperationException("Email is not correct format");
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = Verify.IsDataContainsOnlyIntegers(value) ? value : throw new InvalidOperationException("Phone number must contain only numbers");
            }
        }
    }
}
