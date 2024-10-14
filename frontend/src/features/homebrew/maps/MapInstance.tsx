import React, { useState } from "react";
import MapBoard from "./MapBoard";
import { useBoard } from "./useBoard";
import Spinner from "../../../ui/interactive/Spinner";
import styled, { css } from "styled-components";
import Heading from "../../../ui/text/Heading";
import { useMoveBack } from "../../../hooks/useMoveBack";
import Button from "../../../ui/interactive/Button";
import { Coordinate } from "../../../models/session/Coordinate";
import Box from "../../../ui/containers/Box";

const Container = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: center;
  gap: 20px;
  align-items: center;
  padding-top: 20px;
`;

const GridContainer = styled.div`
  grid-row: 1 / 2;
  grid-column: 2 / 3;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
  overflow: hidden;
`;

const LeftPanel = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  padding: 20px;
  height: 100%;
  width: 500px;
`;

const StyledElementBox = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  max-width: 800px;
  padding: 0 20px;
`;

const LeftAlignedButton = styled(Button)`
  margin-right: auto;
`;

const Span = styled.span`
  font-size: 3rem;
`;

export default function MapInstance() {
  const { isLoading, board } = useBoard();
  const [selectedBox, setSelectedBox] = useState<Coordinate | null>(null);
  const moveBack = useMoveBack();

  if (isLoading) {
    return <Spinner />;
  }

  if (!board) {
    return <div>Board is null</div>;
  }

  const boardData = {
    name: board.name,
    description: board.description,
    sizeX: board.sizeX,
    sizeY: board.sizeY,
  };

  const selectedField = board.fields.find(
    (field) =>
      field.positionX === selectedBox?.x && field.positionY === selectedBox?.y
  );

  return (
    <>
      <StyledElementBox>
        <LeftAlignedButton
          variation="secondary"
          onClick={moveBack}
          customStyles={css`
            background: none;
          `}
        >
          <Span>&larr;</Span>
        </LeftAlignedButton>
        <Heading as="h1" align="center">
          {board.name}
        </Heading>
      </StyledElementBox>
      <Container>
        <MapBoard
          board={boardData}
          fields={board.fields}
          selectedBox={selectedBox}
          onSelectedBox={setSelectedBox}
        />
        {selectedBox && (
          <LeftPanel>
            <Heading as="h2">Field Info</Heading>
            <StyledElementBox>
              Desctription: {selectedField?.description}
            </StyledElementBox>
            <StyledElementBox>
              Position X: {selectedField?.positionX}
            </StyledElementBox>
            <StyledElementBox>
              Position Y: {selectedField?.positionY}
            </StyledElementBox>
            <StyledElementBox>
              Heigth: {selectedField?.positionZ}
            </StyledElementBox>
            <StyledElementBox>Color: {selectedField?.color}</StyledElementBox>

            <StyledElementBox>
              Field cover level: {selectedField?.fieldCoverLevel}
            </StyledElementBox>
            <StyledElementBox>
              Field movement cost: {selectedField?.fieldMovementCost}
            </StyledElementBox>
          </LeftPanel>
        )}
      </Container>
    </>
  );
}
