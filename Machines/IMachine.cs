namespace BigBearPlastics
{
    //
    public interface IMachine : IStateful, IServicableMachine
    {
        public int ID { get; }
        public int Priority { get; }
        Queue<IJobModel> Jobs { get; }
        IJobModel CurrentJob { get; set; }
        public bool CanRun { get; }
        public bool NextJob();
        public IMachinePerformanceIndicators Performance { get; }

        //TODO: Decide if these containers are related to the machine itself or to the fact that the machine is servicable.
        //      After all to be servicable the machine must have these containers so does it make sense for them to be here?
        IContainer? InputContainer { get; set; }
        IContainer? OutputContainer { get; set; }
        IContainer? ScrapContainer { get; set; }
    }
}