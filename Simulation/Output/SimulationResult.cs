using BigBearPlastics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LineServiceSimulator.Simulation.Output
{
    public class SimulationResult : ISimulationResult
    {
        public SimulationResult()
        {
            MachineSimData = new Dictionary<int, Dictionary<string, MachinePerformanceModel>>();
            ServicerSimData = new Dictionary<string, ServicerPerformanceModel>();
        }
        public Dictionary<int, Dictionary<string, MachinePerformanceModel>> MachineSimData { get; private set; }
        public Dictionary<string, ServicerPerformanceModel> ServicerSimData { get; private set; }

        public void AddMachineData(int machineId, string time, MachinePerformanceModel data)
        {
            if (!MachineSimData.ContainsKey(machineId))
                MachineSimData.Add(machineId, new Dictionary<string, MachinePerformanceModel>());
            MachineSimData[machineId].Add(time, data);
        }

        public void AddServicerData(string time, ServicerPerformanceModel data)
        {
            ServicerSimData.Add(time, data);
        }

        public async Task SaveAsJson(string path) {
            await using FileStream machineStream = File.Create($"{path}/machines.json");
            await using FileStream servicerStream = File.Create($"{path}/servicers.json");

            await JsonSerializer.SerializeAsync(machineStream,MachineSimData);
            await JsonSerializer.SerializeAsync(servicerStream,ServicerSimData);
        }
    }
}
