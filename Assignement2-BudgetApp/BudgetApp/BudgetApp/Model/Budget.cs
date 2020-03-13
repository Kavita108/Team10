using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
    public class Budget
    {
        public string Filename { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }

        public IList<Expense> Expenses { get; set; }
    }
}
