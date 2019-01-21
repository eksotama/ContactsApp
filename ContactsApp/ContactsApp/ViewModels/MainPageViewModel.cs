using ContactsApp.Models;
using ContactsApp.Services;
using ContactsApp.ViewModels.Base;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;

        private IList<Contact> contacts;

        public ICommand AddContact { get; private set; }
        public ICommand EditContact { get; private set; }

        ContactViewModel selectedContact;

        public ContactViewModel SelectedContact
        {
            get { return selectedContact; }
            set { SetPropertyValue(ref selectedContact, value); }
        }


        public MainPageViewModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        //public MainPageViewModel(IContactsRepository contactsRepository)
        //{
        //    _contactsRepository = contactsRepository;
        //    // Contacts = (async () => await _contactsRepository.GetContactsAsync());

        //    Task.Run(async () =>
        //    {
        //        var contacts = await _contactsRepository.GetContactsAsync();

        //        Contacts = new ObservableCollection<ContactViewModel>(contacts.Select(c => new ContactViewModel(c)));
        //    });

        //    AddContact = new Command(async () => await OnAddContact());
        //    EditContact = new Command<ContactViewModel>(async (cvm) => await OnEditContact(cvm));
        //}

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            Contacts = await _contactService.GetContactsAsync();

            IsBusy = false;
        }

        private async Task OnAddContact()
        {
            Contacts.Add(new Contact());

            //await App.Navigation.
        }

        public IList<Contact> Contacts
        {
            get { return contacts; }
            set
            {
                SetPropertyValue(ref contacts, value);
            }
        }

        async Task SaveContact()
        {
            IsBusy = true;
            await Task.Delay(2000); // db call

            IsBusy = false;

            await Application.Current.MainPage.DisplayAlert("Save", "Contact saved", "OK");
        }

        private async Task OnEditContact(ContactViewModel cvm)
        {
            SelectedContact = cvm;
            await DependencyService.Get<INavigationService>()
                .NavigateToAsync<ContactViewModel>();
        }
    }
}
