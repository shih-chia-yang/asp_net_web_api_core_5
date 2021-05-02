using System.Threading;
using System.Threading.Tasks;

namespace code.Domain.Event
{
    public abstract class AbstractRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken) => Task.FromResult(Handle(request));

        protected abstract TResponse Handle(TRequest request);
    }
}