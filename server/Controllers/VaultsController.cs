
namespace keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth0Provider;

        public VaultsController(Auth0Provider auth0Provider, VaultsService vaultsService)
        {
            _auth0Provider = auth0Provider;
            _vaultsService = vaultsService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Vault>>> GetVaults(string name)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                List<Vault> vaults = _vaultsService.GetVaults(name, userInfo?.Id);
                return Ok(vaults);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }



        [HttpGet("{vaultId}")]
        public ActionResult<Vault> GetVaultById(int vaultId)
        {
            try
            {
                Vault vault = _vaultsService.GetVaultById(vaultId);
                return Ok(vault);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                Vault vault = _vaultsService.CreateVault(vaultData);
                return Ok(vault);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [Authorize]
        [HttpPut("{vaultId}")]
        public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                Vault vault = _vaultsService.UpdateVault(vaultId, userInfo.Id, vaultData);
                return Ok(vault);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [Authorize]
        [HttpDelete("{vaultId}")]
        public async Task<ActionResult<string>> DestroyVault(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultsService.DestroyVault(vaultId, userInfo.Id);
                return Ok(message);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
    }
}