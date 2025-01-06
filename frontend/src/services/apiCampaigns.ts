import { Campaign, CampaignInsertDto } from "../models/campaign";
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
): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(campaignDto),
  };
  const data = await customFetch(`${BASE_URL}/api/campaign`, options);
  return Number(data);
}

export async function removeCampaign(campaignId: number): Promise<void> {
  const options: RequestInit = {
    method: "DELETE",
  };
  await customFetch(`${BASE_URL}/api/campaign/${campaignId}`, options);
}

export async function addCharacterToCampaign(
  campaignId: number,
  characterId: number
): Promise<number> {
  const options: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };

  const data = await customFetch(
    `${BASE_URL}/api/campaign/addCharacterToCampaign/${campaignId}/${characterId}`,
    options
  );

  return Number(data);
}

export async function removeCharacterFromCampaign(
  characterId: number
): Promise<Response> {
  const options: RequestInit = {
    method: "DELETE",
  };

  return await customFetch(
    `${BASE_URL}/api/campaign/removeCharacterFromCampaign/${characterId}`,
    options
  );
}
