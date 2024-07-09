export async function getCampaigns(): Promise<Campaigns[]> {
  const response = await fetch("http://localhost:3000/campaigns");

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const data: Campaigns[] = await response.json();

  return data;
}

export async function getCampaign(campaignId: string): Promise<Campaigns> {
  const response = await fetch(`http://localhost:3000/campaigns/${campaignId}`);

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const campaign: Campaigns = await response.json();
  return campaign;
}
