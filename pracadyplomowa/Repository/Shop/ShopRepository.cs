using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository
{
    public class ShopRepository : BaseRepository<Shop>, IShopRepository
    {
        public ShopRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Shop>> GetShops(int campaignId)
        {
            List<Shop> shops = await _context.Shops
            .Where(e => e.R_CampaignId == campaignId)
            .ToListAsync();

            return shops;
        }

        public async Task<List<ShopItem>> GetShopItems(int shopId)
        {
            List<ShopItem> items = await _context.ShopItems
            .Where(e => e.R_ItemInShop.Id == shopId)
            .Include(e => e.R_ShopHasItem)
            .ToListAsync();

            return items;
        }

        public async Task<ShopItem> GetShopItem(int shopId, int itemId)
        {
            var item = await _context.ShopItems
            .Include(e => e.R_ShopHasItem)
            .FirstOrDefaultAsync(e => e.R_ShopHasItemId == itemId && e.R_ItemInShopId == shopId);

            return item;
        }

        public void AddShopItem(ShopItem shopItem)
        {
            _context.ShopItems.Add(shopItem);
        }

        public void RemoveShopItem(ShopItem shopItem)
        {
            _context.ShopItems.Remove(shopItem);
        }
    }
}