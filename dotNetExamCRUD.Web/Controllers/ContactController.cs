using dotNetExamCRUD.Application.Contacts.Commands;
using dotNetExamCRUD.Application.Contacts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace dotNetExamCRUD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchContacts([FromQuery] SearchContactQuery request)
        {
            return Ok(await _mediator.Send(request));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] UpdateContactCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact([FromBody] DeleteContactCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] AddContactCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
