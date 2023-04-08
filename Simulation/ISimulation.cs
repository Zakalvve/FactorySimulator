using LineServiceSimulator.Simulation.Output;

namespace BigBearPlastics
{
    public interface ISimulation
    {
        //the simulation throws an error if NotNull is false
        void FinalReport(bool NotNull);
        ISimulationResult Simulate();
        Task<ISimulationResult> SimulateAsync();
    }
}