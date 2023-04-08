namespace BigBearPlastics
{
    public interface ISimulationClock
    {
        public double ToMinutes(int time);
        public double ToHours(int time);
        public int ElapsedTime { get; }
        public double ElapsedTimeInMinutes { get; }
        public double ElapsedTimeInHours { get; }
        string Time { get; }
    }
}