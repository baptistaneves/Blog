namespace Blog.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController()
        {

        }

        /// <summary>
        /// Listar todas categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(new
            {
                Id = Guid.NewGuid(),
                Description = "Moves",
                Version = "V1"
            });
        }
    }
}
