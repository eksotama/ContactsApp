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

        private ObservableCollection<Contact> contacts;
        private Contact selectedContact;

        public MainPageViewModel(IContactService contactService)
        {
            _contactService = contactService;

            MessagingCenter.Subscribe<SaveContactViewModel, Contact>(this, "addContact", (sender, arg) =>
            {
                Contacts.Add(arg);
            });
        }

        public ObservableCollection<Contact> Contacts
        {
            get { return contacts; }
            set
            {
                SetPropertyValue(ref contacts, value);
            }
        }

        public Contact SelectedContact
        {
            get { return selectedContact; }
            set { SetPropertyValue(ref selectedContact, value); }
        }

        public ICommand AddContact => new Command(async () => await OnAddContact());
        public ICommand EditContact => new Command<Contact>(async (c) => await OnEditContact(c));
        public ICommand DeleteContact => new Command<Contact>(async (c) => await OnDeleteContact(c));
        public ICommand ShowCarousel => new Command(async () => await OnShowCarousel());

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            Contacts = await _contactService.GetContactsAsync();

            IsBusy = false;
        }

        private async Task OnAddContact()
        {
            await NavigationService.NavigateToAsync<SaveContactViewModel>();
        }

        private async Task OnEditContact(Contact contact)
        {
            SelectedContact = contact;
            await NavigationService.NavigateToAsync<SaveContactViewModel>(contact);
        }

        private async Task OnDeleteContact(Contact contact)
        {
            await _contactService.DeleteContactAsync(contact);
            Contacts.Remove(contact);
        }

        private async Task OnShowCarousel()
        {
            await NavigationService.NavigateModalAsync<ContactCarouselViewModel>();
        }

    }
}
