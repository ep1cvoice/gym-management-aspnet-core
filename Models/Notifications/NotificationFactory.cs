namespace GymApp.Models.Notifications
{
    public abstract class NotificationFactory
    {
        public abstract INotification CreateNotification();
    }
}
