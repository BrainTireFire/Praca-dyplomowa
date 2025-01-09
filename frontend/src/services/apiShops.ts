import { Shop, ShopInsertDto } from "../models/shop";
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

  const data = await customFetch(`${BASE_URL}/api/shop/${campaignId}`, options);

  return data;
}
