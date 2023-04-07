namespace BigBearPlastics
{
    public interface ISimulatable
    {
        public void Tick();
        public void Record(ISimulationAnalyst analyst);
    }
}
