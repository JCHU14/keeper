

namespace keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepService _keepService;
        private readonly Auth0Provider _auth0Provider;

        public KeepsController(KeepService keepService, Auth0Provider auth0Provider)
        {
            _keepService = keepService;
            _auth0Provider = auth0Provider;
        }



        [HttpGet]
        public async Task<ActionResult<List<Keep>>> GetKeeps(string name)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                List<Keep> keeps = _keepService.GetKeeps(name, userInfo?.Id);
                return Ok(keeps);

            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpGet("{keepId}")]
        public ActionResult<Keep> GetKeepById(int keepId)
        {
            try
            {
                Keep keep = _keepService.GetKeepById(keepId);
                return Ok(keep);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                keepData.CreatorId = userInfo.Id;
                Keep keep = _keepService.CreateKeep(keepData);
                return Ok(keep);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }




        [Authorize]
        [HttpPut("{keepId}")]
        public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                Keep keep = _keepService.UpdateKeep(keepId, userInfo.Id, keepData);
                return Ok(keep);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [Authorize]
        [HttpDelete("{keepId}")]
        public async Task<ActionResult<string>> DestroyKeep(int keepId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                string message = _keepService.DestroyKeep(keepId, userInfo.Id);
                return Ok(message);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
    }
}