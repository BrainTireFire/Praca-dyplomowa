import { CharacterItem } from "./character";
import { Shop } from "./shop";

export type Campaign = {
  id: number;
  name: string;
  description: string;
  gameMaster: GameMaster;
  members: CharacterItem[];
  shops: Shop[];
};

export type CampaignInsertDto = {
  name: string;
  description: string;
};
