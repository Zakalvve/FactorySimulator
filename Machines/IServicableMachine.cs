using BigBearPlastics;

namespace LineServiceSimulator.Machines
{
    public interface IServicableMachine
    {
        //TODO: Tight coupling of machine to servicer. These classes shouldn't need to know about one another and the request could be raised as an event instead.
        public IServicer Servicer { get; set; }
        public void Request(List<ServiceRequest> requests);
        public ICommand ReplaceInputContainerCommand();
        public ICommand ReplaceOutputContainerCommand();
        public ICommand ReplaceScrapContainerCommand();
        public void ReplaceInputContainer();
        public void ReplaceOutputContainer();
        public void ReplaceScrapContainer();
    }
}
