using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Midterm_Lab.Models;

namespace Midterm_Lab
{
    class Program
    {
        // Storage 
        static List<Category> categories = new List<Category>();
        static List<Supplier> suppliers = new List<Supplier>();
        static List<Product> products = new List<Product>();
        static List<User> users = new List<User>();
        static List<TransactionRecord> transactions = new List<TransactionRecord>();

        static User currentUser = null;

        const string DashboardRule = "-------------------------------------------------------";

        // Entry Point 
        static void Main(string[] args)
        {
            SeedData();
            ShowLoginScreen();

            if (currentUser == null)
            {
                PrintError("Login failed. Exiting.");
                return;
            }

            bool running = true;
            while (running)
            {
                ShowMainDashboard();
                string choice = ReadInput("Enter choice");

                try
                {
                    switch (choice)
                    {
                        case "1": RunCategoryManagement(); break;
                        case "2": RunSupplierManagement(); break;
                        case "3": RunProductManagement(); break;
                        case "4": RunStockOperations(); break;
                        case "5": RunReportsAndAnalytics(); break;
                        case "0":
                            PrintSuccess("Goodbye! Logged out.");
                            running = false;
                            break;
                        default:
                            PrintError("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    PrintError($"Unexpected error: {ex.Message}");
                }

                if (running)
                {
                    Console.WriteLine();
                    Console.Write("  Press Enter to continue...");
                    Console.ReadLine();
                }
            }
        }

        // Back Helper 
        public static bool IsBack(string input)
        {
            return input.Equals("B", StringComparison.OrdinalIgnoreCase);
        }

        static void PrintBackHint()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  <- [B] Back");
            Console.ResetColor();
        }

        static bool IsValidSupplierContact(string contact)
        {
            if (contact == null || contact.Length != 11 || !contact.StartsWith("09"))
                return false;
            foreach (char c in contact)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        static bool IsValidSupplierEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            int at = email.IndexOf('@');
            if (at <= 0)
                return false;

            int dotAfter = email.IndexOf('.', at + 1);
            if (dotAfter <= at + 1 || dotAfter >= email.Length - 1)
                return false;

            return true;
        }

        // Seed Data 
        static void SeedData()
        {
            users.Add(new User("admin", "admin123", UserRole.Admin));
        }

        // Login 
        static void ShowLoginScreen()
        {
            Console.Clear();
            PrintHeader("╔══════════════════════════════════════════╗");
            PrintHeader("║       INVENTORY MANAGEMENT SYSTEM        ║");
            PrintHeader("╚══════════════════════════════════════════╝");
            Console.WriteLine("      User: admin | Password: admin123");
            Console.WriteLine();

            for (int attempt = 1; attempt <= 3; attempt++)
            {
                try
                {
                    string username = ReadInput("Username");
                    Console.Write("  Password: ");
                    string password = ReadMaskedInput();

                    User found = users.Find(u => u.Username == username);
                    if (found != null && found.ValidatePassword(password))
                    {
                        currentUser = found;
                        PrintSuccess($"\n  Welcome, {currentUser.Username}! Role: {currentUser.Role}");
                        System.Threading.Thread.Sleep(1000);
                        return;
                    }
                    else
                    {
                        PrintError($"  Invalid credentials. Attempt {attempt}/3.");
                    }
                }
                catch (Exception ex)
                {
                    PrintError($"  Login error: {ex.Message}");
                }
            }
        }

        static string ReadMaskedInput()
        {
            string pass = "";
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Substring(0, pass.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return pass;
        }

        // Main dashboard & sub-menus 
        static void ShowMainDashboard()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(DashboardRule);
            Console.WriteLine($"  INVENTORY MANAGEMENT SYSTEM  |  User: {currentUser.Username}");
            Console.WriteLine(DashboardRule);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("  [1] CATEGORY MANAGEMENT");
            Console.WriteLine("  [2] SUPPLIER MANAGEMENT");
            Console.WriteLine("  [3] PRODUCT MANAGEMENT");
            Console.WriteLine("  [4] STOCKS MANAGAMENT");
            Console.WriteLine("  [5] REPORTS");
            Console.WriteLine("  [0] Logout");
            Console.WriteLine();
        }

        static void PrintSubMenuHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(DashboardRule);
            Console.WriteLine($"  {title}");
            Console.WriteLine(DashboardRule);
            Console.ResetColor();
            Console.WriteLine();
        }

