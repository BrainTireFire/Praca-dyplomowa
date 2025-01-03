import { Campaign } from "../campaign";
import { Board } from "../map/Board";

export type Encounter = {
  id: number;
  name: string;
  board: Board;
  campaign: Campaign;
  participances: number[];
};
