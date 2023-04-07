using LineServiceSimulator.Machines;

namespace BigBearPlastics
{
    internal class RequestInputContainer : BaseCommand
    {
        public RequestInputContainer(IServicableMachine requester) : base(requester) { }

        public override void Execute() {
            _requester.ReplaceInputContainer();
        }
    }
}
