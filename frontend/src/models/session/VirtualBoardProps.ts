import { HubConnection } from "@microsoft/signalr";
import { Encounter } from "../encounter/Encounter";
import {
  ControlStateActions,
  Mode,
} from "../../features/campaigns/session/SessionLayout";
import { AreaShape } from "../../features/powers/models/power";

export type VirtualBoardProps = {
  connection: HubConnection | null;
  groupName: string | undefined;
  encounter: Encounter;
  mode: Mode;
  dispatch: React.Dispatch<ControlStateActions>;
  path: number[];
  otherPath: number[];
  weaponAttack: WeaponAttack;
  power: Power;
};

export type WeaponAttack = {
  weaponId: number;
  range: number;
  isRanged: boolean;
};

export type Power = {
  powerId: number;
  range: number | null;
  maxTargets: number | null;
  areaShape: AreaShape | null;
  areaSize: number | null;
};
