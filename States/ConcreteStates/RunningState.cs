namespace BigBearPlastics
{
    public class RunningState : BaseState
    {
        private int _timeTillNextPart;

        public RunningState(IMachine context,IMessageLogger logger) : base(context,logger) { }

        private void StartNextPart() {
            _timeTillNextPart = _context.CurrentJob.SecondsPerPart;
            _context.InputContainer.Take(_context.CurrentJob.Parts.Count);
        }
        public override void TransitionTo() {
            _logger.LogSignedMessage("Transition to RUNNING");
            StartNextPart();
        }
        //one tick of the simulation
        public override void Tick() {
            _timeTillNextPart--;
            _context.Performance.Uptime++;
            if (_timeTillNextPart <= 0) {
                _context.CurrentJob.Parts.ForEach(part => {
                    _logger.LogSignedMessage($"Created new part {part.Name}");
                });

                _context.Performance.TotalPartsProduced++;
                //update job
                _context.CurrentJob.CurrentPartsProduced++;
                //place the part into the sillage
                _context.OutputContainer?.Add(_context.CurrentJob.Parts.Count);

                //place the scrap into the scrap bin
                _context.ScrapContainer?.Add(_context.CurrentJob.Parts.Count);

                //check for conditions that would stop the machine from running
                //no more parts, output container is full or scrap bin is full, job complete
                if (_context.CurrentJob.IsComplete) {
                    _logger.LogSignedMessage("Job Complete");
                    _context.ChangeState(new ToolChangeState(_context,_logger));
                    return;
                }
                if (!_context.CanRun) {
                    //change state to idle
                    _context.ChangeState(new IdleState(_context,_logger));
                    return;
                }

                //continue operation
                StartNextPart();
            }
        }

        public override void Record(ISimulationAnalyst analyst) {
            analyst.ExtractTickRecord(this);
        }
    }
}
