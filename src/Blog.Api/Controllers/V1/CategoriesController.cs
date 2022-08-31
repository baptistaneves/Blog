using Blog.Api.Contracts.Categories.Request;
using Blog.Api.Contracts.Categories.Response;
using Blog.Application.Categories.Commands;
using Blog.Application.Categories.Queries;

namespace Blog.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles ="Admin")]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Listar todas categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(CancellationToken token)
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IEnumerable<CategoryResponse>>(result.Payload));
        }

        [HttpGet, Route(ApiRoutes.Category.GetCategoryById)]
        [ValidateGuid("categoryId")]
        public async Task<IActionResult> GetCategoryId(string categoryId, CancellationToken token)
        {
            var categoryIdGuid = Guid.Parse(categoryId);
            var query = new GetCategoryByIdQuery { CategoryId = categoryIdGuid };
            var result = await _mediator.Send(query, token);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<CategoryResponse>(result.Payload));
        }

        [HttpPost, Route(ApiRoutes.Category.AddCategory)]
        [ValidateModel]
        public async Task<IActionResult> AddCategory([FromBody] CreateUpdateCategoryRequest newCategory, 
            CancellationToken token)
        {
            var command = new AddCategoryCommand { Description = newCategory.Description };
            var result = await _mediator.Send(command, token);

            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(_mapper.Map<CategoryResponse>(result.Payload));
        }

        [HttpPut, Route(ApiRoutes.Category.UpdateCategory)]
        [ValidateModel]
        [ValidateGuid("categoryId")]
        public async Task<IActionResult> UpdateCategory([FromBody] CreateUpdateCategoryRequest updatedCategory,
            string categoryId, CancellationToken token)
        {
            var categoryIdGuid = Guid.Parse(categoryId);
            var command = new UpdateCategoryCommand 
            { 
                Description = updatedCategory.Description,
                CategoryId = categoryIdGuid
            };

            var result = await _mediator.Send(command, token);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete, Route(ApiRoutes.Category.RemoveCategory)]
        [ValidateGuid("categoryId")]
        public async Task<IActionResult> RemoveCategory(string categoryId, CancellationToken token)
        {
            var categoryIdGuid = Guid.Parse(categoryId);
            var command = new RemoveCategoryCommand { CategoryId = categoryIdGuid };

            var result = await _mediator.Send(command, token);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }
    }
}
