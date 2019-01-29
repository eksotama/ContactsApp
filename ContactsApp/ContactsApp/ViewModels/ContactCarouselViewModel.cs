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
    public class ContactCarouselViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;

        private ObservableCollection<Contact> contacts;

        public ContactCarouselViewModel(IContactService contactService)
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
            IsBusy = true;

            Contacts = await _contactService.GetContactsAsync();

            IsBusy = false;
        }

    }
}
