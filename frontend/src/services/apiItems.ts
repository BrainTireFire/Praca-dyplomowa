import { EffectBlueprint } from "../features/effects/EffectBlueprintForm";
import { Item } from "../features/items/models/item";
import { ImmaterialResourceAmount } from "../models/immaterialResourceAmount";
import { ItemListItem } from "../models/item";
import { PowerListItem } from "../models/power";
import { Slot } from "../models/slot";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getItems(): Promise<ItemListItem[]> {
  const response = await customFetch(`${BASE_URL}/api/item/`);

  console.log(response);

  return response;
}

export async function getItem(itemId: number): Promise<Item> {
  const response = await customFetch(`${BASE_URL}/api/item/${itemId}`);
  return response;
}

export async function postItem(itemDto: Item): Promise<void> {
  console.log(itemDto);
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(itemDto),
  };
  let endpointName;
  if (itemDto.itemType === "MeleeWeapon") {
    endpointName = "meleeWeapon";
  } else if (itemDto.itemType === "RangedWeapon") {
    endpointName = "rangedWeapon";
  } else if (itemDto.itemType === "Apparel") {
    endpointName = "apparel";
  } else throw new Error("Unknown item type");
  await customFetch(`${BASE_URL}/api/item/${endpointName}`, options);
  return;
}

export async function updateItem(itemDto: Item): Promise<void> {
  console.log(itemDto);
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(itemDto),
  };
  let endpointName;
  if (itemDto.itemType === "MeleeWeapon") {
    endpointName = "meleeWeapon";
  } else if (itemDto.itemType === "RangedWeapon") {
    endpointName = "rangedWeapon";
  } else if (itemDto.itemType === "Apparel") {
    endpointName = "apparel";
  } else throw new Error("Unknown item type");
  await customFetch(`${BASE_URL}/api/item/${endpointName}`, options);
  return;
}

export async function addSlot(slotId: number, itemId: number): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(slotId),
  };
  await customFetch(`${BASE_URL}/api/item/${itemId}/slots`, options);
  return;
}

export async function removeSlot(
  slotId: number,
  itemId: number
): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(`${BASE_URL}/api/item/${itemId}/slots/${slotId}`, options);
  return;
}

export async function getItemSlots(itemId: number): Promise<Slot[]> {
  const response = await customFetch(`${BASE_URL}/api/item/${itemId}/slots`);

  console.log(response);

  return response;
}

export async function updateItemSlots(
  itemId: number,
  slots: Slot[]
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(slots),
  };
  await customFetch(`${BASE_URL}/api/item/${itemId}/slots`, options);
  return;
}

export async function getItemPowers(itemId: number): Promise<PowerListItem[]> {
  const response = await customFetch(`${BASE_URL}/api/item/${itemId}/powers`);

  console.log(response);

  return response;
}

export async function updateItemPowers(
  itemId: number,
  powers: PowerListItem[]
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(powers),
  };
  await customFetch(`${BASE_URL}/api/item/${itemId}/powers`, options);
  return;
}

export async function getItemResources(
  itemId: number
): Promise<ImmaterialResourceAmount[]> {
  const response = await customFetch(
    `${BASE_URL}/api/item/${itemId}/resources`
  );

  console.log(response);

  return response;
}

export async function updateItemResources(
  itemId: number,
  resources: ImmaterialResourceAmount[]
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(resources),
  };
  await customFetch(`${BASE_URL}/api/item/${itemId}/resources`, options);
  return;
}

export async function addEffectInstance(
  effectBlueprintDto: EffectBlueprint,
  itemId: number
): Promise<void> {
  console.log(effectBlueprintDto);
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(effectBlueprintDto),
  };
  await customFetch(`${BASE_URL}/api/item/${itemId}/effects`, options);
  return;
}
