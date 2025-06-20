import { EffectBlueprint } from "../features/effects/EffectBlueprintForm";
import { Item } from "../features/items/models/item";
import { ImmaterialResourceAmount } from "../models/immaterialResourceAmount";
import { ItemListItem } from "../models/item";
import { ItemFamily } from "../models/itemfamily";
import { PowerListItem } from "../models/power";
import { Slot } from "../models/slot";
import { ItemType } from "../pages/items/itemTypes";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getItems(
  blueprintOrInstance: "blueprint" | "instance" | null
): Promise<ItemListItem[]> {
  let response;
  if (blueprintOrInstance === null) {
    response = await customFetch(`${BASE_URL}/api/item/`);
  }
  if (blueprintOrInstance === "blueprint") {
    response = await customFetch(`${BASE_URL}/api/item?IsBlueprint=true`);
  }
  if (blueprintOrInstance === "instance") {
    response = await customFetch(`${BASE_URL}/api/item?IsBlueprint=false`);
  }

  return response;
}

export async function getItem(itemId: number): Promise<Item> {
  const response = await customFetch(`${BASE_URL}/api/item/${itemId}`);
  return response;
}

export async function postItem(itemDto: Item): Promise<number> {
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
  } else if (itemDto.itemType === "MundaneItem") {
    endpointName = "mundaneItem";
  } else throw new Error("Unknown item type");
  return await customFetch(`${BASE_URL}/api/item/${endpointName}`, options);
}

export async function updateItem(itemDto: Item): Promise<void> {
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
  } else if (itemDto.itemType === "MundaneItem") {
    endpointName = "mundaneItem";
  } else throw new Error("Unknown item type");
  await customFetch(`${BASE_URL}/api/item/${endpointName}`, options);
  return;
}

export async function deleteItem(itemId: number): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(`${BASE_URL}/api/item/${itemId}`, options);
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

export async function addEffectInstanceOnWearer(
  effectBlueprintDto: EffectBlueprint,
  itemId: number
): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(effectBlueprintDto),
  };
  return await customFetch(
    `${BASE_URL}/api/item/${itemId}/effectsOnWearer`,
    options
  );
}
export async function addEffectInstanceOnItem(
  effectBlueprintDto: EffectBlueprint,
  itemId: number
): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(effectBlueprintDto),
  };
  return await customFetch(
    `${BASE_URL}/api/item/${itemId}/effectsOnItem`,
    options
  );
}

export async function getItemFamilies(
  itemId: number | null,
  itemIdentities: ItemType[]
): Promise<ItemFamily[]> {
  const response = await customFetch(
    `${BASE_URL}/api/item/itemFamilies?${
      itemIdentities.length > 0
        ? `&itemType=` + itemIdentities.join("&itemType=")
        : ""
    }${!!itemId ? `&itemId=${itemId}` : ""}`
  );

  return response;
}

export async function giveItemApi(
  itemId: number,
  characterId: number
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
  };
  await customFetch(
    `${BASE_URL}/api/item/give/${itemId}/${characterId}`,
    options
  );
}
