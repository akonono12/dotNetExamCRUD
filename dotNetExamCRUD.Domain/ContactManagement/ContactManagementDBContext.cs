using CustomerCRUDWebApp.Domain.CustomerManagement.Entities;
using dotNetExamCRUD.Domain.ContactManagement.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Domain.ContactManagement
{
    public class ContactManagementDBContext:DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public ContactManagementDBContext(DbContextOptions<ContactManagementDBContext>
        options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ContactConfiguration());
        }
    }
}
