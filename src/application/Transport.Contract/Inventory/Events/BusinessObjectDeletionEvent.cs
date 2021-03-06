using Super.Paula.Application.Orchestration;
using Super.Paula.Validation;
using System.ComponentModel.DataAnnotations;

namespace Super.Paula.Application.Inventory.Events
{
    [AllowedSubscriber(AllowedSubscribers.BusinessObjectInspectionAuditRecord)]
    [AllowedSubscriber(AllowedSubscribers.Inspector)]
    public record BusinessObjectDeletionEvent(

        [StringLength(140)]
        [UniqueName]
        [KebabCase]
        string UniqueName)

        : EventBase();
}