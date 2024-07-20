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

export default function MainBoard() {
  const [connection, setConnection] = useState<HubConnection | null>(null);

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

    return () => {
      hubConnection.stop().catch(console.error);
    };
  }, []);

  return (
    <Container>
      <GridContainer>
        <VirtualBoard connection={connection} />
      </GridContainer>
      <RightPanel>RightPanel</RightPanel>
      <BottomPanel>BottomPanel</BottomPanel>
    </Container>
  );
}
