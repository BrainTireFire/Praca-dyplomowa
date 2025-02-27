import { createContext } from "react";

export const EffectParentObjectIdContext =
  createContext<EffectParentObjectIdContextType>({
    objectId: null,
    objectType: "ItemWearer",
  });

export type EffectParentObjectIdContextType = {
  objectId: number | null;
  objectType: EffectParentObjectIdContextTypeObjectType;
};

export type EffectParentObjectIdContextTypeObjectType =
  | "ItemWearer"
  | "ItemItself"
  | "CharacterConstant"
  | "CharacterTemporary"
  | "FieldConstant"
  | "FieldTemporary";
