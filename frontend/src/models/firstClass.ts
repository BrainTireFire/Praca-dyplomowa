import { ability } from "../features/effects/abilities";
import { DiceSet } from "./diceset";

export type FirstClass = {
  id: number;
  name: string;
  level: number;
  mainAbility: ability;
  hitpoints: number;
  hitDice: DiceSet;
};
