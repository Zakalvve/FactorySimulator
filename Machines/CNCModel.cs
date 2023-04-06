namespace BigBearPlastics
{
    public class CNCModel : ISimulatableMachine {
        public CNCModel(int id, int priority, Queue<IJobModel> jobs,IJobModel curJob, IServicer servicer, IContainer? input = null, IContainer? output = null, IContainer? scrap = null) {
            ID = id;
            Priority = priority;
            Jobs = jobs;
            CurrentJob = curJob;
            InputContainer = input;
            OutputContainer = output;
            ScrapContainer = scrap;
            Servicer = servicer;
            _currentState = new IdleState(this);
            _currentState.TransitionTo();
        }

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

        public int SecondsPerPart {
            get {
                if (CurrentJob == null) return -1;
                return (int)(1 / (CurrentJob.PartsPerHour / (60 * 60)));
            }
        }

        public void Tick() {
            _currentState.Tick();
        }

        //used to record data
        public void Record() {
            throw new NotImplementedException();
        }

        public void ChangeState(IState state) {
            _currentState = state;
            _currentState.TransitionTo();
        }

        public void LogMessage(string message) {
            Console.WriteLine($"CNC{ID}: {message}");
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
    }
}