

namespace keeper.Services
{
    public class KeepService
    {
        private readonly KeepRepo _repository;

        public KeepService(KeepRepo repository)
        {
            _repository = repository;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            Keep keep = _repository.CreateKeep(keepData);
            return keep;
        }

        internal string DestroyKeep(int keepId, string userId)
        {
            Keep keep = GetKeepById(keepId);

            if (keep.CreatorId != userId)
            {
                throw new Exception("NOT YOURS TO DELETE");
            }

            _repository.DestroyKeep(keepId);

            return $"{keep.Name} has been deleted";
        }

        internal Keep GetKeepById(int keepId)
        {
            Keep keep = _repository.GetKeepById(keepId);
            if (keep == null)
            {
                throw new Exception($"Invalid Id: {keepId}");
            }
            keep.Views++;
            Keep keep1 = UpdateViewCount(keep);
            return keep;

        }

        internal Keep UpdateViewCount(Keep keepData)
        {
            Keep keep = _repository.UpdateKeep(keepData);
            return keep;
        }


        internal List<Keep> GetKeeps(string name, string id)
        {
            List<Keep> keeps = _repository.GetKeeps();
            return keeps;
        }

        internal List<Keep> GetKeepsByProfileId(string profileId)
        {
            List<Keep> keeps = _repository.GetKeepsByProfileId(profileId);
            return keeps;
        }

        internal Keep UpdateKeep(int keepId, string userId, Keep keepData)
        {
            Keep keepToUpdate = GetKeepById(keepId);

            if (keepToUpdate.CreatorId != userId)
            {
                throw new Exception("NOT YOURS TO EDIT");
            }

            keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
            keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;
            keepToUpdate.Img = keepData.Img ?? keepToUpdate.Img;

            Keep keep = _repository.UpdateKeep(keepToUpdate);
            return keep;
        }




    }

}
