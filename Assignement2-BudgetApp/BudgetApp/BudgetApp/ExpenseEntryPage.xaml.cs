using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseEntryPage : ContentPage
    {
        public ExpenseEntryPage()
        {
            InitializeComponent();
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            if (!string.IsNullOrWhiteSpace(expense.Filename))
            {
                File.AppendAllText(expense.Filename, Environment.NewLine);
                var purchasedDate = expense.PurchasedDate.Split(' ')[0];
                File.AppendAllText(expense.Filename, expense.Name + " " + expense.Amount + " " + purchasedDate + " " + expense.CategoryLabel);
            }
            await Navigation.PopAsync();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}