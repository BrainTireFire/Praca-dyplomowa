export async function getCampaigns(): Promise<Campaign[]> {
  const response = await fetch("http://localhost:3000/campaigns");

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const data: Campaign[] = await response.json();

  return data;
}

export async function getCampaign(campaignId: string): Promise<Campaign> {
  const response = await fetch(`http://localhost:3000/campaigns/${campaignId}`);

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const campaign: Campaign = await response.json();
  return campaign;
}
