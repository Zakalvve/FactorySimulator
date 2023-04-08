using System.Diagnostics.CodeAnalysis;

namespace BigBearPlastics
{
    public class CNCModel : ISimulatableMachine
    {
        public CNCModel(int id,int priority,Queue<IJobModel> jobs,IJobModel curJob,IServicer servicer,IMessageLogger logger, IMachinePerformanceIndicators performance, IContainer? input = null,IContainer? output = null,IContainer? scrap = null) {
            ID = id;
            Priority = priority;
            Jobs = jobs;
            CurrentJob = curJob;
            InputContainer = input;
            OutputContainer = output;
            ScrapContainer = scrap;
            Servicer = servicer;
            Performance = performance;
            _logger = logger;
            _logger.Prefix = $"CNC{ID}:";
            _currentState = new RunningState(this,_logger);
            _currentState.TransitionTo();
        }

        public int ID { get; }
        public int Priority { get; }
        public Queue<IJobModel> Jobs { get; }
        public IJobModel CurrentJob { get; set; }
        public bool CanRun { get { return AllContainersValid; } }
        public IMachinePerformanceIndicators Performance { get; }

        private IMessageLogger _logger;
        private IState _currentState;

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
        public void CloseReport(int simDuration) {
            _logger.LogSignedMessage($"Av. Uptime {Performance.AverageUptime}%. Av. PPH {Performance.AveragePartsPerHour}");
        }
        public void Record(ISimulationAnalyst analyst) {
            analyst.ExtractTickRecord(this);
        }


        [MemberNotNullWhen(true,new[] { "InputContainer","OutputContainer","ScrapContainer" })]
        private bool AllContainersAvailable { get { return InputContainer != null && OutputContainer != null && ScrapContainer != null; } }
        private bool AllContainersValid { get { 
                if (AllContainersAvailable)
                    return !InputContainer.IsEmpty && !OutputContainer.IsFull && !ScrapContainer.IsFull;
                return false;
        } }
        public IContainer? InputContainer { get; set; }
        public IContainer? OutputContainer { get; set; }
        public IContainer? ScrapContainer { get; set; }

        public IServicer Servicer { get; set; }
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