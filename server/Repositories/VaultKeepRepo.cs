



namespace keeper.Repositories
{
    public class VaultKeepRepo
    {

        private readonly IDbConnection _db;

        public VaultKeepRepo(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO
            vaultKeeps(vaultId, keepId, creatorId)
            VALUES(@VaultId, @KeepId, @CreatorId);
            
            SELECT * FROM vaultKeeps WHERE id = LAST_INSERT_ID();";

            VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
            return vaultKeep;
        }

        internal void DeleteVaultKeep(int vaultKeepId)
        {
            string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";
            _db.Execute(sql, new { vaultKeepId });
        }

        internal VaultKeep GetVaultKeepsById(int vaultKeepId)
        {
            string sql = "SELECT * FROM vaultKeeps WHERE id = @vaultKeepId;";
            VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
            return vaultKeep;
        }

        internal List<ProfileVaultKeeps> GetKeepsByVaultId(int vaultId)
        {
            string sql = @"
            SELECT
            vaultKeeps.*,
            keeps.*,
            accounts.*
            FROM vaultKeeps
            JOIN keeps ON keeps.id = vaultKeeps.keepId
            JOIN accounts ON accounts.id = keeps.creatorId
            WHERE vaultKeeps.vaultId = @vaultId;";

            List<ProfileVaultKeeps> vaultKeeps = _db.Query<VaultKeep, ProfileVaultKeeps, Profile, ProfileVaultKeeps>(sql, (vaultKeep, profileVaultKeep, profile) =>
            {
                profileVaultKeep.VaultKeepId = vaultKeep.Id;
                profileVaultKeep.VaultId = vaultKeep.VaultId;
                profileVaultKeep.Creator = profile;
                return profileVaultKeep;
            }, new { vaultId }).ToList();
            return vaultKeeps;
        }

        internal List<VaultKeepVault> GetVaultKeepsByAccountId(string userId)
        {
            string sql = @"
            SELECT
            vaultKeep.*,
            vault.*,
            account.*
            FROM vaultKeeps
            JOIN vaults ON vaultKeep.vaultId = vault.id
            JOIN accounts ON account.id = vault.creatorId
            WHERE vaultKeeps.accountId = @userId;";

            List<VaultKeepVault> vaultKeepVaults = _db.Query<VaultKeep, VaultKeepVault, Profile, VaultKeepVault>(sql, (vaultKeep, vaultKeepVault, profile) =>
            {
                vaultKeepVault.VaultKeepId = vaultKeep.Id;
                vaultKeepVault.CreatorId = vaultKeep.CreatorId;
                vaultKeepVault.Creator = profile;
                return vaultKeepVault;
            }, new { userId }).ToList();
            return vaultKeepVaults;
        }


    }
}