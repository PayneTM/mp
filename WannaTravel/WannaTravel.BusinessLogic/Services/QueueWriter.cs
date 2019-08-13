using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WannaTravel.BusinessLogic.Interfaces;
using WannaTravel.Repositories.EF.Repos;

namespace WannaTravel.BusinessLogic.Services
{
    public class QueueWriter
    {
        private readonly ICommentCollection _commentCollection;
        private readonly AutoResetEvent _autoResetEvent;


        public QueueWriter(ICommentCollection commentCollection)
        {
            _commentCollection = commentCollection;
            _autoResetEvent = new AutoResetEvent(false);
        }

        public Task RunTask(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {                    
                    _autoResetEvent.WaitOne(2000);

                    await WriteToDbAsync();
                }
            });
        }

        public async Task WriteToDbAsync(bool isShutdownRequested = false)
        {
            using (var repo = new CommentRepository())
            {
                var data = _commentCollection.GetItemsToInsert(isShutdownRequested);

                if (data.Any())
                {
                    await repo.AddRangeAsync(data);
                    await repo.SaveAsync();
                    _commentCollection.manualResetEvent.Set();
                    _commentCollection.manualResetEvent.Reset();
                }
            }
        }
    }
}
