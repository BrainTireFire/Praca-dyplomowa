import { ItemFamily } from "../models/itemfamily";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getItemFamilies(): Promise<ItemFamily[]> {
  const response = await customFetch(`${BASE_URL}/api/effect/itemFamilies`);

  console.log(response);

  return response;
}
