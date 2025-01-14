import { Encounter } from "../models/encounter/Encounter";
import { EncounterCreateDto } from "../models/encounter/EncounterCreateDto";
import { EncounterUpdateDto } from "../models/encounter/EncounterUpdateDto";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function createEncounter(
  encounterCreateDto: EncounterCreateDto
): Promise<EncounterCreateDto> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(encounterCreateDto),
  };

  const data: EncounterCreateDto = await customFetch(
    `${BASE_URL}/api/encounter`,
    options
  );
  return data;
}

export async function updatePlaceEncounter(
  encounterId: number,
  encounterUpdateDto: EncounterUpdateDto[]
): Promise<null> {
  const options: RequestInit = {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      IsActive: true,
      FieldsToUpdate: encounterUpdateDto,
    }),
  };

  await customFetch(
    `${BASE_URL}/api/encounter/placeEncounter/${encounterId}`,
    options
  );

  return null;
}

export async function getEncounters(): Promise<Encounter[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: Encounter[] = await customFetch(
    `${BASE_URL}/api/encounter/myEncounters`,
    options
  );

  return data;
}

export async function getEncounter(encounterId: number): Promise<Encounter> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: Encounter = await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}`,
    options
  );

  return data;
}
export async function rollInitiative(encounterId: number): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };

  await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/initiative`,
    options
  );

  return;
}
export async function modifyInitiative(
  encounterId: number,
  newQueue: InitiativeOrderItem[]
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(newQueue),
  };

  await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/initiative`,
    options
  );

  return;
}
export type InitiativeOrderItem = {
  characterId: number;
  placeInQueue: number;
};
export async function getInitiativeQueue(
  encounterId: number
): Promise<InitiativeQueueItem[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/initiative`,
    options
  );
}
export type InitiativeQueueItem = {
  characterId: number;
  name: string;
  playerName: string;
  placeInQueue: number;
  initiativeRollResult: number;
  activeTurn: boolean;
};
export async function getIsGM(encounterId: number): Promise<boolean> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/gmCheck`,
    options
  );
}
export async function setActiveTurn(
  encounterId: number,
  characterId: number
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };

  await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/initiative/${characterId}`,
    options
  );

  return;
}
