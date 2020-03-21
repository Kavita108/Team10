using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BudgetApp.Model
{
    public class Expense
    {
        public string Filename { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string PurchasedDate { get; set; }
        public ImageSource Category { get; set; }
        public string CategoryLabel { get; set; }
    }
}
