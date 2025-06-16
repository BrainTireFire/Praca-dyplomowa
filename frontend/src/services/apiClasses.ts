import { CharacterClass } from "../models/characterclass";
import { FirstClass } from "../models/firstClass";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getClasses(): Promise<FirstClass[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: FirstClass[] = await customFetch(
    `${BASE_URL}/api/class`,
    options
  );

  return data;
}
