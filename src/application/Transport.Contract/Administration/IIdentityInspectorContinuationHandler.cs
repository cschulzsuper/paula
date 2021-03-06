using Super.Paula.Application.Administration.Continuation;
using Super.Paula.Application.Orchestration;

namespace Super.Paula.Application.Administration
{
    public interface IIdentityInspectorContinuationHandler :
        IContinuationHandler<CreateIdentityInspectorContinuation>,
        IContinuationHandler<ActivateIdentityInspectorContinuation>,
        IContinuationHandler<DeactivateIdentityInspectorContinuation>,
        IContinuationHandler<DeleteIdentityInspectorContinuation>
    {
    }
}