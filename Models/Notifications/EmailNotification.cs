using System.Diagnostics;

namespace gym_manager_dotnet.Models.Notifications
{
    public class EmailNotification : INotification
    {
        public void Send(string recipient, string message)
        {
            // SYMULACJA wysyłania maila
            Debug.WriteLine("===== EMAIL =====");
            Debug.WriteLine($"Do: {recipient}");
            Debug.WriteLine($"Treść: {message}");
            Debug.WriteLine("=================");
        }
    }
}
