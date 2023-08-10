using dotNetExamCRUD.Domain.ContactManagement.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Commands
{
   
    public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
    {
        public DeleteContactCommandValidator(IContactRepository repository)
        {
            RuleFor(x => x.Id)
         .MustAsync(async (id, token) =>
         {
             var result = await repository.GetAllContacts().AnyAsync(x => x.ContactID == id, token);

             return result;
         })
         .When(x => x.Id != Guid.Empty)
         .WithMessage("Contact detail has not been found");

        }
    }
}
