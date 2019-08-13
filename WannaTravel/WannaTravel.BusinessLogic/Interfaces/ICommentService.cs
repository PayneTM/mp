using System.Collections.Generic;
using System.Threading;
using WannaTravel.BusinessLogic.Entities;
using WannaTravel.Models.DbEntities;

namespace WannaTravel.BusinessLogic.Interfaces
{
    public interface ICommentCollection
    {
        void AddComment(CommentEntity comment);

        IEnumerable<Comment> GetItemsToInsert(bool isShutdownRequested = false);

        ManualResetEvent manualResetEvent { get; }

    }
}
