namespace BigBearPlastics
{
    public class FLT : IServicer
    {
        private ServiceRequest? _currentJob;
        private int _timeSinceStartJob = 0;

        private PriorityQueue<ServiceRequest,int> _jobs;
        public FLT(PriorityQueue<ServiceRequest,int> jobs) {
            _jobs = jobs;
        }
        public bool AcceptRequest(int priority,List<ServiceRequest> requests) {
            try {
                requests.ForEach(request => {
                    _jobs.Enqueue(request,priority);
                    LogMessage($"Accepted request at priority {priority} from CNC{request.MachineID}");
                });
                LogMessage($"Requests in queue {_jobs.Count}");
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public int Uptime { get; set; }

        public void Record() {
            throw new NotImplementedException();
        }

        public void Tick() {
            if (_currentJob != null) {
                _timeSinceStartJob++;
                Uptime++;
                if (_timeSinceStartJob > _currentJob.ResponseTime) {
                    //fullfill the request
                    LogMessage($"Resolved request \"{_currentJob.Name}\" for CNC{_currentJob.MachineID}. Response time {(int)(_timeSinceStartJob / 60D)} minutes");
                    _currentJob.Command.Execute();
                    _timeSinceStartJob = 0;
                    _currentJob = null;
                }
            }
            else {
                if (_jobs.Count > 0) {
                    _currentJob = _jobs.Dequeue();
                    LogMessage($"Picking up next job: \"{_currentJob.Name}\". Requested by CNC{_currentJob.MachineID}");
                    LogMessage($"Requests in queue {_jobs.Count}");
                }
            }
        }

        public void LogMessage(string message) {
            Console.WriteLine($"FLT: {message}");
        }
    }
}
