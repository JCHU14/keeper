




namespace keeper.Services
{
    public class VaultsService
    {

        private readonly VaultRepo _repository;



        public VaultsService(VaultRepo repository)
        {
            _repository = repository;
        }





        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _repository.CreateVault(vaultData);
            return vault;
        }





        internal string DestroyVault(int vaultId, string userId)
        {
            Vault vault = GetVaultById(vaultId, userId);

            if (vault.CreatorId != userId)
            {
                throw new Exception("NOT YOURS TO DELETE");
            }

            _repository.DestroyVault(vaultId);

            return $"{vault.Name} has been deleted";
        }








        // *FIXME - Might have to fix

        internal Vault GetVaultById(int vaultId, string userId)
        {
            Vault vault = _repository.GetVaultById(vaultId);
            if (vault == null)
            {
                throw new Exception($"Invalid Id: {vaultId}");
            }
            if (vault.IsPrivate == true && vault.CreatorId != userId)
            {
                throw new Exception("good try....");
            }

            return vault;
        }





        internal List<Vault> GetVaults(string name, string userId)
        {
            List<Vault> vaults = name == null ? _repository.GetVaults() : _repository.GetVaultsWithQuery(name);
            vaults = vaults.FindAll(vault => vault.IsPrivate == true || vault.CreatorId == userId);
            return vaults;
        }



        internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
        {
            Vault vaultToUpdate = GetVaultById(vaultId, userId);

            if (vaultToUpdate.CreatorId != userId)
            {
                throw new Exception("NOT YOURS TO EDIT");
            }

            vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
            vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
            vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
            vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

            Vault vault = _repository.UpdateVault(vaultToUpdate);
            return vault;


        }
        internal List<Vault> GetVaultsByProfileId(string profileId)
        {
            List<Vault> vaults = _repository.GetVaultsByProfileId(profileId);
            return vaults;
        }

        internal List<Vault> GetVaultsByAccountId(string userId)
        {
            List<Vault> vaults = _repository.GetVaultsByAccountId(userId);
            return vaults;
        }
    }
}