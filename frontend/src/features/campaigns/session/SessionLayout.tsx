import React, { useEffect, useReducer, useRef, useState } from "react";
import styled from "styled-components";
import VirtualBoard from "./VirtualBoard";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Heading from "../../../ui/text/Heading";
import { useParams } from "react-router-dom";
import { BASE_URL } from "../../../services/constAPI";
import ActionBar from "./ActionBar";
import { useQueryClient } from "@tanstack/react-query";
import { WeaponAttack, Power } from "../../../models/session/VirtualBoardProps";
import ModalNoButton from "../../../ui/containers/ModalNoButton";
import { WeaponAttackResolution } from "./WeaponAttackResolution";
import { PowerCastResolution } from "./PowerCastResolution";
import { WeaponAttackOverlayDto } from "../../../models/encounter/WeaponAttackOverlayDto";
import toast from "react-hot-toast";
import { useActionLogs } from "../hooks/useActionLogs";

const GridContainer = styled.div`
  grid-row: 1 / 2;
  grid-column: 1 / 2;
  justify-content: center;
  align-items: center;
  height: auto;
`;

const RightPanel = styled.div`
  grid-row: 1 / 4;
  grid-column: 2 / 3;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  height: 100%;
  overflow-y: hidden;
`;

const BottomPanel = styled.div`
  grid-row: 2 / 3;
  grid-column: 1 / 2;
  display: flex;
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  height: auto;
`;
const BottomPanel2 = styled.div`
  grid-row: 3 / 4;
  grid-column: 1 / 2;
  height: auto;
`;

const UsersList = styled.div`
  margin: 10px;
  padding: 10px;
  background-color: var(--color-navbar);
  border: 1px solid #ddd;
  border-radius: 5px;
`;

const ChatContentMessage = styled.div`
  flex-grow: 1;
  overflow-y: auto;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  scrollbar-gutter: stable;
`;

const ChatMessageBox = styled.div`
  display: flex;
  flex-direction: column;
  padding: 10px;
  border-bottom: 1px solid var(--color-border);
`;

const ChatForm = styled.form`
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-top: 1px solid var(--color-border);
  padding: 10px;
  margin-top: auto; /* Ensure the form is pushed to the bottom */

  & > input[type="text"] {
    flex: 0 0 75%;
    margin-right: 10px;
  }

  & > button {
    flex: 0 0 15%;
    margin-right: 10px;
  }
`;

export type Mode =
  | "Idle"
  | "Movement"
  | "WeaponAttack"
  | "PowerCast"
  | "WeaponAttackOverlay"
  | "PowerCastOverlay";

export type ControlState = {
  mode: Mode;
  path: number[];
  weaponAttackSelected: WeaponAttack | null;
  powerSelected: Power | null;
  weaponAttackRollOverlayData: WeaponAttackOverlayData | null;
  powerCastOverlayData: PowerCastOverlayData | null;
  powerTargets: number[];
};

// Define action types
const CHANGE_MODE = "CHANGE_MODE";
const APPEND_PATH = "APPEND_PATH";
const POP_PATH = "POP_PATH";
const TOGGLE_PATH = "TOGGLE_PATH";
const SET_PATH = "SET_PATH";
const SET_WEAPON_ATTACK = "SET_WEAPON_ATTACK";
const SET_POWER = "SET_POWER";
const SET_POWER_ID = "SET_POWER_ID";
const SET_SELECTED_POWER_DATA = "SET_SELECTED_POWER_DATA";
const TOGGLE_POWER_TARGET = "TOGGLE_POWER_TARGET";
const TOGGLE_POWER_TARGET_ARRAY = "TOGGLE_POWER_TARGET_ARRAY";
const WEAPON_ATTACK_OVERLAY_DATA = "WEAPON_ATTACK_OVERLAY_DATA";
const POWER_CAST_OVERLAY_DATA = "POWER_CAST_OVERLAY_DATA";

