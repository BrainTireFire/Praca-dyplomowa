import { ImmaterialResourceBlueprint } from "../models/immaterialResourceBlueprint";
import { Race } from "../models/race";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getImmaterialResourceBlueprints(): Promise<
  ImmaterialResourceBlueprint[]
> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: ImmaterialResourceBlueprint[] = await customFetch(
    `${BASE_URL}/api/immaterialResourceBlueprint`,
    options
  );

  return data;
}
