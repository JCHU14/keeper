namespace keeper.Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepRepo _repo;

        public VaultKeepsService(VaultKeepRepo repo)
        {
            _repo = repo;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);

            if (vaultKeep == null)
            {
                throw new Exception("Invalid Id");
            }
            return vaultKeep;
        }

        internal string DeleteVaultKeep(int vaultKeepId, string userId)
        {
            VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
            if (vaultKeep.CreatorId != userId)
            {
                throw new Exception("not yours to delete");
            }
            _repo.DeleteVaultKeep(vaultKeepId);
            return "Deleted VaultKeep";
        }

        internal VaultKeep GetVaultKeepById(int vaultKeepId)
        {
            VaultKeep vaultKeep = _repo.GetVaultKeepsById(vaultKeepId);
            if (vaultKeep == null)
            {
                throw new Exception("Invalid Id");
            }
            return vaultKeep;
        }

        internal List<ProfileVaultKeeps> GetVaultKeepsByVaultId(int vaultId)
        {
            List<ProfileVaultKeeps> vaultKeeps = _repo.GetKeepsByVaultId(vaultId);
            return vaultKeeps;
        }

        internal List<VaultKeepVault> GetVaultKeepsByAccountId(string userId)
        {
            List<VaultKeepVault> vaultKeepVaults = _repo.GetVaultKeepsByAccountId(userId);
            return vaultKeepVaults;
        }


    }
}