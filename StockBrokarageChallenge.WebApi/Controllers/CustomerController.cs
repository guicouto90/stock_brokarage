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
            try
            {
                var output = await _requestHandlers.Using<CustomerCreateUseCase>()
                .ExecuteAsync(input);

                return Ok($"Customer created with account number {output.Account.AccountNumber}");
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
    }
}
