export type Board = {
  id: number;
  name: string;
  description: string;
  sizeX: number;
  sizeY: number;
  fields: Field[];
};

export type Field = {
  id: number;
  positionX: number;
  positionY: number;
  positionZ: number;
  color: string;
  description: string;
  fieldCoverLevel: string;
  fieldMovementCost: string;
};