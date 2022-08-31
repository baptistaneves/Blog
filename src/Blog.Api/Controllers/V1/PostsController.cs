using Blog.Api.Contracts.Posts.Requests;
using Blog.Api.Contracts.Posts.Responses;
using Blog.Application.Posts.Commands;
using Blog.Application.Posts.Queries;

namespace Blog.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Admin, Editor")]
    public class PostsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts(CancellationToken token)
        {
            var query = new GetAllPostsQuery();
            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(result.Payload);
        }

        [HttpGet, Route(ApiRoutes.Post.GetPostById)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetPostById(string postId, CancellationToken token)
        {
            var postGuidId = Guid.Parse(postId);

            var query = new GetPostByIdQuery { PostId = postGuidId };
            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(result.Payload);
        }

        [HttpPost, Route(ApiRoutes.Post.CreatePost)]
        [ValidateModel]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest newPost, CancellationToken token)
        {
            var userProfileId = HttpContext.GetUserProfileIdCliamValue();
            var command = _mapper.Map<CreatePostCommand>(newPost);
            command.UserProfileId = userProfileId;

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);
           
            return Ok(_mapper.Map<PostResponse>(result.Payload));
        }

        [HttpPatch, Route(ApiRoutes.Post.UpdatePost)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequest updatedPost, string postId, CancellationToken token)
        {
            var postIdGuid = Guid.Parse(postId);
            var command = _mapper.Map<UpdatePostCommand>(updatedPost);
            command.PostId = postIdGuid;

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete, Route(ApiRoutes.Post.RemovePost)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> RemovePost(string postId, CancellationToken token)
        {
            var postIdGuid = Guid.Parse(postId);
            var command = new RemovePostByIdCommand{ PostId = postIdGuid };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }
    }
}
