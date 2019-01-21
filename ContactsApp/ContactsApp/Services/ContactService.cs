using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Models;
using SQLite;
using Xamarin.Forms;

namespace ContactsApp.Services
{
    public class ContactService : IContactService
    {
        private readonly SQLiteAsyncConnection connection;

        public ContactService()
        {
            connection = DependencyService.Get<ISQLiteService>().GetConnection("contacts.db");
            connection.CreateTableAsync<Contact>().Wait();
            //connection.InsertAsync(new Contact { Name = "Ilya", Number = "767-854-0441" });
            //connection.InsertAsync(new Contact { Name = "Grace", Number = "999-999-9999" });
            //connection.InsertAsync(new Contact { Name = "Mike", Number = "123-456-7896" });
        }
        public async Task<ObservableCollection<Contact>> GetContactsAsync()
        {
            var contacts = await connection.Table<Contact>().ToListAsync();

            return new ObservableCollection<Contact>(contacts);
        }

        public Task AddContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetContactAsync(int contactId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
