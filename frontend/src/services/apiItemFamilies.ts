import { ItemFamily } from "../models/itemfamily";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getItemFamilies(): Promise<ItemFamily[]> {
  const response = await customFetch(`${BASE_URL}/api/itemFamily`);

  return response;
}

export async function getItemFamily(itemFamilyId: number): Promise<ItemFamily> {
  const response = await customFetch(
    `${BASE_URL}/api/itemFamily/${itemFamilyId}`
  );
  return response;
}

export async function postItemFamily(itemFamily: ItemFamily): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(itemFamily),
  };
  return await customFetch(`${BASE_URL}/api/itemFamily`, options);
}

export async function updateItemFamily(itemFamily: ItemFamily): Promise<void> {
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(itemFamily),
  };
  await customFetch(`${BASE_URL}/api/itemFamily`, options);
  return;
}

export async function deleteItemFamily(itemId: number): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  };
  await customFetch(`${BASE_URL}/api/itemFamily/${itemId}`, options);
  return;
}
