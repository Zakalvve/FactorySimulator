namespace BigBearPlastics
{
    public class CNCModel : ISimulatableMachine {
        public CNCModel(int id, int priority, Queue<IJobModel> jobs,IJobModel curJob, IServicer servicer, IMessageLogger logger, IContainer? input = null, IContainer? output = null, IContainer? scrap = null) {
            ID = id;
            Priority = priority;
            Jobs = jobs;
            CurrentJob = curJob;
            InputContainer = input;
            OutputContainer = output;
            ScrapContainer = scrap;
            Servicer = servicer;
            _logger = logger;
            _logger.Prefix = $"CNC{ID}:";
            _currentState = new IdleState(this, _logger);
            _currentState.TransitionTo();
        }

        private IMessageLogger _logger;
        public int Uptime { get; set; } = 0;
        public int Downtime { get; set; } = 0;
        public int ID { get; set; }
        public int Priority { get; set; }
        public IJobModel CurrentJob { get; set; }
        public Queue<IJobModel> Jobs { get; set; }
        public IContainer? InputContainer { get; set; }
        public IContainer? OutputContainer { get; set; }
        public IContainer? ScrapContainer { get; set; }

        private IState _currentState;
        public bool CanRun { get { return AllContainersAvailable && AllContainersValid; } }
        private bool AllContainersAvailable { get { return InputContainer != null && OutputContainer != null && ScrapContainer != null; } }
        private bool AllContainersValid { get { return !InputContainer.IsEmpty && !OutputContainer.IsFull && !ScrapContainer.IsFull; } }
        public IServicer Servicer { get; set; }

        public void Tick() {
            _currentState.Tick();
        }

        public void ChangeState(IState state) {
            _currentState = state;
            _currentState.TransitionTo();
        }

        public bool NextJob() {
            if (Jobs.Count > 0) { 
                CurrentJob = Jobs.Dequeue();
                return true;
            }
            return false;
        }

        public void ReplaceInputContainer() {
            InputContainer = Factory.CreateContainerInverted(CurrentJob.PartsPerInputContainer);
        }

        public void ReplaceOutputContainer() {
            OutputContainer = Factory.CreateContainer(CurrentJob.PartsPerOutputContainer);
        }

        public void ReplaceScrapContainer() {
            ScrapContainer = Factory.CreateContainer(CurrentJob.ScrapPerScrapContainer);
        }

        public ICommand ReplaceInputContainerCommand() {
            return new RequestInputContainer(this);
        }

        public ICommand ReplaceOutputContainerCommand() {
            return new RequestOutputContainer(this);
        }

        public ICommand ReplaceScrapContainerCommand() {
            return new RequestScrapContainer(this);
        }

        public void Request(List<ServiceRequest> requests) {
            Servicer.AcceptRequest(Priority,requests);
        }

        public void CloseReport(int simDuration) {
            _logger.LogSignedMessage($"Uptime {(int)((Uptime / (double)(simDuration)) * 100)}%");
        }

        public void Record(ISimulationAnalyst analyst) {
            analyst.Visit(this);
        }
    }
}