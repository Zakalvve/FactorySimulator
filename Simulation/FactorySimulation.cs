using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBearPlastics
{
    public class FactorySimulation
    {
        private int _simDuration;
        public FactorySimulation(int duration/*More sim settings could go here (sim settings model?)*/) {
            _simDuration = duration;
        }
        public Task Simulate() {
            return Task.Run(Simulation);
        }
        private List<IReportingSimulatable> InitializeSimulation() {
            List<IReportingSimulatable> sims = Factory.CreateAllMachines().Cast<IReportingSimulatable>().ToList();
            sims.Add(Factory.GetServicer());
            return sims;
        }
        private void Simulation() {

            List<IReportingSimulatable> sims = InitializeSimulation();

            for (int runTime = _simDuration; runTime > 0; runTime--) {
                sims.ForEach(sim => {
                    sim.Tick();
                });
            }

            sims.ForEach(sim => {
                sim.CloseReport(_simDuration);
            });
        }
    }
}
