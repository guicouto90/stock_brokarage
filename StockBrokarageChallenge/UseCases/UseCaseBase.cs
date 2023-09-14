using StockBrokarageChallenge.Application.Shared.Abstractions;

namespace StockBrokarageChallenge.Application.UseCases
{
    public class UseCaseBase
    {
        protected IRequestHandlerCollection UseCases { get; }

        public UseCaseBase(IRequestHandlerCollection useCases)
        {
            UseCases = useCases ?? throw new ArgumentNullException(nameof(useCases));
        }
    }
}
