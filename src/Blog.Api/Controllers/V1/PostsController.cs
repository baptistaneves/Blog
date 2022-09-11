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
        public async Task<IActionResult> CreatePost([FromBody] CreatePost newPost, CancellationToken token)
        {
            var prefix = Guid.NewGuid() + "_";
            if (!await UploadFile(newPost.Image, prefix)) return HandleErrorResponse();

            var userProfileId = HttpContext.GetUserProfileIdCliamValue();

            var command = _mapper.Map<CreatePostCommand>(newPost);
            command.UserProfileId = userProfileId;
            command.Image = prefix + newPost.Image.FileName;

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<PostResponse>(result.Payload));
        }

        [HttpPatch, Route(ApiRoutes.Post.UpdatePost)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePost updatedPost, string postId, CancellationToken token)
        {
            var command = _mapper.Map<UpdatePostCommand>(updatedPost);
            command.PostId = Guid.Parse(postId);

            if (updatedPost.Image != null)
            {
                var post = await _mediator.Send(new GetPostByIdQuery { PostId = Guid.Parse(postId) }, token);
                if (post.IsError) return HandleErrorResponse(post.Errors);

                DeleteFile(post.Payload.Image);

                var prefix = Guid.NewGuid() + "_";

                if (!await UploadFile(updatedPost.Image, prefix)) return HandleErrorResponse();

                command.Image = prefix + updatedPost.Image.FileName;
            }
            
            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete, Route(ApiRoutes.Post.RemovePost)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> RemovePost(string postId, CancellationToken token)
        {
            var postIdGuid = Guid.Parse(postId);
            var command = new RemovePostByIdCommand { PostId = postIdGuid };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpGet, Route(ApiRoutes.Post.GetAllPostComments)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetAllPostComments(string postId, CancellationToken token)
        {
            var query = new GetAllPostCommentsQuery { PostId = Guid.Parse(postId) };

            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IEnumerable<PostCommentResponse>>(result.Payload));
        }

        [HttpGet, Route(ApiRoutes.Post.GetCommentById)]
        [ValidateGuid("postId", "commentId")]
        public async Task<IActionResult> GetCommentById(string postId, string commentId, CancellationToken token)
        {
            var query = new GetPostCommentByIdQuery
            {
                PostId = Guid.Parse(postId),
                PostCommentId = Guid.Parse(commentId)
            };
            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<PostCommentResponse>(result.Payload));
        }

        [HttpPost, Route(ApiRoutes.Post.AddPostComment)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> AddPostComment(string postId, [FromBody] CreateUpdatePostComment newComment,
            CancellationToken token)
        {
            var command = new AddPostCommentCommand
            {
                PostId = Guid.Parse(postId),
                UserProfileId = HttpContext.GetUserProfileIdCliamValue(),
                Text = newComment.Text
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(result.Payload);
        }

        [HttpPut, Route(ApiRoutes.Post.UpdateComment)]
        [ValidateGuid("postId", "commentId")]
        public async Task<IActionResult> UpdatePostComment(string postId, string commentId,
            [FromBody] CreateUpdatePostComment updatedComment, CancellationToken token)
        {
            var command = new UpdatePostCommentCommand
            {
                PostId = Guid.Parse(postId),
                PostCommentId = Guid.Parse(commentId),
                Text = updatedComment.Text
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete, Route(ApiRoutes.Post.RemovePostComment)]
        [ValidateGuid("postId", "commentId")]
        public async Task<IActionResult> RemovePostComment(string postId, string commentId,
            CancellationToken token)
        {
            var command = new RemovePostCommentCommand
            {
                PostCommentId = Guid.Parse(commentId),
                PostId = Guid.Parse(postId)
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }


        [HttpGet, Route(ApiRoutes.Post.GetAllPostReactions)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetAllPostReactions(string postId, CancellationToken token)
        {
            var query = new GetAllPostReactionsQuery { PostId = Guid.Parse(postId) };

            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IEnumerable<PostReactionResponse>>(result.Payload));
        }

        [HttpPost, Route(ApiRoutes.Post.AddPostReaction)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> AddPostReaction(string postId, [FromBody] CreatePostReaction reaction,
            CancellationToken token)
        {
            var command = new AddPostReactionCommand
            {
                PostId = Guid.Parse(postId),
                UserProfileId = HttpContext.GetUserProfileIdCliamValue(),
                ReactionType = reaction.ReactionType
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<PostReactionResponse>(result.Payload));
        }

        [HttpDelete, Route(ApiRoutes.Post.RemovePostReaction)]
        [ValidateGuid("postId", "reactionId")]
        public async Task<IActionResult> AddPostReaction(string postId, string reactionId,
            [FromBody] CreatePostReaction reaction, CancellationToken token)
        {
            var command = new RemovePostReactionCommand
            {
                PostId = Guid.Parse(postId),
                PostReactionId = Guid.Parse(reactionId)
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpGet, Route(ApiRoutes.Post.GetAllCommentAnswers)]
        [ValidateGuid("postId", "commentId")]
        public async Task<IActionResult> GetAllCommentAnswers(string postId, string commentId,
            CancellationToken token)
        {
            var query = new GetAllCommentAnswersQuery
            {
                PostId = Guid.Parse(postId),
                CommentId = Guid.Parse(commentId)
            };

            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IEnumerable<CommentAnswerResponse>>(result.Payload));
        }

        [HttpGet, Route(ApiRoutes.Post.GetCommentAnswerById)]
        [ValidateGuid("postId", "commentId", "commentAnswerId")]
        public async Task<IActionResult> GetCommentAnswerById(string postId, string commentId,
            string commentAnswerId, CancellationToken token)
        {
            var query = new GetCommentAnswerByIdQuery
            {
                PostId = Guid.Parse(postId),
                CommentId = Guid.Parse(commentId),
                CommentAnswerId = Guid.Parse(commentAnswerId)
            };

            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<CommentAnswerResponse>(result.Payload));
        }

        [HttpPost, Route(ApiRoutes.Post.AddCommentAnswer)]
        [ValidateGuid("postId", "commentId")]
        [ValidateModel]
        public async Task<IActionResult> AddCommentAnswer(string postId, string commentId,
            [FromBody] CreateUpdateCommentAnswer newAnswer, CancellationToken token)
        {
            var command = new AddCommentAnswerCommand
            {
                PostId = Guid.Parse(postId),
                CommentId = Guid.Parse(commentId),
                UserProfileId = HttpContext.GetUserProfileIdCliamValue(),
                Text = newAnswer.Text
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(result.Payload);
        }

        [HttpPut, Route(ApiRoutes.Post.UpdateCommentAnswer)]
        [ValidateGuid("postId", "commentId", "commentAnswerId")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCommentAnswer(string postId, string commentId,
            string commentAnswerId, [FromBody] CreateUpdateCommentAnswer answerUpdated, CancellationToken token)
        {
            var command = new UpdateCommentAnswerCommand
            {
                PostId = Guid.Parse(postId),
                CommentId = Guid.Parse(commentId),
                CommentAnswerId = Guid.Parse(commentAnswerId),
                UpdatedText = answerUpdated.Text,
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete, Route(ApiRoutes.Post.RemoveCommentAnswer)]
        [ValidateGuid("postId", "commentId", "commentAnswerId")]
        public async Task<IActionResult> RemoveCommentAnswer(string postId, string commentId,
            string commentAnswerId, CancellationToken token)
        {
            var command = new RemoveCommentAnswerCommand
            {
                PostId = Guid.Parse(postId),
                PostCommentId = Guid.Parse(commentId),
                CommentAnswerId = Guid.Parse(commentAnswerId)
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpGet, Route(ApiRoutes.Post.GetAllCommentReactions)]
        [ValidateGuid("postId", "commentId")]
        public async Task<IActionResult> GetAllCommentReactions(string postId, string commentId,
            CancellationToken token)
        {
            var query = new GetAllCommentReactionsQuery
            {
                PostId = Guid.Parse(postId),
                PostCommentId = Guid.Parse(commentId)
            };

            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IEnumerable<CommentReactionResponse>>(result.Payload));
        }

        [HttpPost, Route(ApiRoutes.Post.AddCommentReaction)]
        [ValidateGuid("postId", "commentId")]
        public async Task<IActionResult> AddCommentReaction(string postId, string commentId,
            [FromBody] CreateCommentReaction newReaction, CancellationToken token)
        {
            var command = new AddCommentReactionCommand
            {
                PostId = Guid.Parse(postId),
                PostCommentId = Guid.Parse(commentId),
                UserProfileId = HttpContext.GetUserProfileIdCliamValue(),
                ReactionType = newReaction.ReactionType
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(result.Payload);
        }

        [HttpDelete, Route(ApiRoutes.Post.RemoveCommentReaction)]
        [ValidateGuid("postId", "commentId", "reactionId")]
        public async Task<IActionResult> RemoveCommentReaction(string postId, string commentId,
            string reactionId, CancellationToken token)
        {

            var command = new RemoveCommentReactionCommand
            {
                PostId = Guid.Parse(postId),
                PostCommentId = Guid.Parse(commentId),
                CommentReactionId = Guid.Parse(reactionId)
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [NonAction]
        public async Task<bool> UploadFile(IFormFile file, string prefix)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", prefix + file.FileName);

            if (System.IO.File.Exists(path))
            {
                AddError("Já existe uma imagem com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        [NonAction]
        public void DeleteFile(string file)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + file);

            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            AddError("A imagem não foi encontrada!");
        }
    }
}
