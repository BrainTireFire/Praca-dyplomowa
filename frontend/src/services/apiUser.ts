import { UpdateUserDto } from "../models/account/updateUserDto";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function updateEmail(updateUserDto: UpdateUserDto): Promise<null> {
  const options: RequestInit = {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(updateUserDto),
  };

  await customFetch(`${BASE_URL}/api/user/edit/email`, options);

  return null;
}

export async function updatePassword(
  updateUserDto: UpdateUserDto
): Promise<null> {
  const options: RequestInit = {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(updateUserDto),
  };

  await customFetch(`${BASE_URL}/api/user/edit/password`, options);

  return null;
}

export async function updateUsername(
  updateUserDto: UpdateUserDto
): Promise<null> {
  const options: RequestInit = {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(updateUserDto),
  };

  await customFetch(`${BASE_URL}/api/user/edit/username`, options);

  return null;
}
