import {
  Shop,
  ShopCharacterDto,
  ShopInsertDto,
  ShopItem,
} from "../models/shop";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function postShop(shopInsertDto: ShopInsertDto): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(shopInsertDto),
  };
  const data = await customFetch(`${BASE_URL}/api/shop`, options);
  return Number(data);
}

export async function getShops(campaignId: number): Promise<Shop[]> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(
    `${BASE_URL}/api/shop/campaign/${campaignId}`,
    options
  );

  return data;
}

export async function getShop(shopId: number): Promise<Shop> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(`${BASE_URL}/api/shop/${shopId}`, options);

  return data;
}

export async function removeShop(shopId: number): Promise<void> {
  await customFetch(`${BASE_URL}/api/shop/${shopId}`, { method: "DELETE" });
}

export async function getAllItems(): Promise<ShopItem[]> {
  const data = await customFetch(
    `${BASE_URL}/api/shop/items?IsBlueprint=true`,
    { method: "GET" }
  );

  return data;
}

export async function getShopItems(shopId: number): Promise<ShopItem[]> {
  const data = await customFetch(`${BASE_URL}/api/shop/${shopId}/items`, {
    method: "GET",
  });

  return data;
}

export async function patchShopItem(shopId: number, shopItem: ShopItem) {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(shopItem),
  };
  await customFetch(`${BASE_URL}/api/shop/${shopId}/items`, options);
}

export async function removeShopItem(shopId: number, itemId: number) {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(itemId),
  };
  await customFetch(`${BASE_URL}/api/shop/${shopId}/items`, options);
}

export async function getShopCharacter(
  campaignId: number
): Promise<ShopCharacterDto> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(
    `${BASE_URL}/api/shop/character/${campaignId}`,
    options
  );

  return data;
}

export async function buyItem(
  shopId: number,
  characterId: number,
  itemId: number
) {
  const options: RequestInit = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ shopId, characterId, itemId }),
  };

  return await customFetch(`${BASE_URL}/api/shop/${shopId}/buy`, options);
}

export async function sellItem(
  shopId: number,
  characterId: number,
  itemId: number
) {
  const options: RequestInit = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ shopId, characterId, itemId }),
  };

  return await customFetch(`${BASE_URL}/api/shop/${shopId}/sell`, options);
}
