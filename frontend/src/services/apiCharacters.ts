import {
  CharacterItem,
  Character,
  CharacterInsertDto,
} from "../models/character";
import { DiceSet } from "../models/diceset";
import { ItemFamily } from "../models/itemfamily";
import { Slot } from "../models/slot";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";
import { customFetchJSON } from "./customFetchJSON";

export async function getCharacters(): Promise<CharacterItem[]> {
  const response = await customFetch(`${BASE_URL}/api/character/mycharacters`);

  console.log(response);

  return response;
}

export async function getCharacter(characterId: number): Promise<Character> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}`
  );

  return response;
}

export async function postCharacter(
  characterDto: CharacterInsertDto
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(characterDto),
  };
  await customFetch(`${BASE_URL}/api/character`, options);
  return;
}

export async function getCharactersChoiceGroups(
  characterId: number
): Promise<ChoiceGroup[]> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/choiceGroups`
  );

  return response;
}

export async function makeUseCharacterChoiceGroups(
  characterId: number,
  choiceGroupPayload: ChoiceGroupUse[]
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(choiceGroupPayload),
  };
  await customFetch(
    `${BASE_URL}/api/character/${characterId}/choiceGroups/use`,
    options
  );
  return;
}

export async function getCharacterNextClassLevels(
  characterId: number
): Promise<NextClassLevel[]> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/classes/nextLevels`
  );

  return response;
}

export async function selectNextClassLevel(
  characterId: number,
  classLevelId: number
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(
    `${BASE_URL}/api/character/${characterId}/classes/nextLevels/${classLevelId}/use`,
    options
  );
  return;
}

export async function getEquipmentAndSlots(
  characterId: number
): Promise<CharacterEquipmentAndSlotsDto> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/equipmentSlots`,
    options
  );
  return response;
}

export type ChoiceGroup = {
  id: number;
  name: string;
  numberToChoose: number;
  effects: Effect[];
  powersAlwaysAvailable: Power[];
  powersToPrepare: Power[];
  resources: Resource[];
};

export type Effect = {
  id: number;
  name: string;
  description: string;
};

export type Power = {
  id: number;
  name: string;
  description: string;
};

export type Resource = {
  id: number;
  name: string;
  level: number;
  amount: number;
};

export type ChoiceGroupUse = {
  id: number;
  effectIds: number[];
  powerAlwaysAvailableIds: number[];
  powerToPrepareIds: number[];
  resourceIds: number[];
};
export type NextClassLevel = {
  id: number;
  classId: number;
  level: number;
  name: string;
  choiceGroups: ChoiceGroup[];
  hitDice: DiceSet;
  hitPoints: number;
};

export type CharacterEquipmentAndSlotsDto = {
  items: CharacterEquipmentAndSlotsDto_Item[];
  slots: Slot[];
};

export type CharacterEquipmentAndSlotsDto_Item = {
  id: number;
  name: string;
  itemFamily: ItemFamily;
  slots: Slot[];
  equippableInSlots: Slot[];
};
