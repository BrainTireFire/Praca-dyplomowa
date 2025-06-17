import { CoinPurse } from "../features/items/models/coinPurse";

export type Shop = {
  id: number;
  name: string;
  type: string;
  location: string;
  description: string;
};

export type ShopInsertDto = {
  name: string;
  type: string;
  location: string;
  description: string;
  campaignId: number;
};

export type ShopItem = {
  id: number;
  name: string;
  weight: number;
  description: string;
  price: CoinPurse;
  quantity: number;
};

export type ShopCharacterDto = {
  id: number;
  items: ShopItem[];
  coinPurse: CoinPurse;
  itemsWeight: number;
};
