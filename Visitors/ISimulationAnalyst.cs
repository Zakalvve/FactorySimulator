using LineServiceSimulator.Simulation.Output;

namespace BigBearPlastics
{
    public interface ISimulationAnalyst
    {

        public void ExtractTickRecord(IMachine machine);
        public void ExtractTickRecord(ISimulatableServicer servicer);
        public void ExtractTickRecord(RunningState state);
        public void ExtractTickRecord(IdleState state);
        public void ExtractTickRecord(ToolChangeState state);
        public void ExtractTickRecord(ShutdownState state);

        public ISimulationResult FinalReport();
    }
}
