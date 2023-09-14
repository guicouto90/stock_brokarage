using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.CustomerContext;
using StockBrokarageChallenge.Application.UseCases.CustomerContext.Inputs;

namespace StockBrokarageChallenge.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IRequestHandlerCollection _requestHandlers;

        public CustomerController(IRequestHandlerCollection requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerCreateInput input)
        {
            var output = await _requestHandlers.Using<CustomerCreateUseCase>()
                .ExecuteAsync(input);
            if(output != null)
            {
                return Ok($"Customer created with account number {output.Account.AccountNumber}");// return CreatedAtAction(nameof(Customer), new { id = output.Id }, $"Customer created with account number {output.Account.AccountNumber}");
            } else
            {
                return BadRequest("Customer already have an account");
            }
        }
    }
}
