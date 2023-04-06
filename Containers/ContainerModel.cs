namespace BigBearPlastics
{
    internal class ContainerModel : IContainer
    {
        public ContainerModel(int maxFill): this(maxFill, 0) { }

        public ContainerModel(int maxFill, int curFill) {
            MaximumFillValue = maxFill;
            CurrentFillValue = curFill;
        }

        public int MaximumFillValue { get; set; }
        public int CurrentFillValue { get; set; }
        public bool IsEmpty { get { return CurrentFillValue <= 0; } }
        public bool IsFull { get { return CurrentFillValue >= MaximumFillValue; } }
        public void Add(int amount) {
            CurrentFillValue += amount;
        }
        public void Take(int amount) {
            CurrentFillValue = +amount;
        }
        public void Reset() {
            CurrentFillValue = 0;
        }
    }
}
