using FluentValidation.Results;
using System.Collections.Generic;

namespace Application.Ports.NotificationServices;

public interface INotificationContext
{
    IReadOnlyCollection<ValidationFailure> Notifications { get; }

    bool HasNotifications { get; }

    void AddNotification(string errorMessage);

    void AddNotification(ValidationFailure validationFailure);

    void AddNotification(ValidationResult validationResult);
}