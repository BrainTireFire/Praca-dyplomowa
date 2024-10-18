type Campaign = {
  id: number;
  name: string;
  description: string;
  gameMaster: GameMaster;
  members: Member[];
  shops: Shop[];
};

type CampaignInsertDto = {
  name: string;
  description: string;
};
