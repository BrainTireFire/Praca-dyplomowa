import { DiceSet } from "./diceset";

export type WeaponAttack = {
  id: number;
  main: boolean;
  damage: DiceSet;
  attackBonus: DiceSet;
  damageType: number;
  range: number | null;
  reach: number | null;
};
