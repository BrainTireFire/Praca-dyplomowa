import React, { useEffect, useReducer, useState } from "react";
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
  height: 80%;
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

export type Mode = "Idle" | "Movement" | "WeaponAttack" | "PowerCast";

type ControlState = {
  mode: Mode;
  path: number[];
};

// Define action types
const CHANGE_MODE = "CHANGE_MODE";
const APPEND_PATH = "APPEND_PATH";
const POP_PATH = "POP_PATH";
const TOGGLE_PATH = "TOGGLE_PATH";
const SET_PATH = "SET_PATH";

// Define action interfaces
interface ChangeModeAction {
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

// Union type for all actions
export type ControlStateActions =
  | ChangeModeAction
  | AppendPathAction
  | PopPathAction
  | TogglePathAction
  | SetPathAction;

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
    default:
      return state;
  }
};

// initial state
const initialState: ControlState = {
  mode: "Idle",
  path: [],
};

export default function SessionLayout({ encounter }: any) {
  const { groupName } = useParams();

  //TODO REACT QUERY OR STATE MANAGEMENT
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [usersConnected, setUsersConnected] = useState<string[]>([]);
  const [messages, setMessages] = useState<
    { message: string; username: string }[]
  >([]);

  const [messageInput, setMessageInput] = useState<string>("");

  const [selectedCharacterId, setSelectedCharacterId] = useState<number | null>(
    null
  );

  const [controlState, dispatch] = useReducer(
    controlStateReducer,
    initialState
  );
  const [otherPath, setOtherPath] = useState<number[]>([]);

  console.log(controlState.path);

  useEffect(() => {
    if (groupName) {
      const hubConnection = new HubConnectionBuilder()
        .withUrl(`${BASE_URL}/session?groupName=${groupName}`)
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();

      hubConnection
        .start()
        .then(() => {
          console.log("SignalR Connected.");

          hubConnection
            .invoke("GetUsersInGroup", groupName)
            .then((userList: string[]) => {
              setUsersConnected(userList);
            })
            .catch((err) => console.error("Error fetching user list:", err));

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

          setConnection(hubConnection);
        })
        .catch((error) => console.error("SignalR Connection Error: ", error));

      return () => {
        if (hubConnection) {
          hubConnection.stop().then(() => console.log("Connection stopped"));
        }
      };
    } else {
      console.error("Group name is not defined.");
    }
  }, [groupName]);

  const handleSendMessage = async (e: React.FormEvent) => {
    e.preventDefault();
    if (connection && groupName && messageInput.trim()) {
      try {
        await connection
          .invoke("SendMessageToGroup", groupName, messageInput)
          .catch((err) => console.error("Error while sending message", err));
        setMessageInput("");
      } catch (error) {
        console.error("Error sending message:", error);
      }
    }
  };

  const queryClient = useQueryClient();
  useEffect(() => {
    if (connection) {
      connection.on("UpdatePath", (incomingPath: number[]) => {
        console.log("update");
        console.log(incomingPath);
        setOtherPath(incomingPath);
        // dispatch({ type: "SET_PATH", payload: [] });
      });
      connection.on("RequeryInitiative", () => {
        console.log("Requery initiative signal detected");
        queryClient.invalidateQueries({
          queryKey: ["initiativeQueue", encounter.id],
        });
      });
    }
  }, [connection, encounter.id, queryClient]);

  useEffect(() => {
    if (controlState.path && !!connection) {
      console.log("path change detected");
      setOtherPath([]);
      connection.invoke("SendSelectedPath", controlState.path);
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
        />
      </GridContainer>
      <RightPanel>
        <ChatContentMessage>
          {messages.map((message, index) => (
            <ChatMessageBox key={index}>
              <Heading as="h3" align="left">
                {message.username}
              </Heading>
              <p>{message.message}</p>
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
      <BottomPanel>
        <UsersList>
          <h3>Group: {groupName}</h3>
          <ul>
            {usersConnected.map((username, index) => (
              <li key={index}>{username}</li>
            ))}
          </ul>
        </UsersList>
      </BottomPanel>
      <BottomPanel2>
        <ActionBar
          dispatch={dispatch}
          encounter={encounter}
          connection={connection as HubConnection}
        ></ActionBar>
      </BottomPanel2>
    </>
  );
}
