namespace BigBearPlastics
{
    public interface IServicableMachine
    {
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
