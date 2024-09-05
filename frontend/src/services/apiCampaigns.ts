import { BASE_URL, BASE_URL_JSON_SERVER } from "./constAPI";
import { customFetch } from "./customFetch";
import { customFetchJSON } from "./customFetchJSON";

export async function getCampaigns(): Promise<Campaign[]> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(`${BASE_URL}/api/campaigns`, options);

  return data;
}

export async function getCampaign(campaignId: number): Promise<Campaign> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(
    `${BASE_URL}/api/campaigns/${campaignId}`,
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
  await customFetch(`${BASE_URL}/api/campaigns`, options);
}
