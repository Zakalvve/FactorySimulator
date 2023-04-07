using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBearPlastics
{
    public class FactorySimulation
    {
        private List<IReportingSimulatable> _sims;
        private int _simDuration;
        public FactorySimulation(int duration/*More sim settings could go here (sim settings model?)*/) {
            _simDuration = duration;
        }
        public Task SimulateAsync() {
            return Task.Run(Simulation);
        }
        public void Simulate() {
            Simulation();
        }
        private void Simulation() {

            InitializeSimulation();

            RunSim();

            CloseReport();
        }
        private void InitializeSimulation() {
            _sims = Factory.CreateAllMachines().Cast<IReportingSimulatable>().ToList();
            _sims.Add(Factory.GetServicer());
        }

        public void RunSim() {
            for (int runTime = _simDuration; runTime > 0; runTime--) {
                _sims.ForEach(sim => {
                    sim.Tick();
                });
            }
        }
        public void CloseReport() {
            _sims.ForEach(sim => {
                sim.CloseReport(_simDuration);
            });
        }
    }
}
