import { EffectBlueprint } from "../features/effects/EffectBlueprintForm";
import { ItemFamily } from "../models/itemfamily";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getItemFamilies(): Promise<ItemFamily[]> {
  const response = await customFetch(
    `${BASE_URL}/api/effectBlueprint/itemFamilies`
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
