namespace gym_manager_dotnet.Models.Notifications
{
    public class EmailNotificationFactory : NotificationFactory
    {
        public override INotification CreateNotification()
        {
            return new EmailNotification();
        }
    }
}
