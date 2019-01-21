using ContactsApp.Models;
using ContactsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsApp.Views
{
    public partial class MainPageView : ContentPage
    {
        public MainPageView()
        {
            InitializeComponent();
        }

        private async void LvContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var addEditPage = new AddEditPage();
            addEditPage.BindingContext = new ContactViewModel((Contact)e.SelectedItem);
            await Navigation.PushAsync(new AddEditPage());
        }
    }
}
