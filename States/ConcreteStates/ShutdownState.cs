namespace BigBearPlastics
{
    public class ShutdownState : BaseState
    {
        public ShutdownState(IMachine context,IMessageLogger logger) : base(context,logger) { }

        public override void Record(ISimulationAnalyst analyst) {
            analyst.ExtractTickRecord(this);
        }

        public override void Tick() { }

        public override void TransitionTo() {
            _logger.LogSignedMessage("SHUTDOWN");
        }
    }
}
