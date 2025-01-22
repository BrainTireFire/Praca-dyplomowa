export const statusEffects = [
  "Blinded",
  "Charmed",
  "Deafened",
  "Frightened",
  "Grappled",
  "Incapacitated",
  "Invisible",
  "Paralyzed",
  "Petrified",
  "Poisoned",
  "Prone",
  "Restrained",
  "Stunned",
  "Unconscious",
  "Exhaustion_1",
  "Exhaustion_2",
  "Exhaustion_3",
  "Exhaustion_4",
  "Exhaustion_5",
  "Exhaustion_6",
  "Muffled",
] as const;

export type statusEffect = (typeof statusEffects)[number];

export const StatusEffectLabelMap = {
  Blinded: "Blinded",
  Charmed: "Charmed",
  Deafened: "Deafened",
  Frightened: "Frightened",
  Grappled: "Grappled",
  Incapacitated: "Incapacitated",
  Invisible: "Invisible",
  Paralyzed: "Paralyzed",
  Petrified: "Petrified",
  Poisoned: "Poisoned",
  Prone: "Prone",
  Restrained: "Restrained",
  Stunned: "Stunned",
  Unconscious: "Unconscious",
  Exhaustion_1: "Exhaustion Level 1",
  Exhaustion_2: "Exhaustion Level 2",
  Exhaustion_3: "Exhaustion Level 3",
  Exhaustion_4: "Exhaustion Level 4",
  Exhaustion_5: "Exhaustion Level 5",
  Exhaustion_6: "Exhaustion Level 6",
  Muffled: "Muffled",
} as const;

export const statusEffectDropdown = statusEffects.map((x) => {
  return { value: x, label: StatusEffectLabelMap[x] };
});
