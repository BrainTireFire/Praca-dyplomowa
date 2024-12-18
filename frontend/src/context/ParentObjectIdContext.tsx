import { createContext } from "react";

export const ParentObjectIdContext = createContext<ParentObjectIdContextType>({
  objectId: null,
  objectType: "Item",
});

export type ParentObjectIdContextType = {
  objectId: number | null;
  objectType: "Item" | "Character";
};