// Define action interfaces
export interface ChangeModeAction {
  type: typeof CHANGE_MODE;
  payload: Mode;
}

interface AppendPathAction {
  type: typeof APPEND_PATH;
  payload: number;
}

interface PopPathAction {
  type: typeof POP_PATH;
  payload: number;
}

interface TogglePathAction {
  type: typeof TOGGLE_PATH;
  payload: number;
}

interface SetPathAction {
  type: typeof SET_PATH;
  payload: number[];
}
export interface SetWeaponAttack {
  type: typeof SET_WEAPON_ATTACK;
  payload: WeaponAttack;
}
export interface SetPower {
  type: typeof SET_POWER;
  payload: Power;
}
export interface SetPowerId {
  type: typeof SET_POWER_ID;
  payload: number;
}
export interface SetSelectedPowerData {
  type: typeof SET_SELECTED_POWER_DATA;
  payload: {
    powerId: number;
    powerLevelSelected: number | null;
    resourceLevelSelected: null;
  };
}
export interface WeaponAttackOverlay {
  type: typeof WEAPON_ATTACK_OVERLAY_DATA;
  payload: WeaponAttackOverlayData;
}
export interface TogglePowerTarget {
  type: typeof TOGGLE_POWER_TARGET;
  payload: number; // characterId
}
export interface TogglePowerTargetArray {
  type: typeof TOGGLE_POWER_TARGET_ARRAY;
  payload: number[]; // characterId
}
export interface PowerCastOverlay {
  type: typeof POWER_CAST_OVERLAY_DATA;
  payload: PowerCastOverlayData;
}

type WeaponAttackOverlayData = {
  targetId: number;
  sourceId: number;
};
type PowerCastOverlayData = {
  targetIds: number[];
  sourceId: number;
};

// Union type for all actions
export type ControlStateActions =
  | ChangeModeAction
  | AppendPathAction
  | PopPathAction
  | TogglePathAction
  | SetPathAction
  | SetWeaponAttack
  | SetPower
  | WeaponAttackOverlay
  | TogglePowerTarget
  | TogglePowerTargetArray
  | PowerCastOverlay
  | SetPowerId
  | SetSelectedPowerData;

