namespace BigBearPlastics
{
    public abstract class BaseState : IState
    {
        protected IMachine _context;
        protected IMessageLogger _logger;
        public BaseState(IMachine context,IMessageLogger logger) {
            _context = context;
            _logger = logger;
        }

        public abstract void Record(ISimulationAnalyst analyst);

        public abstract void Tick();

        public abstract void TransitionTo();
    }
}
