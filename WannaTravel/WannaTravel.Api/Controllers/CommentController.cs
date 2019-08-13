using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WannaTravel.BusinessLogic.Entities;
using WannaTravel.BusinessLogic.Interfaces;
using WannaTravel.Models.DbEntities;
using WannaTravel.Repositories.Interfaces;

namespace WannaTravel.Api.Controllers
{
    [RoutePrefix("api/Comment")]
    public class CommentController : ApiController
    {
        private readonly ICommentCollection _commentService;
        private readonly ICommentRepository _commentRepo;


        public CommentController(ICommentCollection commentCollection
            , ICommentRepository commentRepo)
        {
            _commentService = commentCollection;
            _commentRepo = commentRepo;
        }

        [HttpPost]
        public async Task AddComment(CommentEntity comment)
        {
            await Task.Run(() =>
            {
                _commentService.AddComment(comment);
            });
        }

        [HttpGet]
        public async Task<ICollection<Comment>> GetComments()
        {
            _commentService.manualResetEvent.WaitOne();

            return await _commentRepo.GetAllAsync();

        }
    }
}
