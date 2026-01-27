using GymApp.Models.Notifications;

namespace GymApp.Services.Notifications
{
    public sealed class NotificationManager
    {
        private static readonly Lazy<NotificationManager> _instance =
            new(() => new NotificationManager());

        public static NotificationManager Instance => _instance.Value;

        private NotificationManager() { }

        public void Send(
            INotification notification,
            string recipient,
            string message)
        {
            notification.Send(recipient, message);
        }
    }
}
