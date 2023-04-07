namespace BigBearPlastics
{
    public class MessageLogger : IMessageLogger
    {
        public string Prefix { get; set; } = "";

        public void LogMessage(string message) {
            Console.WriteLine(message);
        }

        public void LogSignedMessage(string message) {
            if (String.IsNullOrEmpty(Prefix)) LogMessage(message); else Console.WriteLine($"{Prefix} {message}");
        }
    }
}
