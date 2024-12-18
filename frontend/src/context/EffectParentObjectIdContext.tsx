import { createContext } from "react";

export const EffectParentObjectIdContext =
  createContext<EffectParentObjectIdContextType>({
    objectId: null,
    objectType: "Item",
  });

export type EffectParentObjectIdContextType = {
  objectId: number | null;
  objectType: EffectParentObjectIdContextTypeObjectType;
};

export type EffectParentObjectIdContextTypeObjectType =
  | "Item"
  | "CharacterConstant"
  | "CharacterTemporary";
