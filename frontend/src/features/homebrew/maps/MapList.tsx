import React from "react";
import Spinner from "../../../ui/interactive/Spinner";
import { BoardCreateDto } from "../../../models/map/BoardDto";
import { useBoards } from "./useBoards";
import MapInstance from "./MapItemBox";
import styled from "styled-components";
import { Board } from "../../../models/map/Board";
import Button from "../../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding-top: 15px;
  gap: 20px;
  align-items: center;
`;

const CampaignListLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 1rem;
`;

export default function MapList() {
  const { isLoading, boards } = useBoards();
  const navigate = useNavigate();

  if (isLoading) {
    return <Spinner />;
  }

  if (!boards || boards.length === 0) {
    return (
      <Container>
        No maps available.
        <Button size="large" onClick={() => navigate(`/homebrew/createMap`)}>
          Create board
        </Button>
      </Container>
    );
  }

  return (
    <Container>
      <CampaignListLayout>
        {boards.map((board: Board) => (
          <MapInstance key={board.id} board={board} />
        ))}
      </CampaignListLayout>
      <Button size="large" onClick={() => navigate(`/homebrew/createMap`)}>
        Create board
      </Button>
    </Container>
  );
}
