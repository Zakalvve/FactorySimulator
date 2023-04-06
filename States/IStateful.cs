namespace BigBearPlastics
{
    //interface for state machines
    public interface IStateful
    {
        public void ChangeState(IState state);
    }
}
