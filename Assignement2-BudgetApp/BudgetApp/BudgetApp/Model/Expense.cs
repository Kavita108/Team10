using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
    public enum Category
    {
        Grocery,
        Restaurants,
        Medical,
        Travel,
        Apparels
    }

    public class Expense
    {
        public string Filename { get; set; }
        public int Amount { get; set; }
        public Category category { get; set; }
    }
}
