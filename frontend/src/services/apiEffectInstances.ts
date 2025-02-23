import { EffectBlueprint } from "../features/effects/EffectBlueprintForm";
import { ItemFamily } from "../models/itemfamily";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getItemFamilies(
  effectId: number | null
): Promise<ItemFamily[]> {
  const response = await customFetch(
    `${BASE_URL}/api/effectInstance/itemFamilies${
      !!effectId ? `?effectId=${effectId}` : ""
    }`
  );

  console.log(response);

  return response;
}
export async function getEffectInstance(
  effectId: number
): Promise<EffectBlueprint> {
  const response = await customFetch(
    `${BASE_URL}/api/effectInstance/${effectId}`
  );

  console.log(response);

  return response;
}

export async function updateEffectInstance(
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
  await customFetch(`${BASE_URL}/api/effectInstance`, options);
  return;
}

export async function deleteEffectInstance(id: number): Promise<void> {
  console.log(id);
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(`${BASE_URL}/api/effectInstance/${id}`, options);
  return;
}
