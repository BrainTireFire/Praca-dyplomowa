import { createContext } from "react";

export const ItemIdContext = createContext<ItemIdContextType>({
  itemId: null,
});

export type ItemIdContextType = {
  itemId: number | null;
};
