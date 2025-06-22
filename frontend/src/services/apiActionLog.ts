import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

type ActionLogDto = {
  content: string;
  source: string;
};

export async function getActionLogsByEncounterId(
  encounterId: number
): Promise<ActionLogDto[]> {
  const options: RequestInit = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data: ActionLogDto[] = await customFetch(
    `${BASE_URL}/api/actionlog/${encounterId}`,
    options
  );

  return data;
}
