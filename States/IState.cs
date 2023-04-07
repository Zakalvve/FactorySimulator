namespace BigBearPlastics
{
    //interface that concrete states must implement
    public interface IState : ISimulatable
    {
        //can be called to initialise a state
        public void TransitionTo();
    }
}
