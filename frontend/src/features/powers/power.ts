import { ability } from "../effects/abilities";
import { EffectBlueprint } from "../effects/EffectBlueprintForm";
import { CoinPurse } from "../items/coinPurse";

export type Power = {
  name: string;
  description: string;
  actionType: ActionType;
  isImplemented: boolean;
  castableBy: CastableBy;
  powerType: PowerType;
  targetType: TargetType;
  range: number;
  maxTargets: number;
  maxTargetsToExclude: number;
  areaSize: number;
  areaShape: AreaShape;
  auraSize: number | null;
  difficultyClass: number | null;
  savingThrowAbility: ability;
  requiresConcentration: boolean;
  savingThrowBehaviour: SavingThrowBehaviour;
  savingThrowRoll: SavingThrowRoll;
  verbalComponent: boolean;
  somaticComponent: boolean;
  duration: number;
  upcastBy: UpcastBy;
  classForUpcasting: CharacterClass;
  immaterialResourceUsed: ImmaterialResource;
  materialResourcesUsed: MaterialResource[];
  effectBlueprints: EffectBlueprint;
};

export type ActionType = "action" | "bonusAction" | "reaction" | "none";
export const ActionTypeLabels: { [key in ActionType]: string } = {
  action: "Action",
  bonusAction: "Bonus Action",
  reaction: "Reaction",
  none: "None",
};
export const actionTypeOptions = (
  Object.keys(ActionTypeLabels) as ActionType[]
).map((key) => ({
  value: key,
  label: ActionTypeLabels[key],
}));

export type CastableBy = "character" | "onWeaponHit" | "terrain";
export const CastableByLabels: { [key in CastableBy]: string } = {
  character: "Character",
  onWeaponHit: "On Weapon Hit",
  terrain: "Terrain",
};

export const castableByOptions = (
  Object.keys(CastableByLabels) as CastableBy[]
).map((key) => ({
  value: key,
  label: CastableByLabels[key],
}));

export type PowerType =
  | "attack"
  | "saveable"
  | "minionSpawner_1_of"
  | "minionSpawner_multiple"
  | "auraCreator"
  | "passiveEffect"
  | "polymorphism";
export const PowerTypeLabels: { [key in PowerType]: string } = {
  attack: "Attack",
  saveable: "Saveable",
  minionSpawner_1_of: "Minion Spawner (1 of)",
  minionSpawner_multiple: "Minion Spawner (Multiple)",
  auraCreator: "Aura Creator",
  passiveEffect: "Passive Effect",
  polymorphism: "Polymorphism",
};

export const powerTypeOptions = (
  Object.keys(PowerTypeLabels) as PowerType[]
).map((key) => ({
  value: key,
  label: PowerTypeLabels[key],
}));

export type TargetType =
  | "caster"
  | "character"
  | "weapon"
  | "armor"
  | "mapTiles";
export const TargetTypeLabels: { [key in TargetType]: string } = {
  caster: "Caster",
  character: "Character",
  weapon: "Weapon",
  armor: "Armor",
  mapTiles: "Map Tiles",
};
export const targetTypeOptions = (
  Object.keys(TargetTypeLabels) as TargetType[]
).map((key) => ({
  value: key,
  label: TargetTypeLabels[key],
}));

export type AreaShape =
  | "cone"
  | "cube"
  | "cylinder"
  | "line"
  | "sphere"
  | "none";
export const AreaShapeLabels: { [key in AreaShape]: string } = {
  cone: "Cone",
  cube: "Cube",
  cylinder: "Cylinder",
  line: "Line",
  sphere: "Sphere",
  none: "None",
};
export const areaShapeOptions = (
  Object.keys(AreaShapeLabels) as AreaShape[]
).map((key) => ({
  value: key,
  label: AreaShapeLabels[key],
}));

export type SavingThrowBehaviour = "breaks" | "modifies";
export const SavingThrowBehaviourLabels: {
  [key in SavingThrowBehaviour]: string;
} = {
  breaks: "Breaks",
  modifies: "Modifies",
};
export const savingThrowBehaviourOptions = (
  Object.keys(SavingThrowBehaviourLabels) as SavingThrowBehaviour[]
).map((key) => ({
  value: key,
  label: SavingThrowBehaviourLabels[key],
}));

export type SavingThrowRoll = "none" | "takenOnce" | "retakenEveryTurn";
export const SavingThrowRollLabels: { [key in SavingThrowRoll]: string } = {
  none: "None",
  takenOnce: "Taken Once",
  retakenEveryTurn: "Retaken Every Turn",
};
export const savingThrowRollOptions = (
  Object.keys(SavingThrowRollLabels) as SavingThrowRoll[]
).map((key) => ({
  value: key,
  label: SavingThrowRollLabels[key],
}));

export type UpcastBy = "resourceLevel" | "characterLevel" | "classLevel";
export const UpcastByLabels: { [key in UpcastBy]: string } = {
  resourceLevel: "Resource Level",
  characterLevel: "Character Level",
  classLevel: "Class Level",
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
export type MaterialResource = {
  id: number;
  name: string;
  worth: CoinPurse;
};
