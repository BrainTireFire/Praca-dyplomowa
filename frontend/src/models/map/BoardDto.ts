export type BoardCreateDto = {
  name: string;
  description: string;
  sizeX: number;
  sizeY: number;
  fields: FieldCreateDto[];
};

export type FieldCreateDto = {
  positionX: number;
  positionY: number;
  positionZ: number;
  color: string;
  description: string;
  fieldCoverLevel: string;
  fieldMovementCost: string;
};
