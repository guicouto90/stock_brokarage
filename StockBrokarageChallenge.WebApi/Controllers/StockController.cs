using Microsoft.AspNetCore.Mvc;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.UseCases.StockContext;
using StockBrokarageChallenge.Application.UseCases.StockContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;

namespace StockBrokarageChallenge.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IRequestHandlerCollection _requestHandlers;

        public StockController(IRequestHandlerCollection requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StockCreateInput input)
        {
            try
            {
                var output = await _requestHandlers.Using<StockCreateUseCase>()
                .ExecuteAsync(input);
                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return BadRequest(output);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<StockOutput>), 200)]
        public async Task<IActionResult> ListAllAsync()
        {
            try
            {
                var output = await _requestHandlers.Using<StockGetAllUseCase>().ExecuteAsync(null);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Retorna ações por nome ou código
        /// </summary>
        /// <param name="filter">Filter by name or code</param>
        /// <returns>Returns a list of stocks by name or code</returns>
        [HttpGet("code-or-name")]
        [ProducesResponseType(typeof(StockOutput), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ListAsyncByNameOrCode([FromQuery] string filter)
        {
            try
            {
                var output = await _requestHandlers.Using<StockGetByCodeOrNameUseCase>().ExecuteAsync(filter);
                if (output != null)
                {
                    return Ok(output);
                }
                return NotFound("Ação não encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }
    }
}
