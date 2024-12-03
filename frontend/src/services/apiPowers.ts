import { EffectBlueprint } from "../features/effects/EffectBlueprintForm";
import { MaterialComponent, Power } from "../features/powers/models/power";
import { ImmaterialResourceBlueprint } from "../models/immaterialResourceBlueprint";
import { PowerListItem } from "../models/power";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getImmaterialResourceBlueprints(): Promise<
  ImmaterialResourceBlueprint[]
> {
  const response = await customFetch(
    `${BASE_URL}/api/power/immaterialResourceBlueprints`
  );

  console.log(response);

  return response;
}

export async function getPowers(): Promise<PowerListItem[]> {
  const response = await customFetch(`${BASE_URL}/api/power/`);

  console.log(response);

  return response;
}

export async function getPower(powerId: number): Promise<Power> {
  const response = await customFetch(`${BASE_URL}/api/power/${powerId}`);
  return response;
}

export async function postPower(powerDto: Power): Promise<void> {
  console.log(powerDto);
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(powerDto),
  };
  await customFetch(`${BASE_URL}/api/power`, options);
  return;
}

export async function updatePower(powerDto: Power): Promise<void> {
  console.log(powerDto);
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

export async function addEffectBlueprint(
  effectBlueprintDto: EffectBlueprint,
  powerId: number
): Promise<void> {
  console.log(effectBlueprintDto);
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(effectBlueprintDto),
  };
  await customFetch(`${BASE_URL}/api/power/${powerId}/effects`, options);
  return;
}

export async function getMaterialComponents(
  powerId: number
): Promise<MaterialComponent[]> {
  const response = await customFetch(
    `${BASE_URL}/api/power/${powerId}/materialComponents`
  );

  console.log(response);

  return response;
}

export async function getMaterialComponent(
  componentId: number
): Promise<MaterialComponent[]> {
  const response = await customFetch(
    `${BASE_URL}/api/power/materialComponent/${componentId}`
  );

  console.log(response);

  return response;
}
export async function addMaterialComponent(
  materialResourceDto: MaterialComponent,
  powerId: number
): Promise<void> {
  console.log(materialResourceDto);
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
  console.log(materialResourceDto);
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
