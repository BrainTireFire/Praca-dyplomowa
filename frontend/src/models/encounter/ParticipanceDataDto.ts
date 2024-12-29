import { Field } from "react-hook-form";
import { CharacterItem } from "../character";

export type ParticipanceDataDto = {
  initiativeOrder: number;
  isSurprised: boolean;
  numberOfActionsTaken: number;
  numberOfBonusActionsTaken: number;
  numberOfAttacksTaken: number;
  distanceTraveled: number;
  character: CharacterItem;
  occupiedFields: Field[];
};
