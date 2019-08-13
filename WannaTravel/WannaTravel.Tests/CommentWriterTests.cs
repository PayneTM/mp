using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WannaTravel.BusinessLogic.Entities;
using WannaTravel.BusinessLogic.Interfaces;

namespace WannaTravel.Tests
{
    [TestClass]
    public class CommentWriterTests
    {
        private static Random random = new Random();
        private static List<CommentEntity> Comments = new List<CommentEntity>();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [TestMethod]
        public void CorrectCommentsOrderInDb()
        {
            var comments = new List<CommentEntity>();
            var repo = new Mock<ICommentCollection>(MockBehavior.Loose);
            repo.Setup(x => x.AddComment(It.IsAny<CommentEntity>())).Callback(() => comments.Add(GenerateComment()));

            repo.Object.AddComment(new CommentEntity());
            repo.Object.AddComment(new CommentEntity());

            Assert.IsTrue(Comments.FirstOrDefault().Text == comments.FirstOrDefault().Text);

        }

        private CommentEntity GenerateComment()
        {
            var comment = new CommentEntity { RestaurantId = 1, Rate = 2, Text = RandomString() };
            Comments.Add(comment);
            return comment;

        }

        private string RandomString()
        {
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
