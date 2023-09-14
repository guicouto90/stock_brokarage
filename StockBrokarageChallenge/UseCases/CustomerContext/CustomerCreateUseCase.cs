﻿using AutoMapper;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs;
using StockBrokarageChallenge.Application.UseCases.CustomerContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.CustomerContext.Outputs;

namespace StockBrokarageChallenge.Application.UseCases.CustomerContext
{
    public class CustomerCreateUseCase : UseCaseBase,
        IRequestHandler<CustomerCreateInput, CustomerOutput>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public CustomerCreateUseCase(
            IRequestHandlerCollection useCases,
            ICustomerRepository customerRepository, 
            IAccountRepository accountRepository, 
            IMapper mapper) : base(useCases)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<CustomerOutput> ExecuteAsync(CustomerCreateInput? input)
        {
            var customerExist = await _customerRepository.GetByCpfAsync(input.Cpf);
            if (customerExist != null)
            {
                return null;
            }
            var lastAccount = await _accountRepository.GetLastAccount();
            var accountNumber = lastAccount == null ? 1 : lastAccount.AccountNumber + 1;
            var customer = new Customer(input.Name, input.Cpf, accountNumber, input.Password);
            await _customerRepository.Create(customer);

            return _mapper.Map<CustomerOutput>(customer);
        }
    }
}