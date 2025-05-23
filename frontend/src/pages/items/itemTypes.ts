export const itemTypes = [
  "Item",
  // "Tool", TODO
  "Clothing",
  "LightArmor",
  "MediumArmor",
  "HeavyArmor",
  "Shield",
  "SimpleRangedWeapon",
  "SimpleMeleeWeapon",
  "MartialRangedWeapon",
  "MartialMeleeWeapon",
] as const;

export type ItemType = (typeof itemTypes)[number];

export const itemTypeLabel = Object.fromEntries(
  itemTypes.map((type) => [type, type.replace(/([A-Z])/g, " $1").trim()])
);

export const itemTypeOptions = itemTypes.map((type) => ({
  value: type,
  label: itemTypeLabel[type],
}));

export const itemIdentity = [
  "MundaneItem",
  // "Tool", TODO
  "Apparel",
  "MeleeWeapon",
  "RangedWeapon",
] as const;

export type ItemIdentity = (typeof itemIdentity)[number];

type ObjectWithKeysAndValues<A extends PropertyKey, B> = {
  [key in A]: B[];
};

type IdentityToTypeMapping = ObjectWithKeysAndValues<ItemIdentity, ItemType>;

export const identityToTypeMapping: IdentityToTypeMapping = {
  MundaneItem: ["Item"],
  Apparel: ["Clothing", "LightArmor", "MediumArmor", "HeavyArmor", "Shield"],
  // Tool: ["Tool"], TODO
  MeleeWeapon: ["SimpleMeleeWeapon", "MartialMeleeWeapon"],
  RangedWeapon: ["SimpleRangedWeapon", "MartialRangedWeapon"],
};

export const itemTypeLabels: Record<ItemType, string> = {
  Item: "Item",
  // Tool: "Tool", // TODO
  Clothing: "Clothing",
  LightArmor: "Light armor",
  MediumArmor: "Medium armor",
  HeavyArmor: "Heavy armor",
  Shield: "Shield",
  SimpleRangedWeapon: "Simple ranged weapons",
  SimpleMeleeWeapon: "Simple melee weapons",
  MartialRangedWeapon: "Martial ranged weapons",
  MartialMeleeWeapon: "Martial melee weapons",
};

export function getItemTypeLabel(value: ItemType): string {
  return itemTypeLabels[value];
}