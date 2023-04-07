namespace BigBearPlastics
{
    public class ShutdownState : BaseState
    {
        public ShutdownState(IMachine context,IMessageLogger logger) : base(context,logger) { }

        public override void Record(ISimulationAnalyst analyst) {
            analyst.Visit(this);
        }

        public override void Tick() {
            _context.Downtime++;
        }

        public override void TransitionTo() {
           _logger.LogSignedMessage("SHUTDOWN");
        }
    }
}
