using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WannaTravel.BusinessLogic.Entities;
using WannaTravel.BusinessLogic.Interfaces;
using WannaTravel.Models.DbEntities;
using WannaTravel.Repositories.Interfaces;

namespace WannaTravel.BusinessLogic.Services
{
    public class CommentCollection : ICommentCollection
    {
        private const int MaxCount = 10;
        private readonly ICommentRepository _commentRepository;

        private static object padlock = new object();

        private static Queue<List<Comment>> Queue;
        private static List<List<Comment>> Comments;

        public ManualResetEvent manualResetEvent { get; private set; }

        public CommentCollection(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;

            var list = new List<Comment>();

            Queue = new Queue<List<Comment>>();
            Queue.Enqueue(list);

            Comments = new List<List<Comment>>
            {
                list
            };

            manualResetEvent = new ManualResetEvent(false);
        }

        public void AddComment(CommentEntity comment)
        {
            lock (padlock)
            {
                var entity = new Comment
                {
                    CommentatorName = string.IsNullOrEmpty(comment.CommentatorName) ? "Anon" : comment.CommentatorName,
                    Rate = comment.Rate,
                    RestaurantId = comment.RestaurantId,
                    Text = comment.Text
                };
                GetWorkingCollection().Add(entity); 
            }
        }

        public IEnumerable<Comment> GetItemsToInsert(bool isShutdownRequested = false)
        {
            lock (padlock)
            {
                if (isShutdownRequested || Queue.Peek().Count == MaxCount)
                {
                    return Queue.Dequeue();
                }
                return Enumerable.Empty<Comment>();
            }
        }

        private List<Comment> GetWorkingCollection()
        {
            lock (padlock)
            {
                var list = Comments.FirstOrDefault(x => x.Count < MaxCount);
                if(list == null)
                {
                    var newList = new List<Comment>();
                    Comments.Add(newList);
                    Queue.Enqueue(newList);
                }
                return Comments.FirstOrDefault(x => x.Count < MaxCount); 
            }
        }

    }
}