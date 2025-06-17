using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Items;
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
                return NotFound(new ApiResponse(404, $"Shop not found: {shopId}"));

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

        [HttpGet("items")]
        public async Task<ActionResult<List<ItemFormDto>>> GetItems()
        {
            var items = await _unitOfWork.ItemRepository.GetOwnedItems(User.GetUserId(), new ItemParams { IsBlueprint = true });

            var itemsList = items.Select(e => new ItemFormDto
            {
                Id = e.Id,
                Name = e.Name,
                Weight = e.Weight,
                Description = e.Description,
                Price = new CoinPurseDto
                {
                    GoldPieces = e.Price.GoldPieces,
                    SilverPieces = e.Price.SilverPieces,
                    CopperPieces = e.Price.CopperPieces
                },
            });
            return Ok(itemsList);
        }

        [HttpGet("{shopId}/items")]
        public async Task<ActionResult<List<ItemGetDto>>> GetShopItems(int shopId)
        {
            var items = await _unitOfWork.ShopRepository.GetShopItems(shopId);

            var itemsDto = items.Select(e => new ItemGetDto
            {
                Id = e.R_ShopHasItemId,
                Name = e.R_ShopHasItem.Name,
                Weight = e.R_ShopHasItem.Weight,
                Description = e.R_ShopHasItem.Description,
                Price = new CoinPurseDto
                {
                    GoldPieces = e.Price.GoldPieces,
                    SilverPieces = e.Price.SilverPieces,
                    CopperPieces = e.Price.CopperPieces
                },
                Quantity = e.Quantity
            }).ToList();

            return Ok(itemsDto);
        }

        [HttpPatch("{shopId}/items")]
        public async Task<ActionResult> UpdateShopItems(int shopId, [FromBody] ItemGetDto shopItemDto)
        {
            if (shopId <= 0)
                return BadRequest("Invalid shopId");

            if (await _unitOfWork.ShopRepository.GetOwnerId(shopId) != User.GetUserId())
                return Forbid();

            var existingItem = await _unitOfWork.ShopRepository.GetShopItem(shopId, shopItemDto.Id);
            var shopItems = await _unitOfWork.ShopRepository.GetShopItems(shopId);

            if (shopItems == null)
            {
                shopItems = new List<ShopItem>();
            }

            if (existingItem == null)
            {
                var newItem = new ShopItem
                {
                    R_ShopHasItemId = shopItemDto.Id,
                    R_ItemInShopId = shopId,
                    Price = new CoinSack
                    {
                        GoldPieces = shopItemDto.Price.GoldPieces,
                        SilverPieces = shopItemDto.Price.SilverPieces,
                        CopperPieces = shopItemDto.Price.CopperPieces
                    },
                    Quantity = shopItemDto.Quantity
                };
                _unitOfWork.ShopRepository.AddShopItem(newItem);
            }
            else
            {
                existingItem.Price.GoldPieces = shopItemDto.Price.GoldPieces;
                existingItem.Price.SilverPieces = shopItemDto.Price.SilverPieces;
                existingItem.Price.CopperPieces = shopItemDto.Price.CopperPieces;
                existingItem.Quantity += shopItemDto.Quantity;
            }

            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{shopId}/items")]
        public async Task<ActionResult> RemoveShopItem(int shopId, [FromBody] JsonElement body)
        {
            if (await _unitOfWork.ShopRepository.GetOwnerId(shopId) != User.GetUserId())
                return Forbid();

            if (!body.TryGetProperty("itemId", out var itemIdElement) || !body.TryGetProperty("quantity", out var quantityElement))
            {
                return BadRequest(new ApiResponse(400, "Missing itemId or quantity in request body."));
            }

            int itemId = itemIdElement.GetInt32();
            int quantity = quantityElement.GetInt32();

            var item = await _unitOfWork.ShopRepository.GetShopItem(shopId, itemId);

            if (item == null)
            {
                return NotFound(new ApiResponse(404, "Shop item not found"));
            }

            var diff = item.Quantity - quantity;

            if (diff <= 0)
            {
                _unitOfWork.ShopRepository.RemoveShopItem(item);
            }
            else
            {
                item.Quantity = diff;
            }
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("character/{campaignId}")]
        public async Task<ActionResult<ShopCharacterDto>> GetShopCharacter(int campaignId)
        {
            var userId = User.GetUserId();
            var campaign = await _unitOfWork.CampaignRepository.GetCampaign(userId, campaignId);
            if (campaign == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign with given id - does not exist"));
            }

            var characterId = campaign.R_CampaignHasCharacters.FirstOrDefault(character => character.R_OwnerId == userId)?.Id;
            if (characterId == null)
            {
                return BadRequest(new ApiResponse(400, "You do not have a player character in this campaign"));
            }

            if (characterId <= 0)
                return BadRequest("Invalid characterId");

            var character = await _unitOfWork.ItemRepository.GetCharacterWithBackpackItems((int)characterId);

            var items = character.R_CharacterHasBackpack.R_BackpackHasItems.Select(e => new ItemGetDto
            {
                Id = e.Id,
                Name = e.Name,
                Weight = e.Weight,
                Description = e.Description,
                Price = new CoinPurseDto
                {
                    GoldPieces = e.Price.GoldPieces,
                    SilverPieces = e.Price.SilverPieces,
                    CopperPieces = e.Price.CopperPieces
                },
            });

            var coinSack = character.R_CharacterHasBackpack.CoinSack;
            var CoinPurseDto = new CoinPurseDto
            {
                GoldPieces = coinSack.GoldPieces,
                SilverPieces = coinSack.SilverPieces,
                CopperPieces = coinSack.CopperPieces
            };

            var weight = items.Sum(i => i.Weight);

            var shopCharacterDto = new ShopCharacterDto
            {
                Id = character.Id,
                Items = items.ToList(),
                CoinPurse = CoinPurseDto,
                ItemsWeight = weight
            };
            return shopCharacterDto;
        }

        [HttpPost("{shopId}/buy")]
        public async Task<ActionResult> BuyItem(int shopId, [FromBody] BuyItemDto buyItemDto)
        {
            if (buyItemDto.ShopId != shopId)
            {
                return BadRequest(new ApiResponse(400, "Shop ID in request body does not match the URL."));
            }

            var shopItems = await _unitOfWork.ShopRepository.GetShopItems(shopId);
            if (shopItems == null || !shopItems.Any())
            {
                return NotFound(new ApiResponse(404, "No items available in this shop."));
            }
            var shopItem = shopItems.FirstOrDefault(i => i.R_ShopHasItemId == buyItemDto.ItemId);

            var character = await _unitOfWork.ItemRepository.GetCharacterWithBackpackItems(buyItemDto.CharacterId);
            if (character == null)
            {
                return NotFound(new ApiResponse(404, $"Character not found: {buyItemDto.CharacterId}"));
            }
            var coinSack = character.R_CharacterHasBackpack.CoinSack;
            var backpackItems = character.R_CharacterHasBackpack.R_BackpackHasItems;

            var item = _unitOfWork.ItemRepository.GetById(buyItemDto.ItemId);
            if (item == null)
            {
                return NotFound(new ApiResponse(404, "Item not found or out of stock"));
            }

            if (coinSack < shopItem.Price)
            {
                return BadRequest(new ApiResponse(400, "Not enough coins to buy this item"));
            }

            coinSack.Subtract(shopItem.Price);
            backpackItems.Add(item);

            if (shopItem.Quantity > 1)
            {
                shopItem.Quantity--;
            }
            else
            {
                _unitOfWork.ShopRepository.RemoveShopItem(shopItem);
            }

            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}