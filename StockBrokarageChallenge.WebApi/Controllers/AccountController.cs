using Microsoft.AspNetCore.Mvc;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.UseCases.CustomerContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.CustomerContext;
using StockBrokarageChallenge.Application.UseCases.AccountContext;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace StockBrokarageChallenge.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IRequestHandlerCollection _requestHandlers;

        public AccountController(IRequestHandlerCollection requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }

        [HttpPut("deposit")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DepositValue([FromBody] AccountWithdrawDepositValueInput input)
        {
            try
            {
                var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
                input.CustomerId = int.Parse(customerId);
                var output = await _requestHandlers.Using<AccountDepositValueUseCase>()
                    .ExecuteAsync(input).ConfigureAwait(false);
                if (output != null)
                {
                    return output == "Deposit succeed" ? Ok(output) : BadRequest(output);
                }
                else
                {
                    return BadRequest("Account doesnt exist.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Access denied to this account")
                {
                    return Unauthorized(ex.Message);
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpPut("withdraw")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> WithdrawValue([FromBody] AccountWithdrawDepositValueInput input)
        {
            try
            {
                var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
                input.CustomerId = int.Parse(customerId);
                var output = await _requestHandlers.Using<AccountWithdrawValueUseCase>()
                    .ExecuteAsync(input).ConfigureAwait(false);
                if (output != null)
                {
                    return output == "Withdraw succeed" ? Ok(output) : BadRequest(output);
                }
                else
                {
                    return BadRequest("Account doesnt exist.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Access denied to this account")
                {
                    return Unauthorized(ex.Message);
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }

        }

        [HttpGet("transaction-history")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> TransactionHistory()
        {
            try
            {
                var customerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "customerId").Value);
                var output = await _requestHandlers.Using<AccountListTransactionHistoryUseCase>()
                    .ExecuteAsync(customerId);
                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return BadRequest("Account doesnt exist.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Access denied to this account")
                {
                    return Unauthorized(ex.Message);
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpGet("stock-transaction-history")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> StockTransactionHistory()
        {
            try
            {
                var customerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "customerId").Value);
                var output = await _requestHandlers.Using<AccountStockTransactionsUseCase>()
                    .ExecuteAsync(customerId).ConfigureAwait(false);
                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return BadRequest("Account doesnt exist.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Access denied to this account")
                {
                    return Unauthorized(ex.Message);
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpGet("wallet")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ListWallet()
        {
            try
            {
                var customerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "customerId").Value);
                var output = await _requestHandlers.Using<AccountListAccountUseCase>()
                    .ExecuteAsync(customerId).ConfigureAwait(false);
                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return BadRequest("Account doesnt exist.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Access denied to this account")
                {
                    return Unauthorized(ex.Message);
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpPut("buy-stock")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> BuyStocks([FromBody] AccountBuySellStocksInputs input)
        {
            try
            {
                var customerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "customerId").Value);
                input.CustomerId = customerId;
                var output = await _requestHandlers.Using<AccountBuyStockUseCase>().ExecuteAsync(input).ConfigureAwait(false);
                return Ok(output);
            }
            catch (HttpRequestException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return BadRequest(ex.Message);
                } else if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound(ex.Message);
                } else
                {
                    return StatusCode(500, ex.Message);
                }
            } catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("sell-stock")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SellStocks([FromBody] AccountBuySellStocksInputs input)
        {
            try
            {
                var customerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "customerId").Value);
                input.CustomerId = customerId;
                var output = await _requestHandlers.Using<AccountSellStockUseCase>().ExecuteAsync(input).ConfigureAwait(false);
                return Ok(output);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return BadRequest(ex.Message);
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
