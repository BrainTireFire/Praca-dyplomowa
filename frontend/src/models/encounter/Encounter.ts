import { Campaign } from "../campaign";
import { Board } from "../map/Board";
import { ParticipanceDataDto } from "./ParticipanceDataDto";

export type Encounter = {
  id: number;
  name: string;
  isActive: boolean;
  board: Board;
  campaign: Campaign;
  participances: ParticipanceDataDto[];
  amIGameMaster: boolean;
};
