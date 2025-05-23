import { EffectBlueprint } from "../features/effects/EffectBlueprintForm";
import { ItemFamily } from "../models/itemfamily";
import { Language } from "../models/language";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getItemFamilies(
  effectId: number | null
): Promise<ItemFamily[]> {
  const response = await customFetch(
    `${BASE_URL}/api/effectBlueprint/itemFamilies${
      !!effectId ? `?effectId=${effectId}` : ""
    }`
  );

  console.log(response);

  return response;
}
export async function getLanguages(
  effectId: number | null
): Promise<Language[]> {
  const response = await customFetch(
    `${BASE_URL}/api/effectBlueprint/languages${
      !!effectId ? `?effectId=${effectId}` : ""
    }`
  );

  console.log(response);

  return response;
}
export async function getEffectBlueprint(
  effectId: number
): Promise<EffectBlueprint> {
  const response = await customFetch(
    `${BASE_URL}/api/effectBlueprint/${effectId}`
  );

  console.log(response);

  return response;
}

export async function updateEffectBlueprint(
  effectBlueprintDto: EffectBlueprint
): Promise<void> {
  console.log(effectBlueprintDto);
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(effectBlueprintDto),
  };
  await customFetch(`${BASE_URL}/api/effectBlueprint`, options);
  return;
}

export async function deleteEffectBlueprint(id: number): Promise<void> {
  console.log(id);
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(`${BASE_URL}/api/effectBlueprint/${id}`, options);
  return;
}
