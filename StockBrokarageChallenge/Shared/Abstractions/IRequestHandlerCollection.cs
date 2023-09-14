using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.Application.Shared.Abstractions
{
    public interface IRequestHandlerCollection
    {
        IRequestHandler Using<IRequestHandler>();
    }
}
