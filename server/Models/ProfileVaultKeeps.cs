using Microsoft.Net.Http.Headers;

namespace keeper.Models
{
    public class ProfileVaultKeeps : Keep
    {
        public int VaultKeepId { get; set; }
        public int VaultId { get; set; }
        public Profile Creator { get; set; }
    }
}