using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Models
{
    [Table("contacts")]
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }
    }
}
