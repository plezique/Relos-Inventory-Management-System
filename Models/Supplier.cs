using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Lab.Models
{
    public class Supplier
    {
        private static int _idCounter = 1;

        public int SupplierId { get; private set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Supplier(string name, string contactNumber, string email, string address)
        {
            SupplierId = _idCounter++;
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
            Address = address;
        }

        public override string ToString()
        {
            return $"[{SupplierId}] {Name} | Contact: {ContactNumber} | Email: {Email} | Address: {Address}";
        }
    }
}
