import { DiceSet, DiceSetDefaultValue } from "../../../models/diceset";
import { ImmaterialResourceBlueprint } from "../../../models/immaterialResourceBlueprint";
import { PowerListItem } from "../../../models/power";
import { Slot } from "../../../models/slot";
import { ItemType } from "../../../pages/items/itemTypes";
import { damageType } from "../../effects/damageTypes";
import { skill } from "../../effects/skills";
import { EffectBlueprintListItem } from "../../powers/models/effectBlueprint";
import { weight } from "../enums/weight";
import { CoinPurse } from "./coinPurse";

export type Item = {
  id: number | null;
  name: string;
  description: string;

  weight: number;
  price: CoinPurse;
  itemType: ItemType;
  itemFamilyId: number | null;
  itemTypeBody:
    | MundaneItemBody
    | ToolBody
    | ApparelBody
    | MeleeWeaponBody
    | RangedWeaponBody;
};

export type MundaneItemBody = {};
export const mundaneItemBodyInitialValue: MundaneItemBody = {};

export type ToolBody = {
  skill: skill;
};

export const toolBodyInitialValue: ToolBody = {
  skill: "Acrobatics", // Replace with a valid `skill` type
};

export type EquippableItemBody = {
  effectsOnWearer: EffectBlueprintListItem[];
  powers: PowerListItem[];
  powersOnHit: PowerListItem[];
  //   resources: (ImmaterialResourceBlueprint & { charges: number })[];
  resourcesOnEquip: (ImmaterialResourceBlueprint & { charges: number })[];
  slots: Slot[];
  isSpellFocus: boolean;
  occupiesAllSlots: boolean;
};

export type ApparelBody = EquippableItemBody & {
  minimumStrength: number;
  disadvantageOnStealth: boolean;
  armorClass: number;
};

export const apparelBodyInitialValue: ApparelBody = {
  effectsOnWearer: [],
  powers: [],
  powersOnHit: [],
  resourcesOnEquip: [],
  slots: [],
  isSpellFocus: false,
  minimumStrength: 0,
  disadvantageOnStealth: false,
  occupiesAllSlots: false,
  armorClass: 0,
};

type WeaponBody = EquippableItemBody & {
  damage: DiceSet;
  damageType: damageType;
  weightProperty: weight;
};

export type MeleeWeaponBody = WeaponBody & {
  reach: boolean;
  finesse: boolean;
  throwable: false;
  rangeThrowable: number;
  versatile: boolean;
  versatileDamage: DiceSet;
};

export const meleeWeaponBodyInitialValue: MeleeWeaponBody = {
  effectsOnWearer: [],
  powers: [],
  powersOnHit: [],
  resourcesOnEquip: [],
  slots: [],
  isSpellFocus: false,
  damage: DiceSetDefaultValue,
  versatile: false,
  versatileDamage: DiceSetDefaultValue,
  damageType: "bludgeoning",
  weightProperty: "Normal",
  reach: false,
  finesse: false,
  occupiesAllSlots: false,
  throwable: false,
  rangeThrowable: 10,
};

export type RangedWeaponBody = WeaponBody & { range: number; loaded: boolean };

export const rangedWeaponBodyInitialValue: RangedWeaponBody = {
  effectsOnWearer: [],
  powers: [],
  powersOnHit: [],
  resourcesOnEquip: [],
  slots: [],
  isSpellFocus: false,
  damage: DiceSetDefaultValue,
  damageType: "slashing",
  weightProperty: "Normal",
  range: 0,
  loaded: false,
  occupiesAllSlots: false,
};

export type MeleeThrowableWeaponBody = MeleeWeaponBody & { range: number };

export const itemInitialValue: Item = {
  id: null,
  name: "New item",
  description: "Describe your item",
  weight: 0,
  price: { goldPieces: 0, silverPieces: 0, copperPieces: 0 },
  itemType: "Apparel",
  itemFamilyId: null,
  itemTypeBody: apparelBodyInitialValue,
};
