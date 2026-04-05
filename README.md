# Midterm Laboratory - Inventory Management System

---

## Student info

- **Name:** Enrick Guiller O. Relos  
- **Course:** BSIT-3B  
- **Subject:** IT Elective 2 - Internet Technologies  
- **Instructor:** Jay Carlo C. Ribaya  

---

## How to open and run

1. Open **`Midterm_Lab.sln`** in Visual Studio.  
2. Press **F5** or click the green **Start** button.

**Default login**
- Username: admin
- Password: admin123

---

## Features

- **Categories** - add and view  
- **Suppliers** - add and view (with phone/email checks where applicable)  
- **Products** - add, view, search, update, delete  
- **Stock** - restock and deduct; warns when stock is low vs threshold  
- **History** - keeps a log of actions with time and user  
- **Reports** - e.g. low stock, total inventory value  
- **Navigation** - type **`B`** at prompts to go back  


---

## Structure

```
Midterm_Lab/
  Models/           (Category, Product, Supplier, User, TransactionRecord)
  Properties/
  App.config
  Midterm_Lab.csproj
  Midterm_Lab.sln
  Program.cs        (menus and most logic)
```

---

## Object Oriented Programming
We had to apply basic OOP ideas:

- Separate **classes** for each "thing" (product, category, supplier, user, transaction)  
- **Constructors** to set up objects  
- **Properties** instead of dumping everything in public fields  
- **Encapsulation** - private fields where it makes sense, IDs not editable from outside  
- **Methods** for behavior (e.g. low stock checks, totals)  
- **try / catch** so random errors don't instantly crash the whole program  

---

