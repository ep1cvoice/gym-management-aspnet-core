namespace GymApp.Models.Notifications
{
    public interface INotification
    {
        void Send(string recipient, string message);
    }
}
