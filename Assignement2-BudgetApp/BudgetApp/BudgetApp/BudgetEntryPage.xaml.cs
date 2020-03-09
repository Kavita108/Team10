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
    public partial class BudgetEntryPage : ContentPage
    {
        public BudgetEntryPage()
        {
            InitializeComponent();
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var budget = (Budget)BindingContext;
            if (string.IsNullOrWhiteSpace(budget.Filename))
            {
                // Adding a new monthly budget.
                var filename = Path.Combine(App.FolderPath, $"{budget.Month}.txt");
                budget.Filename = filename;
                File.WriteAllText(filename, budget.Amount + " " + budget.Month);
            }
            else
            {
                // Editing an existing budget. Existing file will be deleted and re-created with changed amount/month.
                File.Delete(budget.Filename);
                File.WriteAllText(budget.Filename, budget.Amount + " " + budget.Month);
            }
            await Navigation.PopAsync();
        }
    }
}