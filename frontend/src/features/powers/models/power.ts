import { ability } from "../../effects/abilities";
import { CoinPurse } from "../../items/models/coinPurse";
import { EffectBlueprintListItem } from "./effectBlueprint";

export type Power = {
  id: number | null;
  name: string;
  description: string;
  requiredActionType: ActionType;
  isImplemented: boolean;
  isMagic: boolean;
  castableBy: CastableBy;
  powerType: PowerType;
  targetType: TargetType;
  isRanged: boolean;
  range: number;
  maxTargets: number;
  maxTargetsToExclude: number;
  areaSize: number;
  areaShape: AreaShape;
  auraSize: number;
  overrideCastersDC: boolean;
  difficultyClass: number;
  savingThrowAbility: ability | null;
  requiresConcentration: boolean;
  savingThrowBehaviour: SavingThrowBehaviour;
  savingThrowRoll: SavingThrowRoll;
  verbalComponent: boolean;
  somaticComponent: boolean;
  duration: number;
  upcastBy: UpcastBy;
  classForUpcasting: number | null;
  immaterialResourceUsed: ImmaterialResource | null;
  materialResourcesUsed: MaterialComponent[];
  effectBlueprints: EffectBlueprintListItem[];
  editable: boolean;
};

export type ActionType =
  | "Action"
  | "BonusAction"
  // | "Reaction"
  | "WeaponAttack"
  | "None";
export const ActionTypeLabels: { [key in ActionType]: string } = {
  Action: "Action",
  BonusAction: "Bonus Action",
  // Reaction: "Reaction",
  WeaponAttack: "Weapon Attack",
  None: "None",
};
export const actionTypeOptions = (
  Object.keys(ActionTypeLabels) as ActionType[]
).map((key) => ({
  value: key,
  label: ActionTypeLabels[key],
}));

export type CastableBy = "Character" | "OnWeaponHit" | "Terrain";
export const CastableByLabels: { [key in CastableBy]: string } = {
  Character: "Character",
  OnWeaponHit: "On Weapon Hit",
  Terrain: "Terrain",
};

export const castableByOptions = (
  Object.keys(CastableByLabels) as CastableBy[]
).map((key) => ({
  value: key,
  label: CastableByLabels[key],
}));

export type PowerType =
  | "Attack"
  | "Saveable"
  // | "MinionSpawner_1_of"
  // | "MinionSpawner_multiple"
  // | "AuraCreator"
  | "PassiveEffect";
// | "Polymorphism"
export const PowerTypeLabels: { [key in PowerType]: string } = {
  Attack: "Attack",
  Saveable: "Saveable",
  // MinionSpawner_1_of: "Minion Spawner (1 of)",
  // MinionSpawner_multiple: "Minion Spawner (Multiple)",
  PassiveEffect: "Passive Effect",
  // AuraCreator: "Aura Creator",
  // Polymorphism: "Polymorphism",
};

export const powerTypeOptions = (
  Object.keys(PowerTypeLabels) as PowerType[]
).map((key) => ({
  value: key,
  label: PowerTypeLabels[key],
}));

export type TargetType = "Caster" | "Character";
// | "Weapon"
// | "Armor"
// | "MapTiles";
export const TargetTypeLabels: { [key in TargetType]: string } = {
  Caster: "Caster",
  Character: "Character",
  // Weapon: "Weapon",
  // Armor: "Armor",
  // MapTiles: "Map Tiles",
};
export const targetTypeOptions = (
  Object.keys(TargetTypeLabels) as TargetType[]
).map((key) => ({
  value: key,
  label: TargetTypeLabels[key],
}));

export type AreaShape =
  | "Cone"
  | "Cube"
  | "Cylinder"
  | "Line"
  | "Sphere"
  | "None";
export const AreaShapeLabels: { [key in AreaShape]: string } = {
  Cone: "Cone",
  Cube: "Cube",
  Cylinder: "Cylinder",
  Line: "Line",
  Sphere: "Sphere",
  None: "None",
};
export const areaShapeOptions = (
  Object.keys(AreaShapeLabels) as AreaShape[]
).map((key) => ({
  value: key,
  label: AreaShapeLabels[key],
}));

export type SavingThrowBehaviour = "Breaks" | "Modifies";
export const SavingThrowBehaviourLabels: {
  [key in SavingThrowBehaviour]: string;
} = {
  Breaks: "Breaks",
  Modifies: "Modifies",
};
export const savingThrowBehaviourOptions = (
  Object.keys(SavingThrowBehaviourLabels) as SavingThrowBehaviour[]
).map((key) => ({
  value: key,
  label: SavingThrowBehaviourLabels[key],
}));

export type SavingThrowRoll = "None" | "TakenOnce" | "RetakenEveryTurn";
export const SavingThrowRollLabels: { [key in SavingThrowRoll]: string } = {
  None: "None",
  TakenOnce: "Taken Once",
  RetakenEveryTurn: "Retaken Every Turn",
};
export const savingThrowRollOptions = (
  Object.keys(SavingThrowRollLabels) as SavingThrowRoll[]
).map((key) => ({
  value: key,
  label: SavingThrowRollLabels[key],
}));

export type UpcastBy =
  | "ResourceLevel"
  | "CharacterLevel"
  | "ClassLevel"
  | "NotUpcasted";
export const UpcastByLabels: { [key in UpcastBy]: string } = {
  ResourceLevel: "Resource level",
  CharacterLevel: "Character level",
  ClassLevel: "Class level",
  NotUpcasted: "Not upcasted",
};
export const upcastByOptions = (Object.keys(UpcastByLabels) as UpcastBy[]).map(
  (key) => ({
    value: key,
    label: UpcastByLabels[key],
  })
);

export type CharacterClass = {
  id: number;
  name: string;
};
export type ImmaterialResource = {
  id: number;
  name: string;
};
export type MaterialComponent = {
  id: number | null;
  itemFamilyId: number | null;
  name: string;
  worth: CoinPurse;
};
