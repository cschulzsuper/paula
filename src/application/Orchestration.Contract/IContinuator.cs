using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Super.Paula.Application.Orchestration
{
    public interface IContinuator
    {
        void Configure(Action<ContinuationHandlerContext> startup);

        Task ExecuteAsync<TContinuation>(TContinuation continuation, ClaimsPrincipal? user = null)
            where TContinuation : ContinuationBase;
    }
}
