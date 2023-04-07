using LineServiceSimulator.Machines;

namespace BigBearPlastics
{
    public abstract class BaseCommand : ICommand
    {
        protected IServicableMachine _requester;

        public BaseCommand(IServicableMachine requester) {
            _requester = requester;
        }
        public virtual void Execute() { }
    }
}
