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
    public class AddContactCommandValidator : AbstractValidator<AddContactCommand>
    {
        public AddContactCommandValidator(IContactRepository repository) {
            
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

            RuleFor(x => x.Email)
            .MustAsync(async (email, token) =>
            {
                var result = await repository.GetAllContacts().AnyAsync(x => x.Email == email,token);
               
                return !result;
            })
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Email has already exist");

            RuleFor(x => x.ContactNumber)
            .MustAsync(async (contact, token) =>
            {
                var result = await repository.GetAllContacts().AnyAsync(x => x.ContactNumber == contact, token);

                return !result;
            })
            .When(x => !string.IsNullOrEmpty(x.ContactNumber))
            .WithMessage("Contact number has already exist");

        }
    }
}
