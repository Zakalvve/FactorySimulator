using LineServiceSimulator.Machines;

namespace BigBearPlastics
{
    //
    public interface IMachine : IStateful, IServicableMachine
    {
        public int ID { get; set; }
        public int Priority { get; set; }
        Queue<IJobModel> Jobs { get; set; }
        IJobModel CurrentJob { get; set; }
        public bool CanRun { get; }
        public bool NextJob();

        //TODO: Decide if Uptime/Downtime properties belong here or if they should instead be implemented in an IKeyPerformanceIndicators interface
        public int Uptime { get; set; }
        public int Downtime { get; set; }

        //TODO: Decide if these containers are related to the machine itself or to the fact that the machine is servicable.
        //      After all to be servicable the machine must have these containers so does it make sense for them to be here?
        IContainer? InputContainer { get; set; }
        IContainer? OutputContainer { get; set; }
        IContainer? ScrapContainer { get; set; }
    }
}