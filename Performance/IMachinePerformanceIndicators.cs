namespace BigBearPlastics
{
    public interface IMachinePerformanceIndicators : IKeyPerformaceIndicators
    {
        public double AveragePartsPerHour { get; }
        public int TotalPartsProduced { get; set; }
        public MachinePerformanceModel ExtractData() {
            return new MachinePerformanceModel(AverageUptime,AveragePartsPerHour);
        }
    }
}