export const skills = [
  "Acrobatics",
  "AnimalHandling",
  "Arcana",
  "Athletics",
  "Deception",
  "History",
  "Insight",
  "Intimidation",
  "Investigation",
  "Medicine",
  "Nature",
  "Perception",
  "Performance",
  "Persuasion",
  "Religion",
  "SleightOfHand",
  "Stealth",
  "Survival",
] as const;

export type skill = (typeof skills)[number];

export const SkillsLabelMap = {
  Acrobatics: "Acrobatics",
  AnimalHandling: "Animal Handling",
  Arcana: "Arcana",
  Athletics: "Athletics",
  Deception: "Deception",
  History: "History",
  Insight: "Insight",
  Intimidation: "Intimidation",
  Investigation: "Investigation",
  Medicine: "Medicine",
  Nature: "Nature",
  Perception: "Perception",
  Performance: "Performance",
  Persuasion: "Persuasion",
  Religion: "Religion",
  SleightOfHand: "Sleight of Hand",
  Stealth: "Stealth",
  Survival: "Survival",
} as const;

export const skillsDropdown = skills.map((x) => {
  return { value: x, label: SkillsLabelMap[x] };
});
