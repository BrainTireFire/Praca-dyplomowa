import { CharacterItem } from "./character";
import { Shop } from "./shop";

export type Campaign = {
  id: number;
  name: string;
  description: string;
  gameMaster: string;
  members: CharacterItem[];
  shops: Shop[];
  isGameMaster: boolean;
};

export type CampaignInsertDto = {
  name: string;
  description: string;
};
