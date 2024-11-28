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
import { Size } from "./size";
import { Power } from "./power";
import { Effect } from "./effect";
import { Resource } from "./resource";
import { ChoiceGroup } from "./choiceGroup";

export type CharacterItem = {
  id: number;
  name: string;
  description: string;
  class: string;
  race: string;
  campaignId: number;
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
  size: Size;
  classes: CharacterClass[];
  hitPoints: {
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
  equipment: Item[];
  preparedPowers: Power[];
  knownPowers: Power[];
  constantEffects: Effect[];
  effects: Effect[];
  resources: Resource[];
  choiceGroups: ChoiceGroup[];
  proficiencyBonus: number;
};

export type CharacterInsertDto = {
  name: string;
  raceId: number;
  startingClassId: number;
  strength: number;
  dexterity: number;
  constitution: number;
  intelligence: number;
  wisdom: number;
  charisma: number;
};
