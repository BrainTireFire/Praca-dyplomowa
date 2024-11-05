import { DiceSet } from "../models/diceset";
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

export async function rollDice(DiceSet: DiceSet): Promise<DiceSet> {
  const options: RequestInit = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(DiceSet),
  };

  const result: DiceSet = await customFetch(`${BASE_URL}/roll-dice`, options);
  return result;
}
