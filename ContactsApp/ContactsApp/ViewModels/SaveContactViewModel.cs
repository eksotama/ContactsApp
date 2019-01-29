using ContactsApp.Models;
using ContactsApp.Services;
using ContactsApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class SaveContactViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;
        private bool isNew = true;

        private string name;
        private string number;
        private Contact _selectedContact;
        private Contact editContact;

        public SaveContactViewModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        public Contact EditContact
        {
            get => editContact;
            set
            {
                editContact = value;
                RaisePropertyChanged();
            }
        }

        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SaveContact => new Command<Contact>(async (c) => await OnSaveContact());

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                SelectedContact = (Contact)navigationData;

                EditContact = new Contact();
                EditContact.Name = SelectedContact.Name;
                EditContact.Number = SelectedContact.Number;

                isNew = false;
            }
            else
            {
                SelectedContact = EditContact = new Contact();
            }

            await base.InitializeAsync(navigationData);
        }

        private async Task OnSaveContact()
        {
            SelectedContact.Name = EditContact.Name;
            SelectedContact.Number = EditContact.Number;

            if (isNew == true)
            {
                await _contactService.AddContactAsync(_selectedContact);
                MessagingCenter.Send(this, "addContact", _selectedContact);
            }
            else
            {
                await _contactService.UpdateContactAsync(SelectedContact);
                await NavigationService.GoBack();
            }
            
        }
    }
}
