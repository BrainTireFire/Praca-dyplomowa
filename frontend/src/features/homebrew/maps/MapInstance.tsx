import React from "react";
import MapBoard from "./MapBoard";
import { useBoard } from "./useBoard";
import Spinner from "../../../ui/interactive/Spinner";
import styled, { css } from "styled-components";
import Heading from "../../../ui/text/Heading";
import { useMoveBack } from "../../../hooks/useMoveBack";
import Button from "../../../ui/interactive/Button";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  align-items: center;
  padding-top: 20px;
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
        <MapBoard board={boardData} fields={board.fields} />
      </Container>
    </>
  );
}
