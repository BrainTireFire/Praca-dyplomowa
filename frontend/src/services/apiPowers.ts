import { Power } from "../features/powers/models/power";
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
