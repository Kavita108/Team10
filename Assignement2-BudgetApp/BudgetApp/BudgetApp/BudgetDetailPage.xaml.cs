using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetDetailPage : ContentPage
    {
        public BudgetDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var budget = (Budget)this.BindingContext;
            var filename = budget.Filename;
            string[] lines = File.ReadAllLines(filename);
            string budgetDetails = lines[0];
            string[] budgetParts = budgetDetails.Split(' ');

            budget.Amount = Decimal.Parse(budgetParts[0]);
            budget.Month = budgetParts[1];
            budget.Expenses = new List<Expense>();
            decimal totalExpenses = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] expenseParts = lines[i].Split(' ');
                Expense expense = new Expense();
                expense.Name = expenseParts[0];
                expense.Amount = Decimal.Parse(expenseParts[1]);
                totalExpenses = totalExpenses + expense.Amount;
                expense.PurchasedDate = expenseParts[2];
                expense.Category = ImageSource.FromResource("BudgetApp.Assets." + expenseParts[3] + ".png",
                    typeof(Expense).GetTypeInfo().Assembly);
                expense.Filename = budget.Filename;
                budget.Expenses.Add(expense);
            }

            ErrorMessage.IsVisible = false;
            if (budget.Amount <= totalExpenses)
            {
                ErrorMessage.Text = "You are out of budget";
                ErrorMessage.IsVisible = true;
            }
            else if ((budget.Amount - totalExpenses) < 500)
            {
                ErrorMessage.Text = "You have less than $500 left";
                ErrorMessage.IsVisible = true;
            }

            BudgetDetailView.ItemsSource = budget.Expenses;
        }

        async void OnAddExpensetButtonClicked(object sender, EventArgs e)
        {
            var expense = new Expense();
            expense.Filename = fileNameLabel.Text;

            await Navigation.PushAsync(new ExpenseEntryPage
            {
                BindingContext = expense
            });
        }
    }
}