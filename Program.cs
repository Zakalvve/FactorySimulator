using BigBearPlastics;

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
            FactorySimulation sim = new FactorySimulation(EightHours());

            await sim.Simulate();

            //use results
            Console.WriteLine("Simulation Complete");
        }

        static int EightHours() {
            return 8 * 60 * 60;
        }
    }
}