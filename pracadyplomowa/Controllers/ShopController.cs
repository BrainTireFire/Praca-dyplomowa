using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers
{
    public class ShopController(IUnitOfWork unitOfWork) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpPost]
        public async Task<ActionResult<int>> CreateShop(ShopInsertDto shopInsertDto)
        {
            var shop = new Shop
            {
                Name = shopInsertDto.Name,
                Type = shopInsertDto.Type,
                Location = shopInsertDto.Location,
                Description = shopInsertDto.Description,
                R_CampaignId = shopInsertDto.CampaignId
            };

            _unitOfWork.ShopRepository.Add(shop);

            await _unitOfWork.SaveChangesAsync();
            return Ok(shop.Id);
        }

        [HttpGet("campaign/{campaignId}")]
        public async Task<ActionResult<ICollection<Shop>>> GetShops(int campaignId)
        {
            List<Shop> shops = await _unitOfWork.ShopRepository.GetShops(campaignId);

            List<ShopDto> shopsDto = shops.Select(e => new ShopDto(e)).ToList();

            return Ok(shopsDto);
        }

        [HttpGet("{shopId}")]
        public ActionResult<Shop> GetShop(int shopId)
        {
            var shop = _unitOfWork.ShopRepository.GetById(shopId);

            if (shop == null)
                return NotFound();

            ShopDto shopsDto = new(shop);

            return Ok(shopsDto);
        }

        [HttpDelete("{shopId}")]
        public async Task<ActionResult> RemoveShop(int shopId)
        {
            _unitOfWork.ShopRepository.Delete(shopId);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}