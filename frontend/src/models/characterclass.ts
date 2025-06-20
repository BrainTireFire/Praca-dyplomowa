import { ability } from "../features/effects/abilities";
import { DiceSet } from "./diceset";

export type CharacterClass = {
  id: number;
  name: string;
  level: number;
  mainAbility: ability;
  difficultyClass: number;
  attackBonus: number;
};
