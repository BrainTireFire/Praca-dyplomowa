import { SavingThrow } from "./savingthrow";
import { Attribute } from "./attribute";
import { Skill } from "./skill";
import { Language } from "./language";
import { ItemFamily } from "./itemfamily";
import { Race } from "./race";
import { CharacterClass } from "./characterclass";
import { DiceSet } from "./diceset";
import { WeaponAttack } from "./weaponattack";
import { Item } from "./item";

export type CharacterItem = {
  id: number;
  name: string;
  description: string;
  class: string;
  race: string;
};

export type Character = {
  id: number;
  name: string;
  description: string;
  attributes: Attribute[];
  savingThrows: SavingThrow[];
  skills: Skill[];
  languages: Language[];
  toolProficiencies: ItemFamily[];
  weaponAndArmorProficiencies: ItemFamily[];
  race: Race;
  class: CharacterClass;
  hitpoints: {
    current: number;
    maximum: number;
    temporary: number;
  };
  initiative: number;
  speed: number;
  armorClass: number;
  deathSaves: {
    successes: number;
    failures: number;
  };
  hitDice: {
    total: DiceSet;
    left: DiceSet;
  };
  weaponAttacks: WeaponAttack[];
  equipent: Item[];
};
