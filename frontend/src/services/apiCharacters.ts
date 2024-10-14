import {
  CharacterItem,
  Character,
  CharacterInsertDto,
} from "../models/character";
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

export type ChoiceGroup = {
  id: number;
  name: string;
  numberToChoose: number;
  effects: Effect[];
  powers: Power[];
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

export type ChoiceGroupUse = {
  id: number;
  effectIds: number[];
  powerIds: number[];
};
