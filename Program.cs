using BigBearPlastics;
using LineServiceSimulator.Simulation.Output;

namespace LineServiceSimulator
{
    internal class Program
    {
        static void Main(string[] args) {
            RunAllAsync().GetAwaiter().GetResult();
        }

        static Task RunAllAsync() {
            return Task.WhenAll(new[] { RunSimulation() });
        }

        static async Task RunSimulation() {

            ISimulation sim = Factory.CreateSimulation(SimulationClock.EightHours());

            ISimulationResult data = await sim.SimulateAsync();
            Console.WriteLine("Simulation Complete");

            //export results to json
            await data.SaveAsJson(Directory.GetCurrentDirectory());
        }
    }
}