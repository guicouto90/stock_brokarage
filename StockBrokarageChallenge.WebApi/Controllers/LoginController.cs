using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.UseCases.LoginContext;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Outputs;

namespace StockBrokarageChallenge.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IRequestHandlerCollection _requestHandlers;

        public LoginController(IRequestHandlerCollection requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInput input) 
        { 
            try
            {
                var result = await _requestHandlers.Using<AuthenticateUseCase>().ExecuteAsync(input);
                return Ok(result);
            } catch(HttpRequestException ex)
            {
                return Unauthorized(ex.Message);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
