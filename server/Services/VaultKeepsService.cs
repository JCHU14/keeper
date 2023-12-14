namespace keeper.Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepRepo _repo;
        private readonly VaultKeepRepo _vaultRepo;

        private readonly VaultsService _vaultsService;

        private readonly KeepService _keepService;

        public VaultKeepsService(VaultKeepRepo repo, VaultKeepRepo vaultRepo, VaultsService vaultsService, KeepService keepService)
        {
            _repo = repo;
            _vaultRepo = vaultRepo;
            _vaultsService = vaultsService;
            _keepService = keepService;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {

            Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);

            VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);

            if (vault.CreatorId != vaultKeepData.CreatorId)
            {
                throw new Exception("not your vault to alter");
            }

            Keep keep = _keepService.GetKeepById(vaultKeepData.KeepId);
            keep.Kept++;
            _keepService.UpdateViewCount(keep);
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
            return $"Deleted VaultKeep {vaultKeepId}";
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


        internal List<ProfileVaultKeeps> GetVaultKeepsByVaultId(int vaultId, string userId)
        {
            _vaultsService.GetVaultById(vaultId, userId);

            List<ProfileVaultKeeps> vaultKeeps = _repo.GetKeepsByVaultId(vaultId);
            return vaultKeeps;
        }




    }
}