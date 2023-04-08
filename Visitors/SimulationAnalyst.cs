using LineServiceSimulator.Simulation.Output;

namespace BigBearPlastics
{
    public class SimulationAnalyst : ISimulationAnalyst
    {
        private ISimulationResult _simulationResult;
        public SimulationAnalyst(ISimulationResult simResult) {
            _simulationResult = simResult;
        }
        private SimulationClock clock = new SimulationClock();

        public ISimulationResult FinalReport() {
            return _simulationResult;
        }

        public void ExtractTickRecord(IMachine machine) {
            _simulationResult.AddMachineData(machine.ID,clock.Time,machine.Performance.ExtractData());
        }

        public void ExtractTickRecord(ISimulatableServicer servicer) {
            _simulationResult.AddServicerData(clock.Time,servicer.Performance.ExtractData());
        }

        public void ExtractTickRecord(RunningState state) {
            //throw new NotImplementedException();
        }

        public void ExtractTickRecord(IdleState state) {
            //throw new NotImplementedException();
        }

        public void ExtractTickRecord(ToolChangeState state) {
            //throw new NotImplementedException();
        }

        public void ExtractTickRecord(ShutdownState state) {
            //throw new NotImplementedException();
        }
    }
}
