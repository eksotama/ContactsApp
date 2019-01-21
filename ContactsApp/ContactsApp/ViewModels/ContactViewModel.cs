using ContactsApp.Models;
using ContactsApp.ViewModels.Base;

namespace ContactsApp.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        readonly Contact contact;

        public string Name
        {
            get => contact.Name;
            set
            {
                if (contact.Name != value)
                {
                    contact.Name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Number
        {
            get => contact.Number;
            set
            {
                if (contact.Number != value)
                {
                    contact.Number = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ContactViewModel()
        {
            contact = new Contact();
        }

        public ContactViewModel(Contact contact)
        {
            this.contact = contact;
        }
    }
}
