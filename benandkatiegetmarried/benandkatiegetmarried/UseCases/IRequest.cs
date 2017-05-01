using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.UseCases
{
    public interface IRequest<TResponse> {}

    public interface IRequestHandler<T, TResponse> where T: IRequest<TResponse>
    {
        TResponse Handle(T request);
    }
}
