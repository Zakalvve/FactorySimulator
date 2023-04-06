namespace BigBearPlastics
{
    public interface IContainer
    {
        public int MaximumFillValue { get; set; }
        public int CurrentFillValue { get; set; }
        public bool IsEmpty { get; }
        public bool IsFull { get; }
        public void Add(int amount);
        public void Take(int amount);
        public void Reset();
    }
}
