namespace BigBearPlastics
{
    public class ShutdownState : IState
    {
        private IMachine _context;
        public ShutdownState(IMachine context) {
            _context = context;
        }

        public void Tick() {
            _context.Downtime++;
        }

        public void TransitionTo() {
            _context.LogMessage("SHUTDOWN");
        }
    }
}
