namespace BigBearPlastics
{
    public class MachinePerformanceIndicators : KeyPerformanceIndicators, IMachinePerformanceIndicators
    {
        public int TotalPartsProduced { get; set; }
        public double AveragePartsPerHour {
            get {
                return Math.Round(TotalPartsProduced / _clock.ElapsedTimeInHours, 2);
            }
        }
    }
}
