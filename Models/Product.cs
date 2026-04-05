using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Lab.Models
{
    public class Product
    {
        private static int _idCounter = 1;

        public int ProductId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int LowStockThreshold { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        public Product(string name, string description, decimal price, int stock, int lowStockThreshold, int categoryId, int supplierId)
        {
            ProductId = _idCounter++;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            LowStockThreshold = lowStockThreshold;
            CategoryId = categoryId;
            SupplierId = supplierId;
        }

        public bool IsLowStock()
        {
            return Stock <= LowStockThreshold;
        }

        public decimal GetTotalValue()
        {
            return Price * Stock;
        }

        public override string ToString()
        {
            return $"[{ProductId}] {Name} | Price: PHP {Price:F2} | Stock: {Stock} | CategoryID: {CategoryId} | SupplierID: {SupplierId}";
        }
    }
}