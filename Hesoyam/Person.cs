using System;

namespace Hesoyam
{
    public abstract class Person : UniqueID
    {
        private string _name;
        private string _surname;

        public Person()
        {

        }

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = Verify.IsDataCorrectFormat(value) ? value : throw new InvalidOperationException("Name is null or white space or contains integer");
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = Verify.IsDataCorrectFormat(value) ? value : throw new InvalidOperationException("Surname is null or white space or contains integer.");
            }
        }

    }
}
