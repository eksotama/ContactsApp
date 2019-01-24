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

        public SaveContactViewModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        public string Name
        {
            get => name;
            set
            {
                SetPropertyValue(ref name, value);
            }
        }

        public string Number
        {
            get => number;
            set
            {
                SetPropertyValue(ref number, value);
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

                Name = SelectedContact.Name;
                Number = SelectedContact.Number;

                isNew = false;
            }
            else
            {
                SelectedContact = new Contact();
            }

            await base.InitializeAsync(navigationData);
        }

        private async Task OnSaveContact()
        {
            SelectedContact.Name = Name;
            SelectedContact.Number = Number;

            if (isNew == true)
            {
                await _contactService.AddContactAsync(_selectedContact);
                MessagingCenter.Send(this, "addContact", _selectedContact);
            }
            else
            {
                await _contactService.UpdateContactAsync(SelectedContact);
            }
        }
    }
}
