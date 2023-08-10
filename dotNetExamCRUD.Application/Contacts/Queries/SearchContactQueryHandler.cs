using dotNetExamCRUD.Application.Contacts.DTOs;
using dotNetExamCRUD.Domain.ContactManagement.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetExamCRUD.Application.Contacts.Queries
{
    internal class SearchContactQueryHandler : IRequestHandler<SearchContactQuery, SearchContactQueryResultDto>
    {
        private readonly IContactRepository _contactRepository;
        public SearchContactQueryHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<SearchContactQueryResultDto> Handle(SearchContactQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchContactQueryResultDto();

            var query = _contactRepository.GetAllContacts();

            if (!string.IsNullOrWhiteSpace(request.Filter?.FirstName))
            {
                query = query.Where(x => x.FirstName == request.Filter.FirstName.TrimEnd());
            }
            if (!string.IsNullOrWhiteSpace(request.Filter?.LastName))
            {
                query = query.Where(x => x.LastName == request.Filter.LastName.TrimEnd());
            }
            if (!string.IsNullOrWhiteSpace(request.Filter?.CompanyName))
            {
                query = query.Where(x => x.CompanyName == request.Filter.CompanyName.TrimEnd());
            }
            if (!string.IsNullOrWhiteSpace(request.Filter?.Email))
            {
                query = query.Where(x => x.Email == request.Filter.Email.TrimEnd());
            }
            if (!string.IsNullOrWhiteSpace(request.Filter?.ContactNumber))
            {
                query = query.Where(x => x.ContactNumber == request.Filter.ContactNumber.TrimEnd());
            }

            var count = await query.CountAsync();

            var contacts = await query
            .OrderByDescending(x => x.DateCreated)
            .Skip(request.PageSize * (request.PageIndex - 1))
            .Take(request.PageSize)
            .ToListAsync();

            var contactList = new List<ContactDto>();
            foreach (var contact in contacts)
            {
                contactList.Add(new ContactDto()
                {
                    CompanyName = contact.CompanyName,
                    Email = contact.Email,
                    ContactNumber = contact.ContactNumber,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    CreatedDate = contact.DateCreated,
                    ContactId = contact.ContactID,
                });
            }

            result.PageSize = request.PageSize;
            result.PageIndex = request.PageIndex;
            result.TotalCount = count;
            result.Results = contactList;

            return result;
        }
    }
}
