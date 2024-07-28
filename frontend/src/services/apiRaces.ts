import { Race } from "../models/race";

export async function getRaces(): Promise<Race[]> {
  const response = await fetch("http://localhost:5000/api/race");

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const data: Race[] = await response.json();

  return data;
}
