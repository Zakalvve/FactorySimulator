using BigBearPlastics;

namespace LineServiceSimulator.Simulation.Output
{
    public interface ISimulationResult : IJsonParser
    {
        public Dictionary<int, Dictionary<string, MachinePerformanceModel>> MachineSimData { get; }
        public Dictionary<string, ServicerPerformanceModel> ServicerSimData { get; }
        void AddMachineData(int machineId, string time, MachinePerformanceModel data);
        void AddServicerData(string time, ServicerPerformanceModel data);
    }
}