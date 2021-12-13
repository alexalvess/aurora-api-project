using Application.Ports.NotificationServices;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Application.NotificationPattern;

public class NotificationContext : INotificationContext
{
    private List<ValidationFailure> _notifications;

    public NotificationContext()
        => _notifications = new List<ValidationFailure>();

    public IReadOnlyCollection<ValidationFailure> Notifications
        => _notifications?.AsReadOnly();

    public bool HasNotifications
        => _notifications.Any();

    public void AddNotification(string errorMessage)
        => _notifications.Add(new ValidationFailure(default, errorMessage));

    public void AddNotification(ValidationFailure validationFailure)
        => VerifyAndAddNotification(validationFailure);

    public void AddNotification(ValidationResult validationResult)
        => validationResult.Errors.ForEach(failure => VerifyAndAddNotification(failure));

    private void VerifyAndAddNotification(ValidationFailure validationFailure)
    {
        if (_notifications.Any(notification => notification.PropertyName.Equals(validationFailure.PropertyName)))
            return;

        _notifications.Add(validationFailure);
    }
}