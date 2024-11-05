export const abilities = [
  "strength",
  "dexterity",
  "constitution",
  "intelligence",
  "wisdom",
  "charisma",
] as const;

export type ability = (typeof abilities)[number];

export const AbilitiesLabelMap = {
  strength: "Strength",
  dexterity: "Dexterity",
  constitution: "Constitution",
  intelligence: "Intelligence",
  wisdom: "Wisdom",
  charisma: "Charisma",
} as const;

export const abilitiesDropdown = abilities.map((x) => {
  return { value: x, label: AbilitiesLabelMap[x] };
});
