import { Slot } from "../models/slot";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getSlots(): Promise<Slot[]> {
  const response = await customFetch(`${BASE_URL}/api/equipmentSlot/`);

  return response;
}
