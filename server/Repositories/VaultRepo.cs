






namespace keeper.Repositories
{
    public class VaultRepo
    {

        private readonly IDbConnection _db;

        public VaultRepo(IDbConnection db)
        {
            _db = db;
        }

        private Vault VaultBuilder(Vault vault, Profile profile)
        {
            vault.Creator = profile;
            return vault;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            string sql = @"
            INSERT INTO
            vaults (name, description, img, creatorId, isPrivate)
            VALUES (@Name, @Description, @Img, @CreatorId, @IsPrivate);
            
            SELECT
            vault.*,
            acc.*
            FROM vaults vault
            JOIN accounts acc ON acc.id = vault.creatorId
            WHERE vault.id = LAST_INSERT_ID();";

            Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
            return vault;
        }



        internal void DestroyVault(int vaultId)
        {
            string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
            _db.Execute(sql, new { vaultId });
        }

        internal Vault GetVaultById(int vaultId)
        {
            string sql = @"
            SELECT
            vault.*,
            acc.*
            FROM vaults vault
            JOIN accounts acc ON acc.id = vault.creatorId
            WHERE vault.id = @vaultId ;";

            Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { vaultId }).FirstOrDefault();
            return vault;
        }

        internal List<Vault> GetVaults()
        {
            string sql = @"
            SELECT
            vault.*,
            acc.*
            FROM vaults vault
            JOIN accounts acc ON acc.id = vault.creatorId
            ;";

            List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder).ToList();
            return vaults;
        }

        internal List<Vault> GetVaultsWithQuery(string name)
        {
            name = $"%{name}";

            string sql = @"
            SELECT
            vault.*,
            COUNT(keep.id) AS keepCount
            acc.*
            FROM vaults vault
            JOIN accounts acc On acc.id = vault.creatorId
            LEFT JOIN keeps keep ON keep.vaultId = vault.id
            GROUP BY (vault.id)
            WHERE vault.name LIKE @name;";

            List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { name }).ToList();
            return vaults;
        }

        internal Vault UpdateVault(Vault vaultData)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            isPrivate = @IsPrivate
            WHERE id = @Id LIMIt 1;
            
            SELECT vault.*,
            acc.*
            FROM vaults vault
            JOIN accounts acc ON acc.id = vault.creatorId
            WHERE vault.id = @Id;";

            Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
            return vault;
        }

        internal List<Vault> GetVaultsByProfileId(string profileId)
        {
            string sql = @"
            SELECT
            vaults.*,
            accounts.*
            FROM vaults
            JOIN accounts ON accounts.id = vaults.creatorId
            WHERE vaults.creatorId = @profileId
            ;";

            List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { profileId }).ToList();
            return vaults;
        }

        internal List<Vault> GetVaultsByAccountId(string userId)
        {
            string sql = @"
            SELECT
            vaults.*,
            accounts.*
            FROM vaults
            JOIN accounts ON accounts.id = vaults.creatorId
            WHERE vaults.creatorId = @userId
            ;";

            List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { userId }).ToList();
            return vaults;
        }
    }
}