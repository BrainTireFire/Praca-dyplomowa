import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getDice(): Promise<Dice[]> {
  const options: RequestInit = {
    method: "GET",
  };

  const data: Dice[] = await customFetch(`${BASE_URL}/dice`, options);

  return data;
}

export async function getDiceById(diceId: string): Promise<Dice> {
  const options: RequestInit = {
    method: "GET",
  };

  const data: Dice = await customFetch(`${BASE_URL}/dice/${diceId}`, options);

  return data;
}
