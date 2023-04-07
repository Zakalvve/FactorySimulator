using LineServiceSimulator.Machines;

namespace BigBearPlastics
{
    public class RequestOutputContainer : BaseCommand
    {
        public RequestOutputContainer(IServicableMachine requester) : base(requester) { }

        public override void Execute() {
            _requester.ReplaceOutputContainer();
        }
    }
}
