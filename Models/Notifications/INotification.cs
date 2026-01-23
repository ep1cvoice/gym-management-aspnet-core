namespace gym_manager_dotnet.Models.Notifications
{
    public interface INotification
    {
        void Send(string recipient, string message);
    }
}
