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
            string[] files = Directory.GetFiles(App.FolderPath);
            var budgets = new List<string>();
            foreach(string file in files)
            {
                budgets.Add(Path.GetFileName(file));
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
            var filename = button.Text;
            var budgetFileName = Path.Combine(App.FolderPath, filename);
            string[] lines = File.ReadAllLines(budgetFileName);
            string budgetDetails = lines[0];
            string[] budgetParts = budgetDetails.Split(' ');

            Budget budget = new Budget();
            budget.Filename = budgetFileName;
            budget.Amount = Int32.Parse(budgetParts[0]);
            budget.Month = budgetParts[1];

            await Navigation.PushAsync(new BudgetEntryPage
            {
                BindingContext = budget
            }) ;
        }
    }
}
