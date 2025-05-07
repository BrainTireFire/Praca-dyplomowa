import { Campaign, CampaignInsertDto } from "../models/campaign";
import { Character } from "../models/character";
import { DiceSet } from "../models/diceset";
import { BASE_URL } from "./constAPI";
import { customFetch } from "./customFetch";

export async function getCampaigns(): Promise<Campaign[]> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(`${BASE_URL}/api/campaign`, options);

  return data;
}

export async function getCampaignsAttend(): Promise<Campaign[]> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(
    `${BASE_URL}/api/campaign/attendCampaigns`,
    options
  );

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

export async function getCampaignJoinInfo(
  campaignId: number
): Promise<Campaign> {
  const options: RequestInit = {
    method: "GET",
  };

  const data = await customFetch(
    `${BASE_URL}/api/campaign/joinInfo/${campaignId}`,
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

export async function getMyCharacter(campaignId: number): Promise<Character> {
  const options: RequestInit = {
    method: "GET",
  };
  const response = await customFetch(
    `${BASE_URL}/api/campaign/${campaignId}/myCharacter`,
    options
  );

  return response;
}

export async function longRest(campaignId: number): Promise<Response> {
  const options: RequestInit = {
    method: "PATCH",
  };

  return await customFetch(
    `${BASE_URL}/api/campaign/${campaignId}/longRest`,
    options
  );
}

export async function getHitDice(campaignId: number): Promise<DiceSet> {
  const options: RequestInit = {
    method: "GET",
  };

  return await customFetch(
    `${BASE_URL}/api/campaign/${campaignId}/hitDice`,
    options
  );
}

export async function performShortRest(campaignId: number, hitDiceMap: Record<number, DiceSet>) : Promise<Response> {
  console.log(hitDiceMap);
  const options: RequestInit = {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(hitDiceMap),
  };
  return await customFetch(
    `${BASE_URL}/api/campaign/${campaignId}/shortRest`,
    options
  );
}