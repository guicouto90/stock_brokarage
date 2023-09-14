using Microsoft.Extensions.Configuration;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.Shared.Security;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Outputs;

namespace StockBrokarageChallenge.Application.UseCases.LoginContext
{
    public class AuthenticateUseCase : UseCaseBase,
        IRequestHandler<LoginInput, LoginOutput>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public AuthenticateUseCase(
            IRequestHandlerCollection useCases, 
            IAccountRepository accountRepository, 
            ICustomerRepository customerRepository,
            IConfiguration configuration) : base(useCases)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _configuration = configuration;
        }

        public async Task<LoginOutput> ExecuteAsync(LoginInput? input)
        {
            var result = await _customerRepository.GetByCpfAsync(input.CustomerCpf);
            if(result == null || !result.Account.VerifyPassword(input.Password))
            {
                throw new HttpRequestException("Cpf/password invalid", null, System.Net.HttpStatusCode.Unauthorized);
            }

            return GenerateBearerToken.GenerateJwt(_configuration, result.Id, result.Account.Id, result.Account.AccountNumber);
        }
    }
}
