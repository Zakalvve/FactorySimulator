namespace BigBearPlastics
{
    public interface IJobModel
    {
        public List<IPartModel> Parts { get; set; }
        public int NumberOfPartsRequired { get; set; }
        public int CurrentPartsProduced { get; set; }
        public int PartsPerInputContainer { get; set; }
        public int PartsPerOutputContainer { get; set; }
        public int ScrapPerScrapContainer { get; set; }
        public double PartsPerHour { get; set; }
        public bool IsComplete { get; }
        public int SecondsPerPart { get; }
    }
}