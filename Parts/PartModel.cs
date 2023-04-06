namespace BigBearPlastics
{
    public class PartModel : IPartModel
    {
        public PartModel(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
