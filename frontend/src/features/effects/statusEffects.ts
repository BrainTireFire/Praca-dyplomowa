export const statusEffects = [
  "blinded",
  "charmed",
  "deafened",
  "frightened",
  "grappled",
  "incapacitated",
  "invisible",
  "paralyzed",
  "petrified",
  "poisoned",
  "prone",
  "restrained",
  "stunned",
  "unconscious",
  "exhaustion_1",
  "exhaustion_2",
  "exhaustion_3",
  "exhaustion_4",
  "exhaustion_5",
  "exhaustion_6",
] as const;

export type statusEffect = (typeof statusEffects)[number];

export const StatusEffectLabelMap = {
  blinded: "Blinded",
  charmed: "Charmed",
  deafened: "Deafened",
  frightened: "Frightened",
  grappled: "Grappled",
  incapacitated: "Incapacitated",
  invisible: "Invisible",
  paralyzed: "Paralyzed",
  petrified: "Petrified",
  poisoned: "Poisoned",
  prone: "Prone",
  restrained: "Restrained",
  stunned: "Stunned",
  unconscious: "Unconscious",
  exhaustion_1: "Exhaustion Level 1",
  exhaustion_2: "Exhaustion Level 2",
  exhaustion_3: "Exhaustion Level 3",
  exhaustion_4: "Exhaustion Level 4",
  exhaustion_5: "Exhaustion Level 5",
  exhaustion_6: "Exhaustion Level 6",
} as const;

export const statusEffectDropdown = statusEffects.map((x) => {
  return { value: x, label: StatusEffectLabelMap[x] };
});
