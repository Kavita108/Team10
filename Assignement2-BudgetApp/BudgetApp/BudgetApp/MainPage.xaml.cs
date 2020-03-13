using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            string[] files = Directory.GetFiles(App.FolderPath);
            var budgets = new List<string>();
            foreach (string file in files)
            {
                var filename = Path.GetFileName(file);
                budgets.Add(filename.Split('.')[0]);
            }

            BudgetsListView.ItemsSource = budgets;
        }
        async void OnAddBudgetButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BudgetEntryPage
            {
                BindingContext = new Budget()
            });
        }

        async void OnBudgetClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var budget = new Budget();
            var budgetFileName = Path.Combine(App.FolderPath, button.Text + ".txt");
            budget.Filename = budgetFileName;
            string[] lines = File.ReadAllLines(budgetFileName);
            string budgetDetails = lines[0];
            string[] budgetParts = budgetDetails.Split(' ');

            budget.Amount = Decimal.Parse(budgetParts[0]);
            budget.Month = budgetParts[1];
            budget.Expenses = new List<Expense>();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] expenseParts = lines[i].Split(' ');
                Expense expense = new Expense();
                expense.Name = expenseParts[0];
                expense.Amount = Decimal.Parse(expenseParts[1]);
                expense.PurchasedDate = expenseParts[2];
                expense.Category = expenseParts[3];
                expense.Filename = budget.Filename;
                budget.Expenses.Add(expense);
            }

            await Navigation.PushAsync(new BudgetDetailPage
            {
                BindingContext = budget
            });
        }

        void OnDeleteBudget(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var budgetFilename = Path.Combine(App.FolderPath, button.ClassId + ".txt");
            File.Delete(budgetFilename);
            this.OnAppearing();
        }
    }

}