using ContactsApp.Models;
using ContactsApp.Services;
using ContactsApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.ViewModels
{
    public class ContactGridViewModel : BaseViewModel
    {
        private IContactService _contactService;

        private ObservableCollection<Contact> contacts;

        public ContactGridViewModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        public ObservableCollection<Contact> Contacts
        {
            get { return contacts; }
            set
            {
                SetPropertyValue(ref contacts, value);
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            // Contacts = (ObservableCollection<Contact>)navigationData;
            Contacts = await _contactService.GetContactsAsync();
           // return base.InitializeAsync(navigationData);
        }
    }
}
