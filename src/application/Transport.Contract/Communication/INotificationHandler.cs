using Super.Paula.Application.Communication.Requests;
using Super.Paula.Application.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Super.Paula.Application.Communication
{
    public interface INotificationHandler
    {
        ValueTask<NotificationResponse> GetAsync(string inspector, int date, int time);
        IAsyncEnumerable<NotificationResponse> GetAll();
        IAsyncEnumerable<NotificationResponse> GetAllForInspector(string inspector);

        ValueTask<NotificationResponse> CreateAsync(string inspector, NotificationRequest request);
        ValueTask ReplaceAsync(string inspector, int date, int time, NotificationRequest request);
        ValueTask DeleteAsync(string inspector, int date, int time, string etag);


        Task<IDisposable> OnCreationAsync(Func<NotificationResponse, Task> handler);
        Task<IDisposable> OnDeletionAsync(Func<string, int, int, Task> handler);
    }
}