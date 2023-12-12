


namespace keeper.Repositories
{
    public class ProfilesRepo
    {
        private readonly IDbConnection _db;

        public ProfilesRepo(IDbConnection db)
        {
            _db = db;
        }

        internal Profile GetById(string id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id });
        }

        internal Profile Create(Profile newProfile)
        {
            string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
            _db.Execute(sql, newProfile);
            return newProfile;
        }
    }
}