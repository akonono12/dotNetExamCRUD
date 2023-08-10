using dotNetExamCRUD.Common.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Commands
{
    public class AddContactCommand:IRequest<CommandResult<Guid>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }
}
