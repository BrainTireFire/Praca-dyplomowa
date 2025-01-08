using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository
{
    public interface IShopRepository : IBaseRepository<Shop>
    {
        public Task<List<Shop>> GetShops(int campaignId);
    }
}