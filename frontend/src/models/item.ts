import { ItemFamily } from "./itemfamily";
import { Slot } from "./slot";

export type Item = {
  id: number;
  name: string;
  itemFamily: ItemFamily;
  slots: Slot[];
  equipped: boolean;
};
