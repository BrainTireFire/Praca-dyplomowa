import { PowerListItem } from "../power";

export type BoardUpdateDto = {
  name: string;
  description: string;
  sizeX: number;
  sizeY: number;
  fields: FieldUpdateDto[];
};

export type FieldUpdateDto = {
  id: number;
  positionX: number;
  positionY: number;
  positionZ: number;
  color: string;
  description: string;
  fieldCoverLevel: string;
  fieldMovementCost: string;
  powers: PowerListItem[];
};
