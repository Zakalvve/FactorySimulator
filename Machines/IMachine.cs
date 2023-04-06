namespace BigBearPlastics
{
    public interface IMachine : IStateful, IServicableMachine
    {
        public int ID { get; set; }
        public int Priority { get; set; }
        Queue<IJobModel> Jobs { get; set; }
        IJobModel CurrentJob { get; set; }
        IContainer? InputContainer { get; set; }
        IContainer? OutputContainer { get; set; }
        IContainer? ScrapContainer { get; set; }
        public int SecondsPerPart { get; }
        public bool CanRun { get; }
        public void LogMessage(string message);
        public bool NextJob();
        public int Uptime { get; set; }
        public int Downtime { get; set; }
    }
}