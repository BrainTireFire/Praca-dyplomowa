import React from "react";
import Spinner from "../../../ui/interactive/Spinner";
import { BoardCreateDto } from "../../../models/map/BoardDto";
import { useBoards } from "./useBoards";
import MapInstance from "./MapItemBox";
import styled from "styled-components";
import { Board } from "../../../models/map/Board";

const CampaignListLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 1rem;
`;

export default function MapList() {
  const { isLoading, boards } = useBoards();

  if (isLoading) {
    return <Spinner />;
  }

  if (!boards || boards.length === 0) {
    return <div>No maps available.</div>;
  }

  return (
    <CampaignListLayout>
      {boards.map((board: Board) => (
        <MapInstance key={board.id} board={board} />
      ))}
    </CampaignListLayout>
  );
}
