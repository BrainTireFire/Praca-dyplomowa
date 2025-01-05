import { Campaign } from "../campaign";
import { Board } from "../map/Board";
import { ParticipanceDataDto } from "./ParticipanceDataDto";

export type Encounter = {
  id: number;
  name: string;
  board: Board;
  campaign: Campaign;
  participances: ParticipanceDataDto[];
};
