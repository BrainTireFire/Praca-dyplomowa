import { CoinPurse } from "../features/items/models/coinPurse";
import { ItemType } from "../pages/items/itemTypes";

export type ItemFamily = {
  id: number | null;
  name: string;
  itemType: ItemType;
  ownerName: string;
  editable: boolean;
};

export const itemFamilyInitialValue: ItemFamily = {
  id: null,
  name: "New item family",
  itemType: "Item",
  ownerName: "",
  editable: true,
};
