using dotNetExamCRUD.Application.Contacts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Queries
{
    public class SearchContactQueryResultDto
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public List<ContactDto> Results { get; set; }
        public int TotalCount { get; set; }
    }
}
