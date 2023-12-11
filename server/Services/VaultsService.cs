




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
            Vault vault = GetVaultById(vaultId);

            if (vault.CreatorId != userId)
            {
                throw new Exception("NOT YOURS TO DELETE");
            }

            _repository.DestroyVault(vaultId);

            return $"{vault.Name} has been deleted";
        }








        // *FIXME - Might have to fix

        internal Vault GetVaultById(int vaultId)
        {
            Vault vault = _repository.GetVaultById(vaultId);
            if (vault == null)
            {
                throw new Exception($"Invalid Id: {vaultId}");
            }

            return vault;
        }





        internal List<Vault> GetVaults(string name, string userId)
        {
            List<Vault> vaults = name == null ? _repository.GetVaults() : _repository.GetVaultsWithQuery(name);
            vaults = vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId);
            return vaults;
        }





        internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
        {
            Vault vaultToUpdate = GetVaultById(vaultId);

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
    }
}