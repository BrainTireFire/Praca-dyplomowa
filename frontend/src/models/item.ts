import { ItemFamily } from "./itemfamily";
import { Slot } from "./slot";

export type Item = {
  id: number;
  name: string;
  itemFamily: ItemFamily;
  slots: Slot[];
  equippableInSlots: Slot[];
  equipped: boolean;
};

export type ItemListItem = {
  id: number;
  name: string;
  ownerName: string;
  description: string;
  weight: number;
};
