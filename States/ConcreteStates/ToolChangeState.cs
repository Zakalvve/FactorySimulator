
namespace BigBearPlastics
{
    public class ToolChangeState : BaseState
    {
        private int durationInSeconds = 15 * 60;
        private int currentDuration = 0;

        public ToolChangeState(IMachine context,IMessageLogger logger) : base(context,logger) { }

        public override void TransitionTo() {
            _logger.LogSignedMessage("Transition to TOOL CHANGE");
            _context.InputContainer = null;
            _context.OutputContainer = null;
            _context.ScrapContainer = null;
            if (!_context.NextJob()) {
                _context.ChangeState(new ShutdownState(_context, _logger));
                return;
            }

            currentDuration = 0;
            //send out requests for new containers - not implemented
            _context.Request(new List<ServiceRequest> { 
                new ServiceRequest(RequestType.REPLACE_INPUT, _context.ReplaceInputContainerCommand(), _context.ID),
                new ServiceRequest(RequestType.REPLACE_OUTPUT, _context.ReplaceOutputContainerCommand(), _context.ID),
                new ServiceRequest(RequestType.REPLACE_SCRAP, _context.ReplaceScrapContainerCommand(), _context.ID)
            });
        }

        public override void Tick() {
            currentDuration++;
            _context.Downtime++;
            if (currentDuration > durationInSeconds) {
                //change complete
                if (_context.CanRun) {
                    _context.ChangeState(new RunningState(_context, _logger));
                    return;
                }
            }
        }

        public override void Record(ISimulationAnalyst analyst) {
            analyst.Visit(this);
        }
    }
}
