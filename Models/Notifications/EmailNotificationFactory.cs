namespace GymApp.Models.Notifications
{
    public class EmailNotificationFactory : NotificationFactory
    {
        public override INotification CreateNotification()
        {
            return new EmailNotification();
        }
    }
}
