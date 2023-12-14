



namespace keeper.Repositories
{
    public class VaultKeepRepo
    {

        private readonly IDbConnection _db;

        public VaultKeepRepo(IDbConnection db)
        {
            _db = db;
        }
        private VaultKeep VaultKeepBuilder(VaultKeep vaultKeep, Profile profile)
        {
            vaultKeep.Creator = profile;
            return vaultKeep;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO
            vaultKeeps(vaultId, keepId, creatorId)
            VALUES(@VaultId, @KeepId, @CreatorId);
            
            SELECT
            vaultKeeps.*,
            accounts.*
            FROM vaultKeeps
            JOIN accounts ON accounts.id = vaultKeeps.creatorId
            WHERE vaultKeeps.id = LAST_INSERT_ID();";

            VaultKeep vaultKeep = _db.Query<VaultKeep, Profile, VaultKeep>(sql, VaultKeepBuilder, vaultKeepData).FirstOrDefault();
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




    }
}