// Reducer function
const controlStateReducer = (
  state: ControlState,
  action: ControlStateActions
): ControlState => {
  switch (action.type) {
    case CHANGE_MODE:
      return {
        ...state,
        mode: action.payload,
        path: action.payload !== "Movement" ? [] : state.path,
        weaponAttackSelected:
          action.payload !== "WeaponAttack" ? null : state.weaponAttackSelected,
        powerSelected:
          action.payload !== "PowerCast" ? null : state.powerSelected,
        powerTargets: [],
      };
    case APPEND_PATH:
      return {
        ...state,
        path: [...state.path, action.payload],
      };
    case POP_PATH:
      const index = state.path.indexOf(action.payload);
      return {
        ...state,
        path: index !== -1 ? state.path.slice(0, index + 1) : state.path,
      };
    case TOGGLE_PATH:
      if (state.path.includes(action.payload)) {
        const idx = state.path.indexOf(action.payload);
        return {
          ...state,
          path: idx !== -1 ? state.path.slice(0, idx + 1) : state.path,
        };
      } else {
        return {
          ...state,
          path: [...state.path, action.payload],
        };
      }
    case SET_PATH:
      return {
        ...state,
        path: action.payload,
      };
    case SET_WEAPON_ATTACK:
      return {
        ...state,
        weaponAttackSelected: action.payload,
        mode: "WeaponAttack",
      };
    case SET_POWER:
      return {
        ...state,
        powerSelected: action.payload,
        mode: "PowerCast",
      };
    case SET_POWER_ID:
      return {
        ...state,
        powerSelected: {
          ...state.powerSelected,
          powerId: action.payload,
        },
        mode: "PowerCast",
      };
    case SET_SELECTED_POWER_DATA:
      return {
        ...state,
        powerSelected: {
          ...state.powerSelected,
          powerId: action.payload.powerId,
          chosenLevel:
            action.payload.powerLevelSelected !== null &&
            action.payload.resourceLevelSelected !== null
              ? {
                  powerLevel: action.payload.powerLevelSelected,
                  resourceLevel: action.payload.resourceLevelSelected,
                }
              : null,
        },
        mode: "PowerCast",
      };
    case TOGGLE_POWER_TARGET_ARRAY: {
      const idsToToggle = action.payload as number[];
      const maxTargets = state.powerSelected?.maxTargets ?? 1;

      const limitedIds =
        state.powerSelected?.areaShape !== "None"
          ? idsToToggle
          : idsToToggle.slice(0, maxTargets);

      return {
        ...state,
        powerTargets: limitedIds,
      };
    }
    case TOGGLE_POWER_TARGET:
      const foundIndex = state.powerTargets.findIndex(
        (x) => x === action.payload
      );
      if (foundIndex === -1) {
        let newPowerTargets = [...state.powerTargets];
        newPowerTargets.push(action.payload);
        if (
          newPowerTargets.length >
          (state.powerSelected?.maxTargets
            ? state.powerSelected?.maxTargets
            : 1)
        ) {
          return state;
        }
        return {
          ...state,
          powerTargets: newPowerTargets,
        };
      } else {
        return {
          ...state,
          powerTargets: state.powerTargets.filter((x) => x !== action.payload),
        };
      }
    case WEAPON_ATTACK_OVERLAY_DATA:
      return {
        ...state,
        weaponAttackRollOverlayData: action.payload,
        mode: "WeaponAttackOverlay",
      };
    case POWER_CAST_OVERLAY_DATA:
      return {
        ...state,
        powerCastOverlayData: action.payload,
        mode: "PowerCastOverlay",
      };
    default:
      return state;
  }
};

// initial state
const initialState: ControlState = {
  mode: "Idle",
  path: [],
  weaponAttackSelected: null,
  powerSelected: null,
  weaponAttackRollOverlayData: null,
  powerCastOverlayData: null,
  powerTargets: [],
};

