namespace BigBearPlastics
{
    public interface IReportingSimulatable : ISimulatable
    {
        public void CloseReport(int simDuration);
    }
}
