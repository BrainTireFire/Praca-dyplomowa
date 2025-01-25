import {
  ConditionalEffectSelection,
  ConditionalEffectsSelectionSet,
  StateType,
} from "../features/campaigns/session/PowerCastConditionalEffectsReducer";
import {
  conditionalEffectSet,
  conditionalEffectType,
  stateType,
} from "../features/campaigns/session/WeaponAttackConditionalEffectsReducer";
import { DiceSet } from "../models/diceset";
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

export async function deleteParticipanceData(
  encounterId: number,
  characterId: number
): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
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
  hitpoints: number;
  maxHitpoints: number;
  temporaryHitpoints: number;
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

export async function makeAttackRoll(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean,
  approvedConditionalEffects: ApprovedConditionalEffectsDto
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

export interface ApprovedConditionalEffectsDto {
  CasterConditionalEffects: number[];
  TargetConditionalEffects: number[];
}

export async function applyWeaponHitEffects(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean,
  isCritical: boolean,
  approvedConditionalEffects: ApprovedConditionalEffectsDto
): Promise<AppliedDamage> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(approvedConditionalEffects),
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/weaponHit?characterId=${characterId}&targetId=${targetId}&weaponId=${weaponId}&isRanged=${isRanged}&isCritical=${isCritical}`,
    options
  );
}
export type AppliedDamage = { [key: string]: number };

export async function getWeaponAttackData(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean
): Promise<WeaponAttackData> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/weaponAttackData?characterId=${characterId}&targetId=${targetId}&weaponId=${weaponId}&isRanged=${isRanged}`,
    options
  );
}

export interface WeaponAttackData {
  weaponDamageAndPowers: WeaponDamageAndPowersDto;
  conditionalEffects: ConditionalEffectsDto;
}

export interface WeaponDamageAndPowersDto {
  weaponId: number;
  weaponName: string;
  damageValues: DamageValueDto[];
  powersOnHit: PowersOnHitDto[];
}

export interface DamageValueDto {
  damageType: string;
  damageValue: DiceSet;
  damageSource: string;
}

export interface PowersOnHitDto {
  powerId: number;
  powerName: string;
  powerDescription: string;
  powerEffects: PowerEffectDto[];
}

export interface PowerEffectDto {
  powerEffectId: number;
  powerEffectName: string;
  powerEffectDescription: string;
}

export interface ConditionalEffectsDto {
  casterConditionalEffects: ConditionalEffectDto[];
  targetConditionalEffects: ConditionalEffectDto[];
}
export interface ConditionalEffectDto {
  effectId: number;
  effectName: string;
  effectDescription: string;
  selected: boolean;
}

export async function makeWeaponAttack(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean,
  approvedConditionalEffects: stateType
): Promise<WeaponAttackResultDto> {
  /**
   * Transforms a stateType object by keeping only `effectId` where `selected === true`.
   * @param {stateType} state - The state object to transform.
   * @returns {stateType} - A new state object with transformed conditional effects.
   */
  function transformState(state: stateType) {
    if (!state) {
      throw new Error("Invalid state object");
    }

    // Helper function to filter and map conditionalEffectType arrays
    const filterEffectIds = (effects: conditionalEffectType[]) =>
      effects
        .filter((effect) => effect.selected)
        .map((effect) => effect.effectId);

    // Transform a conditionalEffectSet
    const transformConditionalEffectSet = (
      effectSet: conditionalEffectSet
    ) => ({
      casterConditionalEffects: filterEffectIds(
        effectSet.casterConditionalEffects
      ),
      targetConditionalEffects: filterEffectIds(
        effectSet.targetConditionalEffects
      ),
    });

    // Transform the state object
    return {
      weaponAttackConditionalEffects: transformConditionalEffectSet(
        state.weaponAttackConditionalEffects
      ),
      powers: state.powers.map((power) => ({
        powerId: power.powerId,
        powerConditionalEffects: transformConditionalEffectSet(
          power.powerConditionalEffects
        ),
      })),
    };
  }

  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(transformState(approvedConditionalEffects)),
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/makeWeaponAttack?characterId=${characterId}&targetId=${targetId}&weaponId=${weaponId}&isRanged=${isRanged}`,
    options
  );
}

export interface WeaponAttackResultDto {
  attackRollResult: string; // Represents the serialized HitType
  powerResult: PowerUsageResultDto[];
  totalDamage: number;
  hitpointsLeft: number;
}
interface PowerUsageResultDto {
  powerName: string;
  success: boolean;
}

/**
 * Generates a query string for an array of integers.
 * @param {string} paramName - The name of the query parameter (e.g., "ids").
 * @param {number[]} values - The array of integers to include in the query string.
 * @returns {string} - The generated query string.
 */
function generateQueryString(
  paramName: string,
  values: (number | string | boolean)[]
) {
  if (!Array.isArray(values) || values.length === 0) {
    return "";
  }

  // Map the array into repeated key=value pairs and join them with "&"
  const queryString = values
    .map(
      (value) => `${encodeURIComponent(paramName)}=${encodeURIComponent(value)}`
    )
    .join("&");
  return `${queryString}`;
}

export async function getPowerData(
  encounterId: number,
  characterId: number,
  powerId: number,
  targetIds: number[]
): Promise<PowerDataAndConditionalEffectsDto> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/powerCastData?characterId=${characterId}&powerId=${powerId}&${generateQueryString(
      "targetIds",
      targetIds
    )}`,
    options
  );
}

