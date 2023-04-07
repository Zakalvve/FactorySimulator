namespace BigBearPlastics
{
    public class JobModel : IJobModel {
        public JobModel(List<IPartModel> parts,int partsRequired,int ppInputContainer,int ppOutputContainer,int spScrapContainer,double pph) {
            Parts = parts;
            NumberOfPartsRequired = partsRequired;
            PartsPerInputContainer = ppInputContainer;
            PartsPerOutputContainer = ppOutputContainer;
            ScrapPerScrapContainer = spScrapContainer;
            PartsPerHour = pph;
            CurrentPartsProduced = 0;
        }
        public List<IPartModel> Parts { get; set; }
        public int NumberOfPartsRequired { get; set; }
        public int CurrentPartsProduced { get; set; }
        public int PartsPerInputContainer { get; set; }
        public int PartsPerOutputContainer { get; set; }
        public int ScrapPerScrapContainer { get; set; }
        public double PartsPerHour { get; set; }
        public bool IsComplete { get { return CurrentPartsProduced >= NumberOfPartsRequired; } }
        public int SecondsPerPart { get { return (int)(1 / (PartsPerHour / (60 * 60))); } }
    }
}
