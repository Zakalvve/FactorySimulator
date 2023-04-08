namespace BigBearPlastics
{
    public class IdleState : BaseState
    {
        public IdleState(IMachine context,IMessageLogger logger) : base(context,logger) { }

        public override void TransitionTo() {
            _logger.LogSignedMessage("Transition to IDLE");
            //make service requests
            if (_context.InputContainer.IsEmpty) {
                _logger.LogSignedMessage("Input stillage empty.");
                _context.Request(new List<ServiceRequest> { new ServiceRequest(RequestType.REPLACE_INPUT,_context.ReplaceInputContainerCommand(),_context.ID) });
            }
            if (_context.OutputContainer.IsFull) {
                _logger.LogSignedMessage("Output stillage full.");
                _context.Request(new List<ServiceRequest> { new ServiceRequest(RequestType.REPLACE_OUTPUT,_context.ReplaceOutputContainerCommand(),_context.ID) });
            }
            if (_context.ScrapContainer.IsFull) {
                _logger.LogSignedMessage("Scrap bin full.");
                _context.Request(new List<ServiceRequest> { new ServiceRequest(RequestType.REPLACE_SCRAP,_context.ReplaceScrapContainerCommand(),_context.ID) });
            }
        }

        public override void Tick() {
            //check for changes to macine state
            if (_context.CanRun) {
                _context.ChangeState(new RunningState(_context,_logger));
                return;
            }
        }

        public override void Record(ISimulationAnalyst analyst) {
            analyst.ExtractTickRecord(this);
        }
    }
}
