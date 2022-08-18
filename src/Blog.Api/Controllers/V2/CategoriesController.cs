namespace Blog.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(new
            {
                Id = Guid.NewGuid(),
                Description = "Moves",
                Version = "V2"
            });
        }
    }
}
