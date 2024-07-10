import { BASE_URL, BASE_URL_JSON_SERVER } from "./constAPI";

export async function getCampaigns(): Promise<Campaign[]> {
  const response = await fetch(`${BASE_URL_JSON_SERVER}campaigns`);

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const data: Campaign[] = await response.json();

  return data;
}

export async function getCampaign(campaignId: string): Promise<Campaign> {
  const response = await fetch(
    `${BASE_URL_JSON_SERVER}/campaigns/${campaignId}`
  );

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const campaign: Campaign = await response.json();
  return campaign;
}
