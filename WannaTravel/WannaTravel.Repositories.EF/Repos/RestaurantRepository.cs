using WannaTravel.Models.DbEntities;
using WannaTravel.Repositories.Interfaces;

namespace WannaTravel.Repositories.EF.Repos
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository() : base()
        {
        }

        public RestaurantRepository(ApplicationDbContext context) : base(context)
        {
        }
        
    }
}
