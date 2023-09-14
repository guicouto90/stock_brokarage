using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace StockBrokarageChallenge.Application.Shared.Abstractions
{
    public class RequestHandlerCollection : IRequestHandlerCollection
    {
        private readonly ILogger<RequestHandlerCollection> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RequestHandlerCollection(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRequestHandler Using<IRequestHandler>()
        {
            try
            {
                //Pesquisar sobre essa linha:
                var useCase = _serviceProvider.CreateScope().ServiceProvider.GetService<IRequestHandler>();

                if (useCase == null)
                {
                    throw new InvalidOperationException($"Use case \"{typeof(IRequestHandler).Name}\" not implemented.");
                }

                return useCase;
            } catch(Exception ex)
            {
                _logger.LogError(ex, "An unexpected error ocurred during use case retrieval.");
                throw;
            }
        }
    }
}
