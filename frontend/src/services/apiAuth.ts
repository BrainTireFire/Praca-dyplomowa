import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function login(loginDto: LoginDto): Promise<UserDto> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(loginDto),
  };

  const data: UserDto = await customFetch(
    `${BASE_URL}/api/account/login`,
    options
  );
  return data;
}

export async function signup(registerDto: RegisterDto): Promise<UserDto> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(registerDto),
  };

  const data: UserDto = await customFetch(
    `${BASE_URL}/api/account/register`,
    options
  );
  return data;
}

export async function logout(): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    // headers: {
    //   "Content-Type": "application/json",
    // },
  };

  await customFetch(`${BASE_URL}/api/account/logout`, options);
}

export async function getCurrentUser(): Promise<ValidateAuthDto> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: ValidateAuthDto = await customFetch(
    `${BASE_URL}/api/account/current-user`,
    options
  );
  return data;
}
