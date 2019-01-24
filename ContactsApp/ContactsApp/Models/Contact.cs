using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ContactsApp.Models
{
    [Table("contacts")]
    public class Contact : INotifyPropertyChanged
    {
        private string name;
        private string number;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storageField, newValue))
                return false;

            storageField = newValue;
            this.RaisePropertyChanged(propertyName);

            return true;
        }
    }
}
