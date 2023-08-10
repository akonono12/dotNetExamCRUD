using CustomerCRUDWebApp.Domain.CustomerManagement.Entities;
using dotNetExamCRUD.Domain.ContactManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Domain.ContactManagement.Repositories
{
    public class ContactRepository:IContactRepository
    {
        private readonly ContactManagementDBContext _dbContext;
        public ContactRepository(ContactManagementDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IQueryable<Contact> GetAllContacts()
        {
            var result = _dbContext.Contacts;
            return result.AsQueryable();
        }

        public void AddContact(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