export default function SessionLayout({ encounter }: any) {
  const { groupName } = useParams();
  const queryClient = useQueryClient();

  //TODO REACT QUERY OR STATE MANAGEMENT
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [usersConnected, setUsersConnected] = useState<string[]>([]);

  const { isLoading, actionLogs } = useActionLogs(encounter.id);

  const [messages, setMessages] = useState<
    { message: string; username: string }[]
  >([]);

  const [messageInput, setMessageInput] = useState<string>("");

  const [controlState, dispatch] = useReducer(
    controlStateReducer,
    initialState
  );
  const [otherPath, setOtherPath] = useState<number[]>([]);

  useEffect(() => {
    if (actionLogs && actionLogs.length > 0 && !isLoading) {
      const formattedLogs = actionLogs.map((log: any) => ({
        message: log.content,
        username: log.source,
      }));
      setMessages(formattedLogs);
    }
  }, [actionLogs, isLoading]);

  const chatRef = useRef(null);
  useEffect(() => {
    // Scroll to the bottom when messages change
    if (chatRef.current) {
      chatRef.current.scrollTop = chatRef.current.scrollHeight;
    }
  }, [messages]);

  useEffect(() => {
    if (groupName) {
      const hubConnection = new HubConnectionBuilder()
        .withUrl(
          `${BASE_URL}/session?groupName=${groupName}&campaignId=${encounter.campaign.id}`
        )
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();

      hubConnection
        .start()
        .then(() => {
          hubConnection
            .invoke("GetUsersInGroup", groupName)
            .then((userList: string[]) => {
              setUsersConnected(userList);
            })
            .catch((err) => {
              toast.error("Error fetching users in group");
            });

          hubConnection.on("UserJoined", (userName: string) => {
            setUsersConnected((prevUsers) => [...prevUsers, userName]);
          });

          hubConnection.on("UserLeft", (userName: string) => {
            setUsersConnected((prevUsers) =>
              prevUsers.filter((user) => user !== userName)
            );
          });

          hubConnection.on(
            "ReceiveMessage",
            (messageDto: { username: string; message: string }) => {
              setMessages((prevMessages) => [...prevMessages, messageDto]);
            }
          );

          hubConnection.on(
            "WeaponAttackOverlay",
            ({
              targetId,
              sourceId,
              weaponId,
              isRanged,
              range,
            }: WeaponAttackOverlayDto) => {
              dispatch({
                type: "SET_WEAPON_ATTACK",
                payload: {
                  weaponId: weaponId,
                  isRanged: isRanged,
                  range: range,
                },
              });

              dispatch({
                type: "WEAPON_ATTACK_OVERLAY_DATA",
                payload: { targetId: targetId, sourceId },
              });
            }
          );

          hubConnection.on(
            "PowerCastOverlay",
            ({
              sourceId,
              powerId,
              powerTargetIds,
              powerLevelSelected,
              resourceLevelSelected,
            }: any) => {
              dispatch({
                type: "SET_SELECTED_POWER_DATA",
                payload: { powerId, powerLevelSelected, resourceLevelSelected },
              });

              dispatch({
                type: "TOGGLE_POWER_TARGET",
                payload: powerTargetIds,
              });

              dispatch({
                type: "POWER_CAST_OVERLAY_DATA",
                payload: { targetIds: powerTargetIds, sourceId },
              });
            }
          );

          setConnection(hubConnection);
        })
        .catch((error) => {
          // toast.error("Error connecting to session hub: " + error);
        });

      return () => {
        if (hubConnection) {
          hubConnection.stop().then(() => {});
        }
      };
    } else {
      // toast.error("Group name is not defined");
    }
  }, [groupName, encounter.campaign.id]);

  const handleSendMessage = async (e: React.FormEvent) => {
    e.preventDefault();
    if (connection && groupName && messageInput.trim()) {
      var request = {
        groupName: groupName,
        content: messageInput,
        campaignId: encounter.campaign.id,
        encounterId: encounter.id,
      };

      try {
        await connection.invoke("SendMessageToGroup", request).catch((err) => {
          toast.error("Error while sending message");
        });
        setMessageInput("");
      } catch (error) {
        toast.error("Error sending message");
      }
    }
  };

  const handleWeaponAttackOverlay = async (
    targetId: number,
    sourceId: number,
    campaignId: number,
    weaponId: number,
    isRange: boolean,
    range: number
  ) => {
    if (connection) {
      try {
        const weaponAttackOverlayRequest = {
          targetId: targetId,
          sourceId: sourceId,
          campaignId: campaignId,
          weaponId: weaponId,
          isRange: isRange,
          range: range,
        };

        await connection
          .invoke("TriggerWeaponAttackOverlay", weaponAttackOverlayRequest)
          .catch((err) => {
            toast.error("Error while sending message");
          });
      } catch (error) {
        toast.error("Error sending message");
      }
    }
  };

  const handlePowerCastOverlay = async (
    sourceId: number,
    campaignId: number,
    powerTargetIds: number[],
    power: Power
  ) => {
    console.info("sourceId", sourceId);
    console.info("campaignId", campaignId);
    console.info("powerTargetIds", powerTargetIds);
    console.info("power", power);
    if (connection) {
      try {
        const powerCastOverlayRequest = {
          sourceId: sourceId,
          campaignId: campaignId,
          powerTargetIds: powerTargetIds,
          powerId: power.powerId,
          powerLevelSelected: power.chosenLevel?.powerLevel,
          resourceLevelSelected: power.chosenLevel?.resourceLevel,
        };

        await connection
          .invoke("TriggerPowerCastOverlay", powerCastOverlayRequest)
          .catch((err) => {
            toast.error("Error while sending message");
          });
      } catch (error) {
        toast.error("Error sending message");
      }
    }
  };

  useEffect(() => {
    if (connection) {
      connection.on("UpdatePath", (incomingPath: number[]) => {
        setOtherPath(incomingPath);
        dispatch({ type: "SET_PATH", payload: [] });
      });
      connection.on("CharacterMoved", (request) => {
        if (encounter.id !== request.encounterId) {
          return;
        }

        dispatch({ type: "SET_PATH", payload: [] });
        setOtherPath([]);

        queryClient.invalidateQueries({
          queryKey: ["encounter", encounter.id],
        });
      });
      connection.on("RequeryInitiative", () => {
        queryClient.invalidateQueries({
          queryKey: ["initiativeQueue", encounter.id],
        });
        queryClient.invalidateQueries({
          queryKey: ["participance", encounter.id],
          exact: false,
        });
        queryClient.invalidateQueries({
          queryKey: ["character"],
          exact: false,
        });
        queryClient.invalidateQueries({
          queryKey: ["isItMyTurn", encounter.id],
          exact: false,
        });
      });
      connection.on("RequeryParticipanceData", () => {
        queryClient.invalidateQueries({
          queryKey: ["participance", encounter.id],
          exact: false,
        });
      });
    }
    return () => {
      if (connection) {
        connection.off("UpdatePath");
        connection.off("RequeryInitiative");
        connection.off("RequeryParticipanceData");
      }
    };
  }, [connection, encounter.id, queryClient]);

  useEffect(() => {
    if (controlState.path && !!connection) {
      if (controlState.path.length !== 0) {
        setOtherPath([]);
        connection.invoke("SendSelectedPath", controlState.path);
      }
    }
  }, [connection, controlState.path]);

  return (
    <>
      <GridContainer>
        <VirtualBoard
          encounter={encounter}
          connection={connection}
          groupName={groupName}
          mode={controlState.mode}
          dispatch={dispatch}
          path={controlState.path}
          otherPath={otherPath}
          weaponAttack={controlState.weaponAttackSelected!}
          power={controlState.powerSelected!}
          onWeaponAttackOverlay={handleWeaponAttackOverlay}
          selectedTargets={controlState.powerTargets}
        />
      </GridContainer>
      <RightPanel>
        <ChatContentMessage ref={chatRef}>
          {messages.map((message, index) => (
            <ChatMessageBox key={index}>
              <Heading as="h3" align="left">
                {message.username}
              </Heading>
              {message.message.split("\n").map((line, i) => (
                <p key={i}>{line}</p>
              ))}
            </ChatMessageBox>
          ))}
        </ChatContentMessage>
        <ChatForm onSubmit={handleSendMessage}>
          <Input
            type="text"
            id="message"
            placeholder="Enter your message"
            autoComplete="message"
            value={messageInput}
            onChange={(e) => setMessageInput(e.target.value)}
          />
          <Button size="large" variation="primary" type="submit">
            Send
            {/* {!isLoading ? "Send" : <SpinnerMini />} */}
          </Button>
        </ChatForm>
      </RightPanel>
      <BottomPanel2>
        <ActionBar
          controlState={controlState}
          dispatch={dispatch}
          encounter={encounter}
          connection={connection as HubConnection}
          onPowerCastOverlay={handlePowerCastOverlay}
        ></ActionBar>
      </BottomPanel2>
      <ModalNoButton
        open={controlState.mode === "WeaponAttackOverlay"}
        handleClose={() => dispatch({ type: "CHANGE_MODE", payload: "Idle" })}
      >
        <WeaponAttackResolution
          controlState={controlState}
        ></WeaponAttackResolution>
      </ModalNoButton>
      <ModalNoButton
        open={controlState.mode === "PowerCastOverlay"}
        handleClose={() => dispatch({ type: "CHANGE_MODE", payload: "Idle" })}
      >
        <PowerCastResolution controlState={controlState}></PowerCastResolution>
      </ModalNoButton>
    </>
  );
}
