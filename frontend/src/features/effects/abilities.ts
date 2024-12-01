export const abilities = [
  "STRENGTH",
  "DEXTERITY",
  "CONSTITUTION",
  "INTELLIGENCE",
  "WISDOM",
  "CHARISMA",
] as const;

export type ability = (typeof abilities)[number];

export const AbilitiesLabelMap = {
  STRENGTH: "Strength",
  DEXTERITY: "Dexterity",
  CONSTITUTION: "Constitution",
  INTELLIGENCE: "Intelligence",
  WISDOM: "Wisdom",
  CHARISMA: "Charisma",
} as const;

export const abilitiesDropdown = abilities.map((x) => {
  return { value: x, label: AbilitiesLabelMap[x] };
});
