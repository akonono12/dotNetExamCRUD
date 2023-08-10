using dotNetExamCRUD.Common.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Commands
{
    public class DeleteContactCommand:IRequest<CommandResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
