import { HubConnection } from "@microsoft/signalr";

export type VirtualBoardProps = {
  connection: HubConnection | null;
  groupName: string | undefined;
};
