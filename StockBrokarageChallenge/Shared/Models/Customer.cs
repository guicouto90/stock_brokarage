using StockBrokarageChallenge.Application.Shared.Abstractions;

namespace StockBrokarageChallenge.Application.Shared.Models
{
    public sealed class Customer
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Cpf { get; }
        // public int AccountId { get; private set; }
        public Account Account { get; private set; }

        public Customer(string name, string cpf)
        {
            DomainExceptionValidation.When(cpf.Length != 11, "Cpf must have length of 11 characters");
            Name = name;
            Cpf = cpf;
        }

        public Customer(string name, string cpf, int accountNumber, string password)
        {
            DomainExceptionValidation.When(cpf.Length != 11, "Cpf must have length of 11 characters");
            DomainExceptionValidation.When(password.Length < 6, "Password must have at least length of 6 characters");
            DomainExceptionValidation.When(name.Length < 3, "Name must have at least length of 3 characters");
            Name = name;
            Cpf = cpf;
            Account = new Account(accountNumber, password, this);
        }

        public void UpdateAccount(Account account) {  Account = account; }
    }
}
