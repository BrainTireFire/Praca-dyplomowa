import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getCampaigns(): Promise<Campaign[]> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(`${BASE_URL}/api/campaign`, options);

  return data;
}

export async function getCampaign(campaignId: number): Promise<Campaign> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(
    `${BASE_URL}/api/campaign/${campaignId}`,
    options
  );

  return data;
}

export async function postCampaign(
  campaignDto: CampaignInsertDto
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(campaignDto),
  };
  await customFetch(`${BASE_URL}/api/campaign`, options);
}

export async function addCharacterToCampaign(
  campaignId: number,
  characterId: number
): Promise<void> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };

  await customFetch(
    `${BASE_URL}/api/campaign/addCharacterToCampaign/${campaignId}/${characterId}`,
    options
  );
  return;
}
