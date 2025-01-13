import { ReusableTable } from "../../../ui/containers/ReusableTable2";
import styled, { css } from "styled-components";
import Button from "../../../ui/interactive/Button";
import Modal from "../../../ui/containers/Modal";
import { NPCSelectionForm } from "./NPCSelectionForm";
import { CharacterItem } from "../../../models/character";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
`;

const StyledHeader = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  max-width: 775px;
`;

const StyledTitle = styled.div`
  flex: 1;
  text-align: center;
`;

const TABLE_COLUMNS = {
  Name: "name",
  Race: "race",
  // HP: "health",
  // AC: "armorClass",
  // STR: "strength",
  // DEX: "dexterity",
  // CON: "constitution",
  // INT: "intelligence",
  // WIS: "wisdom",
  // CHA: "charisma",
};

export default function EncounterNPCTable({
  chosenNpcs,
  onConfirm,
}: {
  chosenNpcs: CharacterItem[];
  onConfirm: (selectedCharacters: CharacterItem[]) => void;
}) {
  return (
    <Container>
      <StyledHeader>
        <StyledTitle>Selected NPCs</StyledTitle>
        <Modal>
          <Modal.Open opens="selection">
            <Button
              size="small"
              type="button"
              onClick={(event) => {
                event.preventDefault();
              }}
            >
              Change selection
            </Button>
          </Modal.Open>
          <Modal.Window name="selection">
            <NPCSelectionForm
              initialNpcList={chosenNpcs}
              onConfirm={onConfirm}
            ></NPCSelectionForm>
          </Modal.Window>
        </Modal>
      </StyledHeader>
      <ReusableTable
        tableRowsColomns={TABLE_COLUMNS}
        data={chosenNpcs}
        isSelectable={false}
        customTableContainer={css`
          flex: 1;
        `}
      />
    </Container>
  );
}
