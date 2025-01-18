import { HubConnection } from "@microsoft/signalr";
import { Encounter } from "../encounter/Encounter";
import {
  ControlStateActions,
  Mode,
} from "../../features/campaigns/session/SessionLayout";

export type VirtualBoardProps = {
  connection: HubConnection | null;
  groupName: string | undefined;
  encounter: Encounter;
  mode: Mode;
  dispatch: React.Dispatch<ControlStateActions>;
  path: number[];
  otherPath: number[];
  weaponAttack: WeaponAttack;
};

export type WeaponAttack = {
  weaponId: number;
  range: number;
  isRanged: boolean;
};
