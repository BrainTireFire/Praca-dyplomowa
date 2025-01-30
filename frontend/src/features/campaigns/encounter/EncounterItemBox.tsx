import React from "react";
import { Encounter } from "../../../models/encounter/Encounter";
import styled from "styled-components";
import Box from "../../../ui/containers/Box";
import Heading from "../../../ui/text/Heading";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import Button from "../../../ui/interactive/Button";
import Modal from "../../../ui/containers/Modal";
import ConfirmDelete from "../../../ui/containers/ConfirmDelete";
import useToggleEncounterActive from "../hooks/useToggleEncounterActive";
import Spinner from "../../../ui/interactive/Spinner";
import useDeleteEncounter from "../hooks/useDeleteEncounter";

const StyledBox = styled(Box)`
  display: grid;
  grid-template-rows: auto auto auto auto;
  gap: 1rem;
  padding: 1.5rem;
  border-radius: 8px;
  box-shadow: var(--shadow-md);
  transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
  cursor: pointer;

  &:hover {
    transform: translateY(-3px);
    box-shadow: var(--shadow-md);
    background: var(--color-navbar-hover);
  }
`;

const InfoText = styled.p`
  font-size: 1rem;
  color: var(--color-secondary-text);
  text-align: center;
  margin: 0;
`;

const ButtonContainer = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 0.5rem;
`;

export default function EncounterItemBox({
  encounter,
}: {
  encounter: Encounter;
}) {
  const { isPending, toggleEncounterActive } = useToggleEncounterActive();
  const { deleteEncounter, isDeleting } = useDeleteEncounter(encounter.id);

  if (isPending || isDeleting) {
    return <Spinner />;
  }

  return (
    <StyledBox>
      <Heading as="h2">{encounter.name}</Heading>

      <InfoText>{encounter.participances.length} Participants</InfoText>
      <InfoText>Active: {encounter.isActive ? "Yes ✅" : "No ❌"}</InfoText>

      <ButtonContainer>
        <ButtonGroup justify="center">
          {encounter.isActive ? (
            <Button
              variation="primary"
              size="large"
              onClick={() => toggleEncounterActive(encounter.id)}
            >
              Set Inactive
            </Button>
          ) : null}

          <Button variation="primary" size="large">
            Edit
          </Button>

          <Modal>
            <Modal.Open opens="ConfirmDeleteEncounter">
              <Button variation="danger" size="large">
                Remove
              </Button>
            </Modal.Open>
            <Modal.Window name="ConfirmDeleteEncounter">
              <ConfirmDelete
                resourceName={encounter.name + " encounter"}
                onConfirm={() => deleteEncounter()}
              />
            </Modal.Window>
          </Modal>
        </ButtonGroup>
      </ButtonContainer>
    </StyledBox>
  );
}
