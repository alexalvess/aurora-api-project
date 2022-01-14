using FluentValidation.Results;
using System.Collections.Generic;

namespace Application.Ports.NotificationServices;

public interface INotificationContext
{
    IReadOnlyCollection<ValidationFailure> Notifications { get; }

    bool HasNotifications { get; }

    void AddNotification(string field, string error);

    void AddNotifications(List<ValidationFailure> validationsFailure);
}