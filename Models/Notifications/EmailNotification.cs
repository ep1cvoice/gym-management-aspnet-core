namespace GymApp.Models.Notifications
{
    public class EmailNotification : INotification
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine("===== EMAIL =====");
            Console.WriteLine($"Do: {recipient}");
            Console.WriteLine($"Treść: {message}");
            Console.WriteLine("=================");
        }
    }
}
