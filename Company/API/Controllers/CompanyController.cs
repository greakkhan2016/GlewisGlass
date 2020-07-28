using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Commuication;
using Application.Companies;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Exceptions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet()]
        public async Task<ActionResult<List<Company>>> List()
        {
            return await _mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch(CompanyException cex)
            {
                var response = new Response(cex.Message);
                return BadRequest(response);
            }
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Company>> Details(int id)
        {
            return await _mediator.Send(new Details.Query { Id = id });
        }

    }
}
