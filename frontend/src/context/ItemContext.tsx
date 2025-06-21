import { createContext } from "react";

export const ItemContext =
  createContext<ItemContextType>({
    objectType: "notapplies",
  });

export type ItemContextType = {
  objectType: ItemTypeObjectType;
};

export type ItemTypeObjectType =
  | "Weapon"
  | "Apparel"
  | "notapplies"
