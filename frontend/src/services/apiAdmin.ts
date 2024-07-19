import { BASE_URL } from "./constAPI";

type UserWithRoleDto = {
  roles: string[];
  username: string;
  id: number;
};

export async function getUsersWithRoles(): Promise<UserWithRoleDto> {
  const response = await fetch(`${BASE_URL}/api/admin/users-with-roles`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
  });

  if (response.status === 401) {
    throw new Error("Unauthorized");
  }

  if (response.status === 403) {
    throw new Error("Forbidden");
  }

  if (!response.ok) {
    throw new Error(`Error: ${response.status}`);
  }

  return response.json();
}
