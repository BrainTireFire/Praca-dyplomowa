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

export async function getEncounters(campaignId: string): Promise<Encounter[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: Encounter[] = await customFetch(
    `${BASE_URL}/api/encounter/myEncounters/${campaignId}`,
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
export async function getIsItMyTurn(
  encounterId: number,
  controlledCharacterId: number
): Promise<boolean> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/turnCheck/${controlledCharacterId}`,
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
export async function nextTurn(encounterId: number): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };

  await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/nextTurn`,
    options
  );

  return;
}
export async function getControlledCharacters(
  encounterId: number
): Promise<number[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/controlledCharacters`,
    options
  );
}
export async function getParticipanceData(
  encounterId: number,
  characterId: number
): Promise<ParticipanceData> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/participanceData/${characterId}`,
    options
  );
}
export async function updateParticipanceData(
  encounterId: number,
  characterId: number,
  participanceData: ParticipanceData
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(participanceData),
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/participanceData/${characterId}`,
    options
  );
}

export type ParticipanceData = {
  characterName: string;
  actionsTaken: number;
  bonusActionsTaken: number;
  attacksMade: number;
  movementUsed: number;
  totalActions: number;
  totalBonusActions: number;
  totalAttacksPerAction: number;
  totalMovement: number;
};

export async function moveCharacter(
  encounterId: number,
  characterId: number,
  fieldIds: number[]
): Promise<number[]> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(fieldIds),
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/movement/${characterId}`,
    options
  );
}

export async function getConditionalEffectsForAttackRoll(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean
): Promise<WeaponAttackConditionalEffectsDto> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/conditionalEffectsForAttackRoll?characterId=${characterId}&targetId=${targetId}&weaponId=${weaponId}&isRanged=${isRanged}`,
    options
  );
}
export async function getConditionalEffectsForWeaponHit(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number
): Promise<WeaponAttackConditionalEffectsDto> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/conditionalEffectsForWeaponHit?characterId=${characterId}&targetId=${targetId}&weaponId=${weaponId}`,
    options
  );
}

export interface WeaponAttackConditionalEffectsDto {
  weaponId: number;
  weaponName: string;
  weaponDescription: string;
  casterConditionalEffects: ConditionalEffectDto[];
  targetsConditionalEffects: Record<number, TargetDto>;
}

export interface ConditionalEffectDto {
  effectId: number;
  effectName: string;
  effectDescription: string;
  selected: boolean;
}

export interface TargetDto {
  targetName: string;
  targetConditionalEffects: ConditionalEffectDto[];
}

export async function makeAttackRoll(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean,
  approvedConditionalEffects: ConditionalEffectsDtos
): Promise<string> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(approvedConditionalEffects),
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/attackRoll?characterId=${characterId}&targetId=${targetId}&weaponId=${weaponId}&isRanged=${isRanged}`,
    options
  );
}

export interface ConditionalEffectsDtos {
  CasterConditionalEffects: number[];
  TargetsConditionalEffects: Record<number, number[]>;
}

export async function applyWeaponHitEffects(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  approvedConditionalEffects: ConditionalEffectsDtos
): Promise<string> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(approvedConditionalEffects),
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/attackRoll?characterId=${characterId}&targetId=${targetId}&weaponId=${weaponId}&isRanged=${isRanged}`,
    options
  );
}
