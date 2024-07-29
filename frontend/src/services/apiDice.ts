import { BASE_URL, BASE_URL_JSON_SERVER } from "./constAPI";
import { customFetchJSON } from "./customFetchJSON";

export async function getDice(): Promise<Dice[]> {
  const options: RequestInit = {
    method: "GET",
  };

  const data: Dice[] = await customFetchJSON(
    `${BASE_URL_JSON_SERVER}/dice`,
    options
  );

  return data;
}

export async function getDiceById(diceId: string): Promise<Dice> {
  const options: RequestInit = {
    method: "GET",
  };

  const data: Dice = await customFetchJSON(
    `${BASE_URL_JSON_SERVER}/dice/${diceId}`,
    options
  );

  return data;
}
