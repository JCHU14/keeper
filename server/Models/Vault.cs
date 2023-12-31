
namespace keeper.Models
{
    public class Vault
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProfileId { get; set; }

        public string AccountId { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int KeepCount { get; set; }
        public bool? IsPrivate { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
}