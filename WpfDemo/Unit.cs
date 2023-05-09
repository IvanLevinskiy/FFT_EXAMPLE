using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDemo
{
    public class Unit
    {
        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public Unit(string firstname, string lastname)
        {
            (FirstName, LastName) = (firstname, lastname);
        }
    }
}
