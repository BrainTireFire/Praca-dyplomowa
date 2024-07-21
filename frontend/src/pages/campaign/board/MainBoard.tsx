import React, { useEffect, useState } from "react";
import styled from "styled-components";
import VirtualBoard from "../../../features/campaigns/virtualBoard/VirtualBoard";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";

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
  height: 96%;
`;

const BottomPanel = styled.div`
  grid-row: 2 / 3;
  grid-column: 1 / 2;
  display: flex;
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  height: 82%;
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

export default function MainBoard() {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [currentRoom, setCurrentRoom] = useState<string | null>(null);
  const [usersInGroup, setUsersInGroup] = useState<string[]>([]);

  useEffect(() => {
    const hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/board")
      .configureLogging(LogLevel.Information)
      .build();

    hubConnection
      .start()
      .then(() => {
        console.log("SignalR Connected.");
        setConnection(hubConnection);
      })
      .catch((error) => console.error("SignalR Connection Error: ", error));

    hubConnection.on("ReceiveUsersInGroup", (users) => {
      setUsersInGroup(users);
    });

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

  return (
    <Container>
      <GridContainer>
        <VirtualBoard connection={connection} currentRoom={currentRoom} />
      </GridContainer>
      <RightPanel>
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
      </RightPanel>
      <BottomPanel>BottomPanel</BottomPanel>
    </Container>
  );
}
