namespace BigBearPlastics
{
    public class IdleState: IState
    {
        private IMachine _context;
        public IdleState(IMachine context) {
            _context = context;
        }

        public void TransitionTo() {
            _context.LogMessage("Transition to IDLE");
            //make service requests
            if (_context.InputContainer.IsEmpty) {
                _context.LogMessage("Input stillage empty.");
                _context.Request(new List<ServiceRequest> { new ServiceRequest(RequestType.REPLACE_INPUT, _context.ReplaceInputContainerCommand(), _context.ID) });
            }
            if (_context.OutputContainer.IsFull) {
                _context.LogMessage("Output stillage full.");
                _context.Request(new List<ServiceRequest> { new ServiceRequest(RequestType.REPLACE_OUTPUT,_context.ReplaceOutputContainerCommand(), _context.ID) });
            }
            if (_context.ScrapContainer.IsFull) {
                _context.LogMessage("Scrap bin full.");
                _context.Request(new List<ServiceRequest> { new ServiceRequest(RequestType.REPLACE_SCRAP,_context.ReplaceScrapContainerCommand(),_context.ID ) });
            }
        }

        public void Tick() {
            _context.Downtime++;
            //check for changes to macine state
            if (_context.CanRun) {
                _context.ChangeState(new RunningState(_context));
                return;
            }
        }
    }
}
