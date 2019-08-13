namespace WannaTravel.BusinessLogic.Entities
{
    public class CommentEntity
    {
        public string CommentatorName { get; set; }
        public int RestaurantId { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }
    }
}
