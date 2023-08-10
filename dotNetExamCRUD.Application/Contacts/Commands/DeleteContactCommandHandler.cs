using dotNetExamCRUD.Common.Shared;
using dotNetExamCRUD.Domain.ContactManagement.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Commands
{
    internal class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, CommandResult<bool>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IValidator<DeleteContactCommand> _validator;
        public DeleteContactCommandHandler(IContactRepository contactRepository, IValidator<DeleteContactCommand> validator)
        {
            _contactRepository = contactRepository;
            _validator = validator;
        }
        public async Task<CommandResult<bool>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CommandResult<bool> { Success = false, Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList() };
            }

            var contact = await _contactRepository.GetAllContacts().SingleOrDefaultAsync(x => x.ContactID == request.Id);
       
            contact.Delete();
            await _contactRepository.SaveChangesAsync();

            return new CommandResult<bool> {Success = true,Data=true };
           
        }
    }
}
