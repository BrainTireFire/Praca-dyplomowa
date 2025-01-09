export type Shop = {
  id: number;
  name: string;
  type: string;
  location: string;
  description: string;
};

export type ShopInsertDto = {
  name: string;
  type: string;
  location: string;
  description: string;
  campaignId: number;
};
