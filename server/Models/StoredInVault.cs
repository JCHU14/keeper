

namespace keeper.Models
{
    public class StoredInVault
    {
        public int Id { get; set; }

        public string AccountId { get; set; }

        public string CreatorId { get; set; }
        public int VaultId { get; set; }
        public int KeepId { get; set; }
    }
}