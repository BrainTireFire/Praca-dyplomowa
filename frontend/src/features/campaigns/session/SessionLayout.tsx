import React, { useEffect, useState } from "react";
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

const GridContainer = styled.div`
  grid-row: 1 / 2;
  grid-column: 1 / 2;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
  overflow: hidden;
`;

const RightPanel = styled.div`
  grid-row: 1 / 3;
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
  height: 48%;
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

export default function SessionLayout() {
  const { groupName } = useParams();

  //TODO REACT QUERY OR STATE MANAGEMENT
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [usersConnected, setUsersConnected] = useState<string[]>([]);
  const [messages, setMessages] = useState<
    { message: string; username: string }[]
  >([]);

  const [messageInput, setMessageInput] = useState<string>("");

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

  return (
    <>
      <GridContainer>
        <VirtualBoard connection={connection} groupName={groupName} />
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
    </>
  );
}
