using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRUDWebApp.Domain.CustomerManagement.Entities;

namespace dotNetExamCRUD.Domain.ContactManagement.Configurations
{
    internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.ContactID);
            builder.HasQueryFilter(x => x.DateDeleted == null);
        }
    }
}
