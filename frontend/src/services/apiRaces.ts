import { Race } from "../models/race";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getRaces(): Promise<Race[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: Race[] = await customFetch(`${BASE_URL}/api/race`, options);

  return data;
}
