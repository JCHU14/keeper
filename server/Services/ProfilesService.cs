



namespace keeper.Services
{

    public class ProfilesService
    {
        private readonly ProfilesRepo _repo;

        public ProfilesService(ProfilesRepo repo)
        {
            _repo = repo;
        }

        internal object GetOrCreateProfile(Profile userInfo)
        {
            Profile profile = _repo.GetById(userInfo.Id);
            if (profile == null)
            {
                return _repo.Create(userInfo);
            }
            return profile;
        }

        internal Profile GetProfileById(string profileId)
        {
            Profile profile = _repo.GetById(profileId);
            if (profile == null)
            {
                throw new Exception($"Invalid Id: {profileId}");
            }
            return profile;
        }
    }
}