using CustomerCRUDWebApp.Application.Customers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Queries
{
    public class SearchContactQuery:IRequest<SearchContactQueryResultDto>
    {
        public SearchContactsQueryFilters? Filter { get; set; }
        public int PageIndex { get; set; }
        public int PageSize => 10;
    }
}
