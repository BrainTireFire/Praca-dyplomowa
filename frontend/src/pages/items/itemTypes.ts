export const itemTypes = [
  "MundaneItem",
  "Tool",
  "Apparel",
  "MeleeWeapon",
  "RangedWeapon",
] as const;

export type ItemType = (typeof itemTypes)[number];
