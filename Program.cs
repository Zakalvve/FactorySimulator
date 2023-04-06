using BigBearPlastics;

namespace LineServiceSimulator
{
    internal class Program
    {
        static void Main(string[] args) {

            List<ISimulatableMachine> machines = Factory.CreateAllMachines();
            IServicer servicer = Factory.GetServicer();

            for (int runTime = EightHours(); runTime > 0; runTime--) {
                machines.ForEach(machine => {
                    machine.Tick();
                });
                servicer.Tick();
            }

            machines.ForEach(machine => {
                machine.LogMessage($"Uptime {(int)((machine.Uptime/(double)EightHours())*100)}%");
            });

            Console.WriteLine($"FLT: Uptime {(int)((servicer.Uptime/(double)EightHours())*100)}%");
        }

        static int EightHours() {
            return 8 * 60 * 60;
        }
    }
}