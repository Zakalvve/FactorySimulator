namespace BigBearPlastics
{
    public interface IServicerPerformanceIndicators : IKeyPerformaceIndicators
    {
        public int CurrentWork { get; set;  }
        public double CurrentWorkInMinutes { get; }
        public void Tick();
        public ServicerPerformanceModel ExtractData();
    }
}