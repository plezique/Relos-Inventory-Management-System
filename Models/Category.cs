using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Lab.Models
{
    public class Category
    {
        private static int _idCounter = 1;

        public int CategoryId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Category(string name, string description)
        {
            CategoryId = _idCounter++;
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"[{CategoryId}] {Name} - {Description}";
        }
    }
}