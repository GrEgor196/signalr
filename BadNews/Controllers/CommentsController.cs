using System;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsRepository commentsRepository;

        public CommentsController(CommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }
        
        [HttpGet("api/news/{id}/comments")]
        public ActionResult<CommentsDto> GetCommentsForNews(Guid newsId)
        {
            var data = commentsRepository.GetComments(newsId);
            if (data == null)
                return NotFound();

            var commentsDto = new CommentsDto
            {
                NewsId = newsId,
                Comments = data.Select(comment => new CommentDto
                {
                    User = comment.User,
                    Value = comment.Value
                }).ToList()
            };
            
            return Ok(commentsDto);
        }
    }
}