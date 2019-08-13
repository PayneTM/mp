using WannaTravel.Models.DbEntities;
using WannaTravel.Repositories.Interfaces;

namespace WannaTravel.Repositories.EF.Repos
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository() : base()
        {
        }

        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
