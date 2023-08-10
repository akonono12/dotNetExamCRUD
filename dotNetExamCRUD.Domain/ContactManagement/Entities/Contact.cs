using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRUDWebApp.Domain.CustomerManagement.Entities
{
    public class Contact
    {
        public Guid ContactID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CompanyName { get; private set; }
        public string Email { get; private set; }
        public string ContactNumber { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateUpdated { get; private  set; }
        public DateTime? DateDeleted { get; private set; }
        private Contact()
        {

        }

        public Contact(string firstName,string lastName,string companyName,string email,string contactNumber)
        {
            ContactID = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
            Email = email;
            ContactNumber = contactNumber;
            DateCreated = DateTime.Now;
        }

        public void Update(string firstName, string lastName, string companyName, string email, string contactNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
            Email = email;
            ContactNumber = contactNumber;
            DateUpdated = DateTime.Now;
        }

        public void Delete()
        {
            DateDeleted = DateTime.Now;
        }
    }
}
