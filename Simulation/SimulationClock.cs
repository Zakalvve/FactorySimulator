namespace BigBearPlastics
{
    public class SimulationClock : ISimulationClock
    {
        public static int CurrentTick { get; set; }
        public static int EightHours() {
            return 8 * 60 * 60;
        }

        public double ToMinutes(int time) {
            return (double)time / 60;
        }
        public double ToHours(int time) {
            return ToMinutes(time) / 60;
        }

        public string Time {
            get {
                return $"{CurrentHour()}:{CurrentMinutes()}";
            }
        }

        public int ElapsedTime { get => CurrentTick; }
        public double ElapsedTimeInMinutes { get { return ToMinutes(ElapsedTime); } }
        public double ElapsedTimeInHours { get { return ToHours(ElapsedTime); } }

        private int CurrentHour() {
            return CurrentTick / (60 * 60);
        }
        private int CurrentMinutes() {
            return (CurrentTick % (60 * 60)) / 60;
        }
    }
}
