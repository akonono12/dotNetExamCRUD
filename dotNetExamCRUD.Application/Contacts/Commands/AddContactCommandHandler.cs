using CustomerCRUDWebApp.Domain.CustomerManagement.Entities;
using dotNetExamCRUD.Common.Shared;
using dotNetExamCRUD.Domain.ContactManagement.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Commands
{
    internal class AddContactCommandHandler : IRequestHandler<AddContactCommand, CommandResult<Guid>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IValidator<AddContactCommand> _validator;
        public AddContactCommandHandler(IContactRepository  contactRepository, IValidator<AddContactCommand> validator) { 
            _contactRepository = contactRepository;
            _validator = validator;
        }
        public async Task<CommandResult<Guid>> Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid) 
            { 
                return new CommandResult<Guid> { Success = false,Data = Guid.NewGuid(), Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList() };
            }
            var contact = new Contact(request.FirstName, request.LastName,request.CompanyName,request.Email,request.ContactNumber);

            _contactRepository.AddContact(contact);
            await _contactRepository.SaveChangesAsync();
            return new CommandResult<Guid> { Success = true,Data=contact.ContactID};
        }

    }
}
