using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Lab.Models
{
    public enum TransactionType
    {
        Restock,
        Deduct,
        AddProduct,
        DeleteProduct,
        UpdateProduct
    }

    public class TransactionRecord
    {
        private static int _idCounter = 1;

        public int TransactionId { get; private set; }
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public TransactionType Type { get; private set; }
        public int Quantity { get; private set; }
        public string PerformedBy { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Notes { get; private set; }

        public TransactionRecord(int productId, string productName, TransactionType type, int quantity, string performedBy, string notes = "")
        {
            TransactionId = _idCounter++;
            ProductId = productId;
            ProductName = productName;
            Type = type;
            Quantity = quantity;
            PerformedBy = performedBy;
            Timestamp = DateTime.Now;
            Notes = notes;
        }

        public override string ToString()
        {
            return $"[{TransactionId}] {Timestamp:yyyy-MM-dd HH:mm:ss} | {Type} | Product: {ProductName} (ID:{ProductId}) | Qty: {Quantity} | By: {PerformedBy} | Notes: {Notes}";
        }
    }
}
