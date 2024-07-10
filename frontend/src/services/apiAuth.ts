import { BASE_URL } from "./constAPI";

type LoginDto = {
  username: string;
  password: string;
};

type UserDto = {
  username: string;
  token: string;
};

export async function login(loginDto: LoginDto): Promise<UserDto> {
  const response = await fetch(`${BASE_URL}/api/account/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(loginDto),
    credentials: "include",
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || "Unauthorized");
  }

  const data: UserDto = await response.json();
  return data;
}

export async function validateToken(): Promise<{ isValid: boolean }> {
  const response = await fetch(`${BASE_URL}/api/account/validate-token`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error("Invalid token");
  }

  return response.json();
}
