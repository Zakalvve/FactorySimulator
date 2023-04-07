namespace BigBearPlastics
{
    public interface ISimulationAnalyst
    {

        public void Visit(IMachine machine);
        public void Visit(ISimulatableServicer servicer);
        public void Visit(RunningState state);
        public void Visit(IdleState state);
        public void Visit(ToolChangeState state);
        public void Visit(ShutdownState state);
    }
}
