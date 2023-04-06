namespace BigBearPlastics
{
    public class RunningState : IState
    {
        private IMachine _context;
        private int _timeTillNextPart;

        public RunningState(IMachine context)
        {
            _context = context;
        }
        private void StartNextPart() {
            _timeTillNextPart = _context.SecondsPerPart;
            _context.InputContainer.Take(_context.CurrentJob.Parts.Count);
        }
        public void TransitionTo() {
            _context.LogMessage("Transition to RUNNING");
            StartNextPart();
        }
        //one tick of the simulation
        public void Tick()
        {
            _timeTillNextPart--;
            _context.Uptime++;
            if (_timeTillNextPart <= 0)
            {
                _context.CurrentJob.Parts.ForEach(part => {
                    _context.LogMessage($"Created new part {part.Name}");
                });
                
                //update job
                _context.CurrentJob.CurrentPartsProduced++;
                //place the part into the sillage
                _context.OutputContainer?.Add(_context.CurrentJob.Parts.Count);

                //place the scrap into the scrap bin
                _context.ScrapContainer?.Add(_context.CurrentJob.Parts.Count);

                //check for conditions that would stop the machine from running
                //no more parts, output container is full or scrap bin is full, job complete
                if (_context.CurrentJob.IsComplete) {
                    _context.LogMessage("Job Complete");
                    _context.ChangeState(new ToolChangeState(_context));
                    return;
                }
                if (!_context.CanRun) {
                    //change state to idle
                    _context.ChangeState(new IdleState(_context));
                    return;
                }

                //continue operation
                StartNextPart();
            }
        }
    }
}
