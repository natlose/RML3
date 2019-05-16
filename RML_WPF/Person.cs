using System;
using System.Collections.Generic;
using System.Text;

namespace RML_WPF
{
    public class Person
    {
        public int Id { get; set; }
        public string Titles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
