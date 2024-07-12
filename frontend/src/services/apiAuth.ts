import { BASE_URL } from "./constAPI";

type LoginDto = {
  username: string;
  password: string;
};

type RegisterDto = {
  username: string;
  email: string;
  password: string;
};

type UserDto = {
  username: string;
  token: string;
};

type ValidateAuthDto = {
  IsAuthenticated: boolean;
  roles: string[];
  username: string;
  email: string;
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

export async function signup(registerDto: RegisterDto): Promise<UserDto> {
  const response = await fetch(`${BASE_URL}/api/account/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(registerDto),
    credentials: "include",
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || "Unauthorized");
  }

  const data: UserDto = await response.json();
  return data;
}

export async function logout() {
  const response = await fetch(`${BASE_URL}/api/account/logout`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || "Unauthorized");
  }
}

export async function validateToken(): Promise<ValidateAuthDto> {
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
