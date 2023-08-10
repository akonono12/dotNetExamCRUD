using CustomerCRUDWebApp.Domain.CustomerManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Domain.ContactManagement.Interfaces
{
    public interface IContactRepository
    {
        IQueryable<Contact> GetAllContacts();
        void AddContact(Contact contact);
        Task SaveChangesAsync();
    }
}
