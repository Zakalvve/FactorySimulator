namespace BigBearPlastics
{
    public interface IMessageLogger
    {
        public string Prefix { get; set; }
        public void LogMessage(string message);
        public void LogSignedMessage(string message);
    }
}
