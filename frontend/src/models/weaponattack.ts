import { DiceSet } from "./diceset";

export type WeaponAttack = {
  id: number;
  main: true;
  damage: DiceSet;
  damageType: string;
  range: 0;
};
