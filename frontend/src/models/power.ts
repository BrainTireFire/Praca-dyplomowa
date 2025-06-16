import { DiceSet } from "./diceset";

export type Power = {
  id: number;
  name: string;
  source: string[];
  difficultyClass: number;
  attackBonus: DiceSet;
};

export type PowerListItem = {
  id: number;
  name: string;
  description: string;
};
