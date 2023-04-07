namespace BigBearPlastics
{
    public class SimulationAnalyst : ISimulationAnalyst
    {
        private Dictionary<int, Dictionary<string,int>> _machineSimData = new Dictionary<int, Dictionary<string, int>>();

        public void Visit(IMachine machine) {
            _machineSimData[machine.ID].Add("Time",1);
        }

        public void Visit(ISimulatableServicer servicer) {
            throw new NotImplementedException();
        }

        public void Visit(RunningState state) {
            throw new NotImplementedException();
        }

        public void Visit(IdleState state) {
            throw new NotImplementedException();
        }

        public void Visit(ToolChangeState state) {
            throw new NotImplementedException();
        }

        public void Visit(ShutdownState state) {
            throw new NotImplementedException();
        }
    }
}
