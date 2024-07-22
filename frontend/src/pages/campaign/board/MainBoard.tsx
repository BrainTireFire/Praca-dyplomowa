import React, { useEffect, useState } from "react";
import styled from "styled-components";
import VirtualBoard from "../../../features/campaigns/virtualBoard/VirtualBoard";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Heading from "../../../ui/text/Heading";

const Container = styled.div`
  display: grid;
  grid-template-columns: 3fr 1fr;
  grid-template-rows: auto 1fr;
  gap: 10px;
  height: 100vh;
`;

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

const RoomSelector = styled.div`
  display: flex;
  flex-direction: column;
  margin: 10px;
`;

const RoomButton = styled.button`
  margin: 5px 0;
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;

  &:hover {
    background-color: #0056b3;
  }
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

export default function MainBoard() {
  const [messages, setMessages] = useState<
    { message: string; username: string }[]
  >([]);
  const [messageInput, setMessageInput] = useState<string>("");
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [currentRoom, setCurrentRoom] = useState<string | null>(null);
  const [usersInGroup, setUsersInGroup] = useState<string[]>([]);

  useEffect(() => {
    const hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/chat") // Ensure this is the correct URL
      .configureLogging(LogLevel.Information)
      .build();

    hubConnection
      .start()
      .then(() => {
        console.log("SignalR Connected.");
        setConnection(hubConnection);
      })
      .catch((error) => console.error("SignalR Connection Error: ", error));

    hubConnection.on(
      "ReceiveMessage",
      (messageDto: { username: string; message: string }) => {
        setMessages((prevMessages) => [...prevMessages, messageDto]);
      }
    );

    return () => {
      hubConnection.stop().catch(console.error);
    };
  }, []);

  const joinRoom = (roomName: string) => {
    if (connection) {
      if (currentRoom) {
        connection.invoke("LeaveGroup", currentRoom).catch(console.error);
      }
      connection.invoke("JoinGroup", roomName).catch(console.error);
      setCurrentRoom(roomName);
    }
  };

  const handleSendMessage = async (e: React.FormEvent) => {
    e.preventDefault();
    if (connection && currentRoom && messageInput.trim()) {
      try {
        await connection.invoke(
          "SendMessageToGroup",
          currentRoom,
          messageInput
        );
        console.log("Message sent:", messageInput); // Debugging line
        setMessageInput(""); // Clear the input after sending the message
      } catch (error) {
        console.error("Error sending message:", error);
      }
    }
  };

  return (
    <Container>
      <GridContainer>
        <VirtualBoard connection={connection} currentRoom={currentRoom} />
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
        <RoomSelector>
          <RoomButton onClick={() => joinRoom("Room1")}>Join Room 1</RoomButton>
          <RoomButton onClick={() => joinRoom("Room2")}>Join Room 2</RoomButton>
          <RoomButton onClick={() => joinRoom("Room3")}>Join Room 3</RoomButton>
        </RoomSelector>
        <UsersList>
          <h3>Users in Group:</h3>
          <ul>
            {usersInGroup.map((user, index) => (
              <li key={index}>{user}</li>
            ))}
          </ul>
        </UsersList>
      </BottomPanel>
    </Container>
  );
}
