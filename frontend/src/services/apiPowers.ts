import { EffectBlueprint } from "../features/effects/EffectBlueprintForm";
import { MaterialComponent, Power } from "../features/powers/models/power";
import { ImmaterialResourceBlueprint } from "../models/immaterialResourceBlueprint";
import { ItemFamily } from "../models/itemfamily";
import { PowerListItem } from "../models/power";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";
import queryString from "query-string";

export async function getImmaterialResourceBlueprints(
  powerId: number | null
): Promise<ImmaterialResourceBlueprint[]> {
  const response = await customFetch(
    `${BASE_URL}/api/power/immaterialResourceBlueprints${
      !!powerId ? `?powerId=${powerId}` : ""
    }`
  );

  return response;
}

export async function getPowers(
  params?: Record<string, string | number | boolean>
): Promise<PowerListItem[]> {
  const queryParams = queryString.stringify(params || {}, {
    skipNull: true,
    skipEmptyString: true,
  });
  const url = `${BASE_URL}/api/power/${queryParams ? `?${queryParams}` : ""}`;
  const response = await customFetch(url);
  return response;
}

export async function getPower(powerId: number): Promise<Power> {
  const response = await customFetch(`${BASE_URL}/api/power/${powerId}`);
  return response;
}

export async function postPower(powerDto: Power): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(powerDto),
  };

  return await customFetch(`${BASE_URL}/api/power`, options);
}

export async function updatePower(powerDto: Power): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(powerDto),
  };
  await customFetch(`${BASE_URL}/api/power`, options);
  return;
}
export async function deletePower(powerId: number): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(`${BASE_URL}/api/power/${powerId}`, options);
  return;
}

export async function addEffectBlueprint(
  effectBlueprintDto: EffectBlueprint,
  powerId: number
): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(effectBlueprintDto),
  };

  return await customFetch(`${BASE_URL}/api/power/${powerId}/effects`, options);
}

export async function getMaterialComponents(
  powerId: number
): Promise<MaterialComponent[]> {
  const response = await customFetch(
    `${BASE_URL}/api/power/${powerId}/materialComponents`
  );

  return response;
}

export async function getMaterialComponent(
  componentId: number
): Promise<MaterialComponent[]> {
  const response = await customFetch(
    `${BASE_URL}/api/power/materialComponent/${componentId}`
  );

  return response;
}
export async function addMaterialComponent(
  materialResourceDto: MaterialComponent,
  powerId: number
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(materialResourceDto),
  };
  await customFetch(
    `${BASE_URL}/api/power/${powerId}/materialComponents`,
    options
  );
  return;
}
export async function deleteMaterialComponent(
  componentId: number
): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(
    `${BASE_URL}/api/power/materialComponent/${componentId}`,
    options
  );
  return;
}

export async function updateMaterialComponent(
  materialResourceDto: MaterialComponent,
  powerId: number
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(materialResourceDto),
  };
  await customFetch(
    `${BASE_URL}/api/power/${powerId}/materialComponents`,
    options
  );
  return;
}
export async function getItemFamilies(
  powerId: number | null
): Promise<ItemFamily[]> {
  const response = await customFetch(
    `${BASE_URL}/api/power/itemFamilies${
      !!powerId ? `?effectId=${powerId}` : ""
    }`
  );

  return response;
}