        static void RunCategoryManagement()
        {
            while (true)
            {
                PrintSubMenuHeader("CATEGORY MANAGEMENT");
                PrintBackHint();
                Console.WriteLine("  [1] Add Category");
                Console.WriteLine("  [2] View Categories");
                Console.WriteLine();
                string sub = ReadInput("Enter choice");
                if (IsBack(sub)) return;

                try
                {
                    switch (sub)
                    {
                        case "1": AddCategory(); break;
                        case "2": ViewCategories(); break;
                        default: PrintError("Invalid option. Please try again."); break;
                    }
                }
                catch (Exception ex)
                {
                    PrintError($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine();
                Console.Write("  Press Enter to continue...");
                Console.ReadLine();
            }
        }

        static void RunSupplierManagement()
        {
            while (true)
            {
                PrintSubMenuHeader("SUPPLIER MANAGEMENT");
                PrintBackHint();
                Console.WriteLine("  [1] Add Supplier");
                Console.WriteLine("  [2] View Suppliers");
                Console.WriteLine();
                string sub = ReadInput("Enter choice");
                if (IsBack(sub)) return;

                try
                {
                    switch (sub)
                    {
                        case "1": AddSupplier(); break;
                        case "2": ViewSuppliers(); break;
                        default: PrintError("Invalid option. Please try again."); break;
                    }
                }
                catch (Exception ex)
                {
                    PrintError($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine();
                Console.Write("  Press Enter to continue...");
                Console.ReadLine();
            }
        }

        static void RunProductManagement()
        {
            while (true)
            {
                PrintSubMenuHeader("PRODUCT MANAGEMENT");
                PrintBackHint();
                Console.WriteLine("  [1] Add Product");
                Console.WriteLine("  [2] View All Products");
                Console.WriteLine("  [3] Search Product");
                Console.WriteLine("  [4] Update Product");
                Console.WriteLine("  [5] Delete Product");
                Console.WriteLine();
                string sub = ReadInput("Enter choice");
                if (IsBack(sub)) return;

                try
                {
                    switch (sub)
                    {
                        case "1": AddProduct(); break;
                        case "2": ViewAllProducts(); break;
                        case "3": SearchProduct(); break;
                        case "4": UpdateProduct(); break;
                        case "5": DeleteProduct(); break;
                        default: PrintError("Invalid option. Please try again."); break;
                    }
                }
                catch (Exception ex)
                {
                    PrintError($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine();
                Console.Write("  Press Enter to continue...");
                Console.ReadLine();
            }
        }

        static void RunStockOperations()
        {
            while (true)
            {
                PrintSubMenuHeader("STOCK OPERATIONS");
                PrintBackHint();
                Console.WriteLine("  [1] Restock Product");
                Console.WriteLine("  [2] Deduct Stock");
                Console.WriteLine();
                string sub = ReadInput("Enter choice");
                if (IsBack(sub)) return;

                try
                {
                    switch (sub)
                    {
                        case "1": RestockProduct(); break;
                        case "2": DeductStock(); break;
                        default: PrintError("Invalid option. Please try again."); break;
                    }
                }
                catch (Exception ex)
                {
                    PrintError($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine();
                Console.Write("  Press Enter to continue...");
                Console.ReadLine();
            }
        }

        static void RunReportsAndAnalytics()
        {
            while (true)
            {
                PrintSubMenuHeader("REPORTS");
                PrintBackHint();
                Console.WriteLine("  [1] View Transaction History");
                Console.WriteLine("  [2] Show Low-Stock Items");
                Console.WriteLine("  [3] Compute Total Inventory Value");
                Console.WriteLine();
                string sub = ReadInput("Enter choice");
                if (IsBack(sub)) return;

                try
                {
                    switch (sub)
                    {
                        case "1": ViewTransactions(); break;
                        case "2": ShowLowStockItems(); break;
                        case "3": ComputeInventoryValue(); break;
                        default: PrintError("Invalid option. Please try again."); break;
                    }
                }
                catch (Exception ex)
                {
                    PrintError($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine();
                Console.Write("  Press Enter to continue...");
                Console.ReadLine();
            }
        }

        //  1. Add Category 
        static void AddCategory()
        {
            PrintSectionHeader("ADD CATEGORY");
            PrintBackHint();

            string name = ReadRequiredInputOrBack("Category Name");
            if (name == null) { PrintWarning("Returning to main menu."); return; }

            string desc = ReadRequiredInputOrBack("Description");
            if (desc == null) { PrintWarning("Returning to main menu."); return; }

            if (categories.Exists(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                PrintError("A category with that name already exists.");
                return;
            }

            categories.Add(new Category(name, desc));
            PrintSuccess($"Category '{name}' added successfully!");
        }

        // 2. Add Supplier
        static void AddSupplier()
        {
            PrintSectionHeader("ADD SUPPLIER");
            PrintBackHint();

            string name = ReadRequiredInputOrBack("Supplier Name");
            if (name == null) { PrintWarning("Returning to main menu."); return; }

            string contact = ReadRequiredInputOrBack("Contact Number");
            if (contact == null) { PrintWarning("Returning to main menu."); return; }
            while (!IsValidSupplierContact(contact))
            {
                PrintError("Invalid contact. Enter exactly 11 digits starting with 09 (e.g. 09171234567).");
                contact = ReadRequiredInputOrBack("Contact Number");
                if (contact == null) { PrintWarning("Returning to main menu."); return; }
            }

            string email = ReadRequiredInputOrBack("Email");
            if (email == null) { PrintWarning("Returning to main menu."); return; }
            while (!IsValidSupplierEmail(email))
            {
                PrintError("Invalid email. Use a format like name@example.com (needs @ and a dot after it).");
                email = ReadRequiredInputOrBack("Email");
                if (email == null) { PrintWarning("Returning to main menu."); return; }
            }

            string address = ReadRequiredInputOrBack("Address");
            if (address == null) { PrintWarning("Returning to main menu."); return; }

            if (suppliers.Exists(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                PrintError("A supplier with that name already exists.");
                return;
            }

            suppliers.Add(new Supplier(name, contact, email, address));
            PrintSuccess($"Supplier '{name}' added successfully!");
        }

        // 3. Add Product
        static void AddProduct()
        {
            PrintSectionHeader("ADD PRODUCT");

            if (categories.Count == 0 || suppliers.Count == 0)
            {
                PrintError("Please add at least one Category and one Supplier first.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  -- CATEGORIES --");
            Console.ResetColor();
            foreach (var c in categories) Console.WriteLine($"  {c}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  -- SUPPLIERS --");
            Console.ResetColor();
            foreach (var s in suppliers) Console.WriteLine($"  {s}");

            Console.WriteLine();
            PrintBackHint();

            string name = ReadRequiredInputOrBack("Product Name");
            if (name == null) { PrintWarning("Returning to main menu."); return; }

            string desc = ReadRequiredInputOrBack("Description");
            if (desc == null) { PrintWarning("Returning to main menu."); return; }

            string priceRaw = ReadRequiredInputOrBack("Price (PHP)");
            if (priceRaw == null) { PrintWarning("Returning to main menu."); return; }
            decimal price;
            while (!decimal.TryParse(priceRaw, out price) || price < 0)
            {
                PrintError("Invalid price. Enter a valid non-negative number.");
                priceRaw = ReadRequiredInputOrBack("Price (PHP)");
                if (priceRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            string stockRaw = ReadRequiredInputOrBack("Initial Stock");
            if (stockRaw == null) { PrintWarning("Returning to main menu."); return; }
            int stock;
            while (!int.TryParse(stockRaw, out stock) || stock < 0)
            {
                PrintError("Invalid stock. Enter a valid non-negative integer.");
                stockRaw = ReadRequiredInputOrBack("Initial Stock");
                if (stockRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            string thresholdRaw = ReadRequiredInputOrBack("Low Stock Threshold");
            if (thresholdRaw == null) { PrintWarning("Returning to main menu."); return; }
            int threshold;
            while (!int.TryParse(thresholdRaw, out threshold) || threshold < 0)
            {
                PrintError("Invalid threshold. Enter a valid non-negative integer.");
                thresholdRaw = ReadRequiredInputOrBack("Low Stock Threshold");
                if (thresholdRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            string catRaw = ReadRequiredInputOrBack("Category ID");
            if (catRaw == null) { PrintWarning("Returning to main menu."); return; }
            int catId;
            while (!int.TryParse(catRaw, out catId) || !categories.Exists(c => c.CategoryId == catId))
            {
                PrintError("Invalid Category ID.");
                catRaw = ReadRequiredInputOrBack("Category ID");
                if (catRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            string supRaw = ReadRequiredInputOrBack("Supplier ID");
            if (supRaw == null) { PrintWarning("Returning to main menu."); return; }
            int supId;
            while (!int.TryParse(supRaw, out supId) || !suppliers.Exists(s => s.SupplierId == supId))
            {
                PrintError("Invalid Supplier ID.");
                supRaw = ReadRequiredInputOrBack("Supplier ID");
                if (supRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            var product = new Product(name, desc, price, stock, threshold, catId, supId);
            products.Add(product);

            LogTransaction(product.ProductId, product.Name, TransactionType.AddProduct, stock, "Initial stock on creation.");
            PrintSuccess($"Product '{name}' added with ID {product.ProductId}.");
        }

        //  4. View All Products
        static void ViewAllProducts()
        {
            PrintSectionHeader("ALL PRODUCTS");

            if (products.Count == 0)
            {
                PrintWarning("No products found.");
                return;
            }

            PrintTableHeader();
            foreach (var p in products)
                PrintProductRow(p);

            Console.WriteLine($"\n  Total products: {products.Count}");
        }

        // 5. Search Product 
        static void SearchProduct()
        {
            PrintSectionHeader("SEARCH PRODUCT");
            PrintBackHint();

            string keyword = ReadRequiredInputOrBack("Search keyword (name or ID)");
            if (keyword == null) { PrintWarning("Returning to main menu."); return; }

            bool isId = int.TryParse(keyword, out int id);

            var results = products.FindAll(p =>
                p.Name.ToLower().Contains(keyword.ToLower()) ||
                (isId && p.ProductId == id));

            if (results.Count == 0)
            {
                PrintWarning("No products matched your search.");
                return;
            }

            PrintTableHeader();
            foreach (var p in results)
                PrintProductRow(p);

            Console.WriteLine($"\n  Found {results.Count} product(s).");
        }

        // 6. Update Product 
        static void UpdateProduct()
        {
            PrintSectionHeader("UPDATE PRODUCT");

            if (products.Count == 0) { PrintWarning("No products found."); return; }
            PrintTableHeader();
            foreach (var p in products) PrintProductRow(p);

            PrintBackHint();

            string idRaw = ReadRequiredInputOrBack("Enter Product ID to update");
            if (idRaw == null) { PrintWarning("Returning to main menu."); return; }

            int id;
            while (!int.TryParse(idRaw, out id))
            {
                PrintError("Please enter a valid integer.");
                idRaw = ReadRequiredInputOrBack("Enter Product ID to update");
                if (idRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            var product = products.Find(p => p.ProductId == id);
            if (product == null)
            {
                PrintError("Product not found.");
                return;
            }

            Console.WriteLine($"\n  Updating: {product.Name}");
            Console.WriteLine("  (Press Enter to keep current value, B to go back)\n");

            string newName = ReadOptionalInput($"Name [{product.Name}]");
            if (IsBack(newName)) { PrintWarning("Returning to main menu."); return; }

            string newDesc = ReadOptionalInput($"Description [{product.Description}]");
            if (IsBack(newDesc)) { PrintWarning("Returning to main menu."); return; }

            string newPrice = ReadOptionalInput($"Price [{product.Price:F2}]");
            if (IsBack(newPrice)) { PrintWarning("Returning to main menu."); return; }

            string newThreshold = ReadOptionalInput($"Low Stock Threshold [{product.LowStockThreshold}]");
            if (IsBack(newThreshold)) { PrintWarning("Returning to main menu."); return; }

            if (!string.IsNullOrWhiteSpace(newName)) product.Name = newName;
            if (!string.IsNullOrWhiteSpace(newDesc)) product.Description = newDesc;
            if (!string.IsNullOrWhiteSpace(newPrice))
            {
                if (decimal.TryParse(newPrice, out decimal parsedPrice) && parsedPrice >= 0)
                    product.Price = parsedPrice;
                else
                    PrintWarning("Invalid price - kept original.");
            }
            if (!string.IsNullOrWhiteSpace(newThreshold))
            {
                if (int.TryParse(newThreshold, out int parsedThreshold) && parsedThreshold >= 0)
                    product.LowStockThreshold = parsedThreshold;
                else
                    PrintWarning("Invalid threshold - kept original.");
            }

            LogTransaction(product.ProductId, product.Name, TransactionType.UpdateProduct, 0, "Product info updated.");
            PrintSuccess("Product updated successfully!");
        }

        // 7. Delete Product 
        static void DeleteProduct()
        {
            PrintSectionHeader("DELETE PRODUCT");

            if (products.Count == 0) { PrintWarning("No products found."); return; }
            PrintTableHeader();
            foreach (var p in products) PrintProductRow(p);

            PrintBackHint();

            string idRaw = ReadRequiredInputOrBack("Enter Product ID to delete");
            if (idRaw == null) { PrintWarning("Returning to main menu."); return; }

            int id;
            while (!int.TryParse(idRaw, out id))
            {
                PrintError("Please enter a valid integer.");
                idRaw = ReadRequiredInputOrBack("Enter Product ID to delete");
                if (idRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            var product = products.Find(p => p.ProductId == id);
            if (product == null)
            {
                PrintError("Product not found.");
                return;
            }

            Console.Write($"\n  Are you sure you want to delete '{product.Name}'? (yes/no/B): ");
            string confirmRaw = Console.ReadLine();
            string confirm = confirmRaw != null ? confirmRaw.Trim().ToLower() : "";

            if (IsBack(confirm)) { PrintWarning("Returning to main menu."); return; }

            if (confirm != "yes")
            {
                PrintWarning("Deletion cancelled.");
                return;
            }

            LogTransaction(product.ProductId, product.Name, TransactionType.DeleteProduct, product.Stock, "Product removed from inventory.");
            products.Remove(product);
            PrintSuccess($"Product '{product.Name}' deleted.");
        }

        //  8. Restock Product 
        static void RestockProduct()
        {
            PrintSectionHeader("RESTOCK PRODUCT");

            if (products.Count == 0) { PrintWarning("No products found."); return; }
            PrintTableHeader();
            foreach (var p in products) PrintProductRow(p);

            PrintBackHint();

            string idRaw = ReadRequiredInputOrBack("Enter Product ID to restock");
            if (idRaw == null) { PrintWarning("Returning to main menu."); return; }

            int id;
            while (!int.TryParse(idRaw, out id))
            {
                PrintError("Please enter a valid integer.");
                idRaw = ReadRequiredInputOrBack("Enter Product ID to restock");
                if (idRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            var product = products.Find(p => p.ProductId == id);
            if (product == null)
            {
                PrintError("Product not found.");
                return;
            }

            string qtyRaw = ReadRequiredInputOrBack("Quantity to add");
            if (qtyRaw == null) { PrintWarning("Returning to main menu."); return; }

            int qty;
            while (!int.TryParse(qtyRaw, out qty) || qty <= 0)
            {
                PrintError("Quantity must be a positive integer.");
                qtyRaw = ReadRequiredInputOrBack("Quantity to add");
                if (qtyRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            product.Stock += qty;
            LogTransaction(product.ProductId, product.Name, TransactionType.Restock, qty, $"Restocked. New stock: {product.Stock}");
            PrintSuccess($"Restocked '{product.Name}'. New stock: {product.Stock}");
        }

        // 9. Deduct Stock 
        static void DeductStock()
        {
            PrintSectionHeader("DEDUCT STOCK");

            if (products.Count == 0) { PrintWarning("No products found."); return; }
            PrintTableHeader();
            foreach (var p in products) PrintProductRow(p);

            PrintBackHint();

            string idRaw = ReadRequiredInputOrBack("Enter Product ID to deduct from");
            if (idRaw == null) { PrintWarning("Returning to main menu."); return; }

            int id;
            while (!int.TryParse(idRaw, out id))
            {
                PrintError("Please enter a valid integer.");
                idRaw = ReadRequiredInputOrBack("Enter Product ID to deduct from");
                if (idRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            var product = products.Find(p => p.ProductId == id);
            if (product == null)
            {
                PrintError("Product not found.");
                return;
            }

            string qtyRaw = ReadRequiredInputOrBack("Quantity to deduct");
            if (qtyRaw == null) { PrintWarning("Returning to main menu."); return; }

            int qty;
            while (!int.TryParse(qtyRaw, out qty) || qty <= 0)
            {
                PrintError("Quantity must be a positive integer.");
                qtyRaw = ReadRequiredInputOrBack("Quantity to deduct");
                if (qtyRaw == null) { PrintWarning("Returning to main menu."); return; }
            }

            if (qty > product.Stock)
            {
                PrintError($"Cannot deduct {qty}. Only {product.Stock} in stock.");
                return;
            }

            product.Stock -= qty;
            LogTransaction(product.ProductId, product.Name, TransactionType.Deduct, qty, $"Stock deducted. Remaining: {product.Stock}");
            PrintSuccess($"Deducted {qty} from '{product.Name}'. Remaining stock: {product.Stock}");

            if (product.IsLowStock())
                PrintWarning($"WARNING: '{product.Name}' is now low on stock! ({product.Stock} remaining)");
        }

        //  10. View Transactions 
        static void ViewTransactions()
        {
            PrintSectionHeader("TRANSACTION HISTORY");

            if (transactions.Count == 0)
            {
                PrintWarning("No transactions recorded yet.");
                return;
            }

            Console.WriteLine($"  {"ID",-5} {"Timestamp",-22} {"Type",-15} {"Product",-20} {"Qty",-6} {"By",-10} Notes");
            Console.WriteLine(new string('-', 100));

            foreach (var t in transactions)
            {
                Console.WriteLine($"  {t.TransactionId,-5} {t.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),-22} {t.Type,-15} {t.ProductName,-20} {t.Quantity,-6} {t.PerformedBy,-10} {t.Notes}");
            }

            Console.WriteLine($"\n  Total transactions: {transactions.Count}");
        }

        // 11. Low Stock Items 
        static void ShowLowStockItems()
        {
            PrintSectionHeader("LOW STOCK ITEMS");

            var lowStock = products.FindAll(p => p.IsLowStock());

            if (lowStock.Count == 0)
            {
                PrintSuccess("All products have sufficient stock.");
                return;
            }

            PrintWarning($"  {lowStock.Count} product(s) are low on stock:\n");
            PrintTableHeader();
            foreach (var p in lowStock)
                PrintProductRow(p);
        }

        // 12. Total Inventory Value 
        static void ComputeInventoryValue()
        {
            PrintSectionHeader("TOTAL INVENTORY VALUE");

            if (products.Count == 0)
            {
                PrintWarning("No products in inventory.");
                return;
            }

            decimal total = 0;
            Console.WriteLine($"\n  {"Product",-25} {"Price",14}  {"Stock",8}  {"Value",14}");
            Console.WriteLine(new string('-', 71));

            foreach (var p in products)
            {
                decimal val = p.GetTotalValue();
                total += val;
                Console.WriteLine($"  {p.Name,-25} {$"PHP {p.Price:F2}",14}  {p.Stock,8}  {$"PHP {val:F2}",14}");
            }

            Console.WriteLine(new string('-', 71));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  {"TOTAL INVENTORY VALUE",-45} {$"PHP {total:F2}",14}");
            Console.ResetColor();
        }

        //  13. View Categories 
        static void ViewCategories()
        {
            PrintSectionHeader("CATEGORIES");
            if (categories.Count == 0) { PrintWarning("No categories added yet."); return; }
            foreach (var c in categories) Console.WriteLine($"  {c}");
        }

        // 14. View Suppliers 
        static void ViewSuppliers()
        {
            PrintSectionHeader("SUPPLIERS");
            if (suppliers.Count == 0) { PrintWarning("No suppliers added yet."); return; }
            foreach (var s in suppliers) Console.WriteLine($"  {s}");
        }

        // Helpers
        static void LogTransaction(int productId, string productName, TransactionType type, int qty, string notes)
        {
            transactions.Add(new TransactionRecord(productId, productName, type, qty, currentUser.Username, notes));
        }

        static void PrintTableHeader()
        {
            Console.WriteLine($"\n  {"ID",-5} {"Name",-25} {"Price",14}  {"Stock",6}  {"Threshold",10}  {"CatID",6}  {"SupID",6}  {"Status",-10}");
            Console.WriteLine(new string('-', 99));
        }

        static void PrintProductRow(Product p)
        {
            string status = p.IsLowStock() ? "LOW" : "OK";
            ConsoleColor color = p.IsLowStock() ? ConsoleColor.Yellow : ConsoleColor.Green;
            Console.Write($"  {p.ProductId,-5} {p.Name,-25} {$"PHP {p.Price:F2}",14}  {p.Stock,6}  {p.LowStockThreshold,10}  {p.CategoryId,6}  {p.SupplierId,6}  ");
            Console.ForegroundColor = color;
            Console.WriteLine(status);
            Console.ResetColor();
        }

        static string ReadRequiredInputOrBack(string prompt)
        {
            while (true)
            {
                Console.Write($"  {prompt}: ");
                string result = Console.ReadLine();
                string value = result != null ? result.Trim() : "";

                if (IsBack(value)) return null;
                if (!string.IsNullOrWhiteSpace(value)) return value;

                PrintError("This field cannot be empty.");
            }
        }

        static string ReadInput(string prompt)
        {
            Console.Write($"  {prompt}: ");
            string result = Console.ReadLine();
            return result != null ? result.Trim() : "";
        }

        static string ReadRequiredInput(string prompt)
        {
            string value;
            do
            {
                value = ReadInput(prompt);
                if (string.IsNullOrWhiteSpace(value))
                    PrintError("  This field cannot be empty.");
            } while (string.IsNullOrWhiteSpace(value));
            return value;
        }

        static string ReadOptionalInput(string prompt)
        {
            Console.Write($"  {prompt}: ");
            string result = Console.ReadLine();
            return result != null ? result.Trim() : "";
        }

        static void PrintHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void PrintSectionHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  == {title} ==");
            Console.ResetColor();
        }

        static void PrintSuccess(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  [OK] {msg}");
            Console.ResetColor();
        }

        static void PrintError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [ERROR] {msg}");
            Console.ResetColor();
        }

        static void PrintWarning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  [!] {msg}");
            Console.ResetColor();
        }
    }
}