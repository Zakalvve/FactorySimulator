namespace BigBearPlastics
{
    public interface IServicer
    {
        public IServicerPerformanceIndicators Performance { get; }
        //servicer exposes method which accepts a list of commands which will be executed with the given priority
        public bool AcceptRequest(int priority,List<ServiceRequest> requests);
    }
}
