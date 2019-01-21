using System;
using System.Collections.ObjectModel;
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

        public async Task<Contact> GetContactAsync(int contactId)
        {
            return await connection.Table<Contact>().Where(c => c.Id == contactId).FirstOrDefaultAsync();
        }

        public async Task AddContactAsync(Contact contact)
        {
            //basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(contact.Name))
                throw new Exception("Valid name required");

            await connection.InsertAsync(contact);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            //basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(contact.Name))
                throw new Exception("Valid name required");

            await connection.UpdateAsync(contact);
        }

        public async Task DeleteContactAsync(Contact contact)
        {
            await connection.DeleteAsync(contact);
        }
    }
}
