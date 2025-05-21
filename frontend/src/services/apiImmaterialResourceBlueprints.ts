import {
  ImmaterialResourceBlueprint,
  ImmaterialResourceBlueprintWithOwner,
} from "../models/immaterialResourceBlueprint";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getImmaterialResourceBlueprints(): Promise<
  ImmaterialResourceBlueprintWithOwner[]
> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: ImmaterialResourceBlueprintWithOwner[] = await customFetch(
    `${BASE_URL}/api/immaterialResourceBlueprint`,
    options
  );

  return data;
}
export async function getImmaterialResourceBlueprint(
  immaterialResourceBlueprintId: number
): Promise<ImmaterialResourceBlueprintWithOwner> {
  const response = await customFetch(
    `${BASE_URL}/api/immaterialResourceBlueprint/${immaterialResourceBlueprintId}`
  );
  return response;
}

export async function postImmaterialResourceBlueprint(
  immaterialResourceBlueprint: ImmaterialResourceBlueprint
): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(immaterialResourceBlueprint),
  };
  return await customFetch(`${BASE_URL}/api/immaterialResourceBlueprint`, options);
}

export async function updateImmaterialResourceBlueprint(
  immaterialResourceBlueprint: ImmaterialResourceBlueprint
): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(immaterialResourceBlueprint),
  };
  await customFetch(`${BASE_URL}/api/immaterialResourceBlueprint`, options);
  return;
}

export async function deleteImmaterialResourceBlueprint(
  immaterialResourceBlueprintId: number
): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(
    `${BASE_URL}/api/immaterialResourceBlueprint/${immaterialResourceBlueprintId}`,
    options
  );
  return;
}
