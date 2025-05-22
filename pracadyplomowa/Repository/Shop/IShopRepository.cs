using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository
{
    public interface IShopRepository : IBaseRepository<Shop>
    {
        public Task<List<Shop>> GetShops(int campaignId);
        public Task<List<ShopItem>> GetShopItems(int shopId);
        public Task<ShopItem> GetShopItem(int shopId, int itemId);
        public void AddShopItem(ShopItem shopItem);
        public void RemoveShopItem(ShopItem shopItem);
        public Task<int> GetOwnerId(int shopId);
    }
}