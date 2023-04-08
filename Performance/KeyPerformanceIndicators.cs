namespace BigBearPlastics
{
    public class KeyPerformanceIndicators : IKeyPerformaceIndicators
    {
        protected ISimulationClock _clock = new SimulationClock();
        public int Uptime { get; set; }

        public double AverageUptime {
            get {
                return Math.Round(((Uptime / (double)(_clock.ElapsedTime)) * 100),2);
            }
        }
    }
}
