import { BASE_URL, BASE_URL_JSON_SERVER } from "./constAPI";
import { customFetchJSON } from "./customFetchJSON";

export async function getCampaigns(): Promise<Campaign[]> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetchJSON(
    `${BASE_URL_JSON_SERVER}/campaigns`,
    options
  );

  return data;
}

export async function getCampaign(campaignId: string): Promise<Campaign> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetchJSON(
    `${BASE_URL_JSON_SERVER}/campaigns/${campaignId}`,
    options
  );

  return data;
}