export type PowerDataForResolutionDto = {
  powerId: number;
  powerName: string;
  resourceName: string;
  powerEffects: Record<number, Record<number, PowerEffectDto[]>>;
  availableImmaterialResourceLevels: number[];
};

export type PowerDataAndConditionalEffectsDto = {
  powerData: PowerDataForResolutionDto;
  conditionalEffects: ConditionalEffectsSetForManyTargetsDto;
};

export type ConditionalEffectsSetForManyTargetsDto = {
  casterConditionalEffects: ConditionalEffectDto[];
  targetData: TargetDataDto[];
};

export type TargetDataDto = {
  targetId: number;
  targetName: string;
  targetConditionalEffects: ConditionalEffectDto[];
};

export async function castPower(
  encounterId: number,
  characterId: number,
  powerId: number,
  approvedConditionalEffects: StateType
): Promise<CastPowerResultDto> {
  /**
   * Transforms a stateType object by keeping only `effectId` where `selected === true`.
   * @param {stateType} state - The state object to transform.
   * @returns {stateType} - A new state object with transformed conditional effects.
   */
  function transformState(state: StateType) {
    if (!state) {
      throw new Error("Invalid state object");
    }

    // Helper function to filter and map conditionalEffectType arrays
    const filterEffectIds = (effects: ConditionalEffectSelection[]) =>
      effects
        .filter((effect) => effect.selected)
        .map((effect) => effect.effectId);

    function cleanConditionalEffectSelection(
      input: Record<number, ConditionalEffectSelection[]>
    ): Record<number, number[]> {
      const result: Record<number, number[]> = {};

      for (const [key, value] of Object.entries(input)) {
        const filtered = filterEffectIds(value);

        result[Number(key)] = filtered;
      }

      return result;
    }
    // Transform a conditionalEffectSet
    const transformConditionalEffectSet = (
      effectSet: ConditionalEffectsSelectionSet
    ) => ({
      casterConditionalEffects: filterEffectIds(
        effectSet.casterConditionalEffects
      ),
      targetConditionalEffects: cleanConditionalEffectSelection(
        effectSet.targetConditionalEffects
      ),
    });

    // Transform the state object
    return {
      spellSlotLevel: state.spellSlotLevel,
      conditionalEffects: transformConditionalEffectSet(
        state.conditionalEffects
      ),
    };
  }

  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(transformState(approvedConditionalEffects)),
  };

  return await customFetch(
    `${BASE_URL}/api/encounter/${encounterId}/castPower?characterId=${characterId}&powerId=${powerId}`,
    options
  );
}

export type CastPowerResultDto = {
  hitMap: Record<number, HitType>; // HitType is serialized to a string
  nameMap: Record<number, string>;
};

export type HitType = "CriticalHit" | "Hit" | "Miss" | "CriticalMiss";
