using CustomerCRUDWebApp.Domain.CustomerManagement.Entities;
using dotNetExamCRUD.Common.Shared;
using dotNetExamCRUD.Domain.ContactManagement.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace dotNetExamCRUD.Application.Contacts.Commands
{
    internal class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, CommandResult<Unit>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IValidator<UpdateContactCommand> _validator;
        public UpdateContactCommandHandler(IContactRepository contactRepository, IValidator<UpdateContactCommand> validator)
        {
            _contactRepository = contactRepository;
            _validator = validator;
        }
        public async Task<CommandResult<Unit>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CommandResult<Unit> { Success = false, Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList() };
            }

            var contact = await _contactRepository.GetAllContacts().SingleOrDefaultAsync(x => x.ContactID == request.ContactId);

            contact.Update(request.FirstName,request.LastName,request.CompanyName,request.Email,request.ContactNumber);
            await _contactRepository.SaveChangesAsync();

            return new CommandResult<Unit> { Success = true, Data = Unit.Value };
        }
    }
}
