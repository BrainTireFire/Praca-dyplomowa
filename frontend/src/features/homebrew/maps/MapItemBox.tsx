import React from "react";
import { Board } from "../../../models/map/Board";
import styled, { css } from "styled-components";
import Box from "../../../ui/containers/Box";
import Heading from "../../../ui/text/Heading";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import Button from "../../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";
import { useDeleteBoard } from "./useDeleteBoard";
import Modal from "../../../ui/containers/Modal";
import ConfirmDelete from "../../../ui/containers/ConfirmDelete";

const BoxCustomStyles = css`
  display: grid;
  grid-template-rows: 0.5fr 0.5fr 1.5fr 0.5fr;

  &:hover {
    transform: translateY(-3px);
    box-shadow: var(--shadow-md);
    background: var(--color-navbar-hover);
  }
`;

const StyledElementBox = styled.div`
  text-align: center;
`;

type Props = {
  board: Board;
};

export default function MapItemBox({ board }: Props) {
  const navigate = useNavigate();
  const { isDeleting, deleteBoard } = useDeleteBoard();

  return (
    <Box
      radius="tiny"
      customStyles={BoxCustomStyles}
      onClick={() => navigate(`/homebrew/map/${board.id}`)}
    >
      <Heading as="h2">{board.name}</Heading>
      <StyledElementBox>{board.description}</StyledElementBox>
      <StyledElementBox>
        {board.sizeX} x {board.sizeY}
      </StyledElementBox>
      <div onClick={(e) => e.stopPropagation()}>
        <ButtonGroup justify="center">
          <Button
            variation="primary"
            size="large"
            onClick={() => navigate(`/homebrew/updateMap/${board.id}`)}
          >
            Edit
          </Button>
          <Modal>
            <Modal.Open opens="deleteBoard">
              <Button variation="primary" size="large">
                Remove
              </Button>
            </Modal.Open>
            <Modal.Window name="deleteBoard">
              <ConfirmDelete
                resourceName="boards"
                disabled={isDeleting}
                onConfirm={() => deleteBoard(board.id)}
              />
            </Modal.Window>
          </Modal>
        </ButtonGroup>
      </div>
    </Box>
  );
}
