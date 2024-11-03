import { CharacterClass } from "../models/characterclass";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getClasses(): Promise<CharacterClass[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: CharacterClass[] = await customFetch(
    `${BASE_URL}/api/class`,
    options
  );

  return data;
}
