using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
    public class Expense
    {
        public string Filename { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string PurchasedDate { get; set; }
        public string Category { get; set; }
    }
}
