import { CoinPurse } from "../features/items/coinPurse";

export type ItemFamily = {
  id: number;
  name: string;
};

export type ItemFamilyWithWorth = {
  id: number;
  name: string;
  worth: CoinPurse;
};
