import { DiceSet } from "./diceset";

export type WeaponAttack = {
  main: true;
  damage: DiceSet;
  damageType: string;
  range: 0;
};
