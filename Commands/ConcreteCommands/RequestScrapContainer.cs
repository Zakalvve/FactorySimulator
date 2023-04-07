using LineServiceSimulator.Machines;

namespace BigBearPlastics
{
    public class RequestScrapContainer : BaseCommand
    {
        public RequestScrapContainer(IServicableMachine requester) : base(requester) { }

        public override void Execute() {
            _requester.ReplaceScrapContainer();
        }
    }
}
