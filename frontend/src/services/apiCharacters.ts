import { EffectBlueprint } from "../features/effects/EffectBlueprintForm";
import {
  CharacterItem,
  Character,
  CharacterInsertDto,
} from "../models/character";
import { DiceSet } from "../models/diceset";
import { ImmaterialResourceAmount } from "../models/immaterialResourceAmount";
import { ItemFamily } from "../models/itemfamily";
import { PowerListItem } from "../models/power";
import { Slot } from "../models/slot";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getCharacters(): Promise<CharacterItem[]> {
  const response = await customFetch(`${BASE_URL}/api/character/mycharacters`);

  return response;
}

export async function getNpcCharacters(): Promise<CharacterItem[]> {
  const response = await customFetch(`${BASE_URL}/api/character/npcCharacters`);

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

export async function deleteCharacter(characterId: number): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(`${BASE_URL}/api/character/${characterId}`, options);
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

export async function equipItemInSlot(
  characterId: number,
  itemId: number,
  slotId: number
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(
    `${BASE_URL}/api/character/${characterId}/equipmentSlots/${slotId}/equip/${itemId}`,
    options
  );
}
export async function unequipItemInSlot(
  characterId: number,
  itemId: number,
  slotId: number
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(
    `${BASE_URL}/api/character/${characterId}/equipmentSlots/${slotId}/unequip/${itemId}`,
    options
  );
}

export async function updateItemResources(
  characterId: number,
  resources: ImmaterialResourceAmount[]
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(resources),
  };
  await customFetch(
    `${BASE_URL}/api/character/${characterId}/resources`,
    options
  );
  return;
}

export async function getCharacterResources(
  characterId: number
): Promise<ImmaterialResourceAmount[]> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/resources`
  );

  console.log(response);

  return response;
}

export async function getCharacterPowers(
  characterId: number
): Promise<PowerListItem[]> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/powers`
  );

  console.log(response);

  return response;
}

export async function getCharacterPowersPreparedForClass(
  characterId: number,
  classId: number
): Promise<PowerListItem[]> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/powersPrepared/class/${classId}`
  );

  console.log(response);

  return response;
}

export async function getCharacterPowersPrepared(
  characterId: number
): Promise<PowerListItem[]> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/powersPrepared`
  );

  console.log(response);

  return response;
}

export async function getCharacterPowersToPrepare(
  characterId: number
): Promise<PowersToPrepare[]> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/powersToPrepare`
  );

  console.log(response);

  return response;
}

export type PowersToPrepare = {
  powerList: PowerListItem[];
  numberToChoose: number;
  classId: number;
  className: string;
};

export async function getCharacterMaxPowersToPrepare(
  characterId: number
): Promise<PowerListItem[]> {
  const response = await customFetch(
    `${BASE_URL}/api/character/${characterId}/maxPowersToPrepare`
  );

  console.log(response);

  return response;
}

export async function updateCharacterKnownPowers(
  characterId: number,
  powers: PowerListItem[]
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(powers),
  };
  await customFetch(`${BASE_URL}/api/character/${characterId}/powers`, options);
  return;
}

export async function updateCharacterPreparedPowers(
  characterId: number,
  classId: number,
  powers: PowerListItem[]
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(powers),
  };
  await customFetch(
    `${BASE_URL}/api/character/${characterId}/powersPrepared/class/${classId}`,
    options
  );
  return;
}

export async function addConstantEffectInstance(
  effectBlueprintDto: EffectBlueprint,
  characterId: number
): Promise<number> {
  console.log(effectBlueprintDto);
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(effectBlueprintDto),
  };

  return await customFetch(
    `${BASE_URL}/api/character/${characterId}/constantEffects`,
    options
  );
}

export async function addTemporaryEffectInstance(
  effectBlueprintDto: EffectBlueprint,
  characterId: number
): Promise<number> {
  console.log(effectBlueprintDto);
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(effectBlueprintDto),
  };

  return await customFetch(
    `${BASE_URL}/api/character/${characterId}/temporaryEffects`,
    options
  );
}
export async function addToEquipment(
  characterId: number,
  itemId: number
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(itemId),
  };

  return await customFetch(
    `${BASE_URL}/api/character/${characterId}/equipment`,
    options
  );
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
  notAllowed: "None" | "ExpertiseWithoutProficiency";
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
