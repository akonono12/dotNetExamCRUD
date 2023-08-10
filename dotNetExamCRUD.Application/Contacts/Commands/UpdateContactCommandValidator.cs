using dotNetExamCRUD.Domain.ContactManagement.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Commands
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator(IContactRepository repository)
        {
            RuleFor(x => x.ContactId)
            .MustAsync(async (id, token) =>
            {
                var result = await repository.GetAllContacts().AnyAsync(x => x.ContactID == id, token);

                return result;
            })
            .When(x => x.ContactId != Guid.Empty)
            .WithMessage("Contact detail has not been found");

            RuleFor(x => x.FirstName)
           .NotEmpty()
           .WithMessage("First name must not be empty");

            RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name must not be empty");

            RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Company name must not be empty");

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email must not be empty")
            .EmailAddress()
            .WithMessage("Invalid Email");

            RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .WithMessage("Contact number must not be empty");

            RuleFor(x => x)
            .MustAsync(async (model, token) =>
            {
                var result = await repository.GetAllContacts().SingleOrDefaultAsync(x => x.Email == model.Email && x.ContactID != model.ContactId, token);
                if (result == null)
                {
                    return true;
                }
                return false;
            })
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Email has already exist");

            RuleFor(x => x)
            .MustAsync(async (model, token) =>
            {
                var result = await repository.GetAllContacts().SingleOrDefaultAsync(x => x.ContactNumber == model.ContactNumber && x.ContactID != model.ContactId, token);

                if (result == null)
                {
                    return true;
                }
                return false;
            })
            .When(x => !string.IsNullOrEmpty(x.ContactNumber))
            .WithMessage("Contact number has already exist");

        }
    }
}
