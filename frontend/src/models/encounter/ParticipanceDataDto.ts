import { Field } from "react-hook-form";
import { CharacterItem } from "../character";

export type ParticipanceDataDto = {
  id: number;
  initiativeOrder: number;
  isSurprised: boolean;
  numberOfActionsTaken: number;
  numberOfBonusActionsTaken: number;
  numberOfAttacksTaken: number;
  distanceTraveled: number;
  character: CharacterItem;
  occupiedField: Field;
};
