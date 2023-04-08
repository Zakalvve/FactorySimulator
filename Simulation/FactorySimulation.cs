using System.Diagnostics.CodeAnalysis;
using LineServiceSimulator.Simulation.Output;

namespace BigBearPlastics
{
    public class FactorySimulation : ISimulation
    {
        private List<IReportingSimulatable> _sims;
        private int _simDuration;
        private ISimulationAnalyst _analyst;
        private ISimulationResult? _data = null;

        public FactorySimulation(int duration,ISimulationAnalyst analyst,List<IReportingSimulatable> sims) {
            _simDuration = duration;
            _analyst = analyst;
            _sims = sims;
        }
        public Task<ISimulationResult> SimulateAsync() {
            return Task.Run(Simulation);
        }
        public ISimulationResult Simulate() {
            return Simulation();
        }
        private ISimulationResult Simulation() {
            RunSim();
            FinalReport(DataNotNull());
            return _data;
        }

        private void RunSim() {
            for (int runTime = _simDuration; runTime > 0; runTime--) {
                SimulationClock.CurrentTick++;
                _sims.ForEach(sim => {
                    sim.Tick();
                });
                if (runTime % (60 * 5) == 0) {
                    _sims.ForEach(sim => {
                        sim.Record(_analyst);
                    });
                }
            }
            _sims.ForEach(sim => {
                sim.Record(_analyst);
            });
        }

        public void FinalReport([DoesNotReturnIf(false)] bool NotNull) {
            _sims.ForEach(sim => {
                sim.CloseReport(_simDuration);
            });

            _data = _analyst.FinalReport();

            if (DataNotNull())
                Console.WriteLine($"Report finalized {_data}");
            else
                throw new NullReferenceException("No data retrieved");

        }

        [MemberNotNullWhen(true,"_data")]
        public bool DataNotNull() {
            return _data != null;
        }
    }
}
