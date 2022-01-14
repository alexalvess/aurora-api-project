using Application.Ports.NotificationServices;
using FluentValidation.Results;
using System;
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

    public void AddNotification(string field, string error)
        => VerifyAndAddNotification(field, error);

    public void AddNotifications(List<ValidationFailure> validationsFailure)
        => validationsFailure?.ForEach(item => VerifyAndAddNotification(item));

    private void VerifyAndAddNotification(string field, string error)
    {
        var validationFailure = new ValidationFailure(field, error);
        VerifyAndAddNotification(validationFailure);
    }

    private void VerifyAndAddNotification(ValidationFailure validationFailure)
    {
        if (_notifications.Any(notification => notification.PropertyName.Equals(validationFailure.PropertyName)))
            return;

        var errorCode = validationFailure.PropertyName.GetHashCode();

        validationFailure.ErrorCode = errorCode < 0 ? (errorCode * -1).ToString() : errorCode.ToString();

        _notifications.Add(validationFailure);
    }
}