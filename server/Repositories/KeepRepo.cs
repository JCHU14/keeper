





namespace keeper.Repositories
{
    public class KeepRepo
    {

        private readonly IDbConnection _db;

        public KeepRepo(IDbConnection db)
        {
            _db = db;
        }

        private Keep KeepBuilder(Keep keep, Profile profile)
        {
            keep.Creator = profile;
            return keep;
        }



        internal Keep CreateKeep(Keep keepData)
        {
            string sql = @"
            INSERT INTO
            keeps (name, description, img, creatorId, views, kept)
            VALUES (@Name, @Description, @Img, @CreatorId, @Views, @Kept);
            
            SELECT
            keep.*,
            acc.*
            FROM keeps keep
            JOIN accounts acc ON acc.id = keep.creatorId
            WHERE keep.id = LAST_INSERT_ID();";

            Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
            return keep;
        }

        internal void DestroyKeep(int keepId)
        {
            string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
            _db.Execute(sql, new { keepId });
        }

        internal Keep GetKeepById(int keepId)
        {
            string sql = @"
            SELECT
            keep.*,
            acc.*
            FROM keeps keep
            JOIN accounts acc ON acc.id = keep.creatorId
            WHERE keep.id = @keepId;";

            Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { keepId }).FirstOrDefault();
            return keep;
        }

        internal List<Keep> GetKeeps()
        {
            string sql = @"
            SELECT
            keep.*,
            acc.*
            FROM keeps keep
            JOIN accounts acc ON acc.id = keep.creatorId;";

            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder).ToList();
            return keeps;
        }

        internal Keep UpdateKeep(Keep keepData)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            views = @views,
            kept = @Kept
            
            WHERE id = @Id LIMIT 1;
            
            SELECT
            keep.*,
            acc.*
            FROM keeps keep
            JOIN accounts acc ON acc.id = keep.creatorId
            WHERE keep.id = @Id;";

            Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
            return keep;
        }

        internal List<Keep> GetKeepsByProfileId(string profileId)
        {
            string sql = @"
            SELECT
            keeps.*,
            accounts.*
            FROM keeps
            JOIN accounts ON accounts.id = keeps.creatorId
            WHERE keeps.creatorId = @profileId
            ;";

            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { profileId }).ToList();
            return keeps;
        }


    }
}