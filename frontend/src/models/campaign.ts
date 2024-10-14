type Campaign = {
  id: number;
  name: string;
  description: string;
  gameMaster: GameMaster;
  members: Member[];
  shops: Shop[];
  invitationLink: string;
};

type CampaignInsertDto = {
  name: string;
  description: string;
  invitationLink: string;
};
