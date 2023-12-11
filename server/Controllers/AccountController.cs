namespace keeper.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultKeepsService _vaultKeepService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultKeepsService vaultKeepService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultKeepService = vaultKeepService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("vaultKeeps")]
  public async Task<ActionResult<List<VaultKeepVault>>> GetVaultKeepsByAccountId()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<VaultKeepVault> vaultKeepVaults = _vaultKeepService.GetVaultKeepsByAccountId(userId);
      return Ok(vaultKeepVaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
