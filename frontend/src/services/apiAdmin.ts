import { UserWithRoleDto } from "../models/account/userWithRoleDto";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getUsersWithRoles(): Promise<UserWithRoleDto> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: UserWithRoleDto = await customFetch(
    `${BASE_URL}/api/admin/users-with-roles`,
    options
  );

  return data;
}
