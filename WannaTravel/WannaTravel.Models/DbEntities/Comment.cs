using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace WannaTravel.Models.DbEntities
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentatorName { get; set; }
        public int RestaurantId { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Restaurant Restaurant { get; set; }
    }
}
