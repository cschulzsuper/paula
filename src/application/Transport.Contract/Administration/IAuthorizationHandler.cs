using System.Threading.Tasks;

namespace Super.Paula.Application.Administration
{
    public interface IAuthorizationHandler
    {
        ValueTask<string> AuthorizeAsync(string organization, string inspector);
        ValueTask<string> StartImpersonationAsync(string organization, string inspector);
        ValueTask<string> StopImpersonationAsync();
    }
}