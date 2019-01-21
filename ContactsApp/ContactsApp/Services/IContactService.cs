using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Services
{
    public interface IContactService
    {
        Task<ObservableCollection<Contact>> GetContactsAsync();
        Task<Contact> GetContactAsync(int contactId);
        Task AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(Contact contact);
    }
}
