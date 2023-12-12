

namespace keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly Auth0Provider _auth0;
        private readonly AccountService _accountService;
        private readonly ProfilesService _profilesService;

        private readonly VaultsService _vaultsService;
        private readonly KeepService _keepsService;

        public ProfilesController(Auth0Provider auth0, ProfilesService profilesService, AccountService accountService, VaultsService vaultsService, KeepService keepsService)
        {
            _auth0 = auth0;
            _profilesService = profilesService;
            _accountService = accountService;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Profile>> GetProfile()
        {
            try
            {
                Profile userId = await _auth0.GetUserInfoAsync<Profile>(HttpContext);
                return Ok(_accountService.GetOrCreateProfile(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{profileId}")]
        public ActionResult<Profile> GetProfileById(string profileId)
        {
            try
            {
                Profile profile = _profilesService.GetProfileById(profileId);
                return Ok(profile);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }



        [HttpGet("{profileId}/vaults")]
        public ActionResult<List<Vault>> GetVaultsByProfileId(string profileId)
        {
            try
            {

                List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
        {
            try
            {

                List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}