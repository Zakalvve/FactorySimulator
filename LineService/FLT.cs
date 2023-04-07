namespace BigBearPlastics
{
    //SRP: Should accept requests and expose methods to comply with ISimulatable
    //It simulates the accepting and completing of requests from machines and simulates time it takes to perform those actions
    public class FLT : ISimulatableServicer
    {
        public int Uptime { get; set; }

        private IMessageLogger _logger;
        private ServiceRequest? _currentJob = null;
        private PriorityQueue<ServiceRequest,int> _jobs;
        private int _timeSinceStartJob = 0;

        public FLT(PriorityQueue<ServiceRequest,int> jobs, IMessageLogger logger) {
            _jobs = jobs;
            _logger = logger;
            _logger.Prefix = $"FLT:";
        }
        public bool AcceptRequest(int priority,List<ServiceRequest> requests) {
            try {
                //add stratergy to determine calculated priority
                requests.ForEach(request => {
                    _jobs.Enqueue(request,priority);
                    _logger.LogSignedMessage($"Accepted request at priority {priority} from CNC{request.MachineID}");
                });
                _logger.LogSignedMessage($"Requests in queue {_jobs.Count}");
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public void Tick() {
            if (_currentJob != null) {
                _timeSinceStartJob++;
                Uptime++;
                if (_timeSinceStartJob > _currentJob.ResponseTime) {
                    //fullfill the request
                    _logger.LogSignedMessage($"Resolved request \"{_currentJob.Name}\" for CNC{_currentJob.MachineID}. Response time {(int)(_timeSinceStartJob / 60D)} minutes");
                    _currentJob.Command.Execute();
                    _timeSinceStartJob = 0;
                    _currentJob = null;
                }
            }
            else {
                if (_jobs.Count > 0) {
                    _currentJob = _jobs.Dequeue();
                    _logger.LogSignedMessage($"Picking up next job: \"{_currentJob.Name}\". Requested by CNC{_currentJob.MachineID}");
                    _logger.LogSignedMessage($"Requests in queue {_jobs.Count}");
                }
            }
        }

        public void CloseReport(int simDuration) {
            _logger.LogSignedMessage($"Uptime {(int)((Uptime / (double)(simDuration)) * 100)}%");
        }

        public void Record(ISimulationAnalyst analyst) {
            analyst.Visit(this);
        }
    }
}
