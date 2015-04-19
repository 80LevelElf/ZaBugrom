using Models.Data.Enums;

namespace Models.Data
{
    public class TagData : BigData
    {
        public int CountOfUsage { get; set; }
        public string Name { get; set; }
        public TagType Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, CountOfUsage);
        }
    }
}