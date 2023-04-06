namespace BigBearPlastics
{
    //interface that concrete states must implement
    public interface IState
    {
        //can be called to initialise a state
        public void TransitionTo();
        //represents a single simulation tick
        public void Tick();
    }
}
