namespace BigBearPlastics
{
    public class MachinePerformanceModel
    {
        public MachinePerformanceModel(double avUptime, double avPph) {
            AverageUptime = avUptime;
            AveragePartsPerHour = avPph;
        }
        public double AverageUptime { get; set; }
        public double AveragePartsPerHour { get; set; }
    }
}
