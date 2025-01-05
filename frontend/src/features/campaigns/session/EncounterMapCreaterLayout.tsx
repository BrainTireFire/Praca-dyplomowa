import React, { useEffect, useState, useCallback } from "react";
import styled from "styled-components";
import MapBoard from "../../homebrew/maps/MapBoard";
import { useEncounter } from "../hooks/useEncounter";
import Spinner from "../../../ui/interactive/Spinner";
import { Field } from "../../../models/map/Board";
import { CharacterItem } from "../../../models/character";
import { Coordinate } from "../../../models/session/Coordinate";
import Heading from "../../../ui/text/Heading";
import Button from "../../../ui/interactive/Button";
import { useUpdatePlaceEncounter } from "../hooks/useUpdatePlaceEncounter";

const Container = styled.div`
  padding-top: 20px;
`;

const HeaderStyled = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-left: 50px;
  padding-right: 50px;
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
  grid-row: 1 / 3;
  grid-column: 1 / 2;
  display: flex;
  flex-direction: column;
  gap: 20px;
  align-items: flex-start;
  background-color: var(--color-navbar);
  border: 1px solid var(--color-border);
  padding: 20px;
  height: 100%;
  width: 300px;
`;

const MainGrid = styled.div`
  display: grid;
  grid-template-rows: 1fr;
  grid-template-columns: 300px 1fr;
  margin-top: 20px;
`;

const Label = styled.label`
  font-weight: bold;
  margin-bottom: 5px;
`;

const FieldSet = styled.div`
  position: relative;
  width: 250px;
  margin-bottom: 10px;
  padding: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: var(--color-button-primary);
  border: 1px solid var(--color-border);
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease, box-shadow 0.3s ease;

  &:hover {
    background-color: var(--color-button-danger);
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  }
`;

const FieldContainerStyled = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

export default function EncounterMapCreaterLayout({ encounterId, onToggle }) {
  const { isLoading, encounter } = useEncounter(encounterId);
  const [selectedBox, setSelectedBox] = useState<Coordinate>({});
  const [fields, setFields] = useState<Field[]>([]);
  const { updatePlaceEncounter, isUpdating } = useUpdatePlaceEncounter();

  useEffect(() => {
    if (encounter?.board) {
      setFields(encounter.board.fields);
    }
  }, [encounter?.board]);

  const handleFieldUpdate = useCallback(
    (updatedField: Field) => {
      setFields((prevFields) => {
        // Find the field that already has the same memberName
        const existingFieldWithMember = prevFields.find(
          (field) => field.memberName === updatedField.memberName
        );

        // If the field exists, clear its memberName
        let fieldsToUpdate = prevFields;
        if (existingFieldWithMember) {
          fieldsToUpdate = prevFields.map((field) =>
            field === existingFieldWithMember
              ? { ...field, memberName: null }
              : field
          );
        }

        // Update or add the target field
        return fieldsToUpdate.map((field) =>
          field.positionX === updatedField.positionX &&
          field.positionY === updatedField.positionY
            ? {
                ...field,
                memberName: updatedField.memberName,
                avatarUrl: updatedField.avatarUrl,
              }
            : field
        );
      });
    },
    [setFields]
  );

  const handleMemberClick = useCallback(
    (character: CharacterItem) => {
      if (!selectedBox) return;

      handleFieldUpdate({
        positionX: selectedBox.x,
        positionY: selectedBox.y,
        memberName: character.name,
        avatarUrl:
          "https://i1.sndcdn.com/avatars-000012078220-stfi4o-t1080x1080.jpg",
      });

      setSelectedBox(null);
    },
    [selectedBox, handleFieldUpdate]
  );

  if (isLoading) {
    return <Spinner />;
  }

  if (!encounter) {
    return <div>There are no encounter</div>;
  }

  const handleSubmit = () => {
    const fieldsToUpdate = getFieldsToUpdate();
    console.log("encounter.id:", encounter.id);
    console.log("fieldsToUpdate:", fieldsToUpdate);
    updatePlaceEncounter({
      encounterId: encounter.id,
      encounterUpdateDto: fieldsToUpdate,
    });
  };

  const getFieldsToUpdate = () => {
    // Filter fields where memberName exists
    const fieldsToUpdate = fields
      .filter((field) => field.memberName)
      .map((field) => {
        // Find the character in participances or campaign members
        const matchedParticipance = encounter.participances.find(
          (participance) => participance.character.name === field.memberName
        );
        const matchedMember = encounter.campaign.members.find(
          (member) => member.name === field.memberName
        );

        const characterId = matchedParticipance
          ? matchedParticipance.character.id
          : matchedMember?.id || null;

        return {
          fieldId: field.id,
          characterId,
          participanceDataId: matchedParticipance?.id || null,
        };
      });

    return fieldsToUpdate;
  };

  return (
    <Container>
      <HeaderStyled>
        <Button onClick={() => onToggle(false)}>Back</Button>
        <Heading as="h2">Create encounter map</Heading>
        <Button onClick={handleSubmit}>Save</Button>
      </HeaderStyled>
      <MainGrid>
        <GridContainer>
          <MapBoard
            board={encounter.board}
            selectedBox={selectedBox}
            onSelectedBox={setSelectedBox}
            fields={fields}
          />
        </GridContainer>
        <LeftPanel>
          <FieldContainerStyled>
            <Label>Members</Label>
            {encounter.campaign.members.map((member) => (
              <FieldSet
                key={member.id}
                onClick={() => handleMemberClick(member)}
              >
                {member.name}
              </FieldSet>
            ))}
          </FieldContainerStyled>
          <FieldContainerStyled>
            <Label>Npc</Label>
            {encounter.participances.map(
              (participance) =>
                participance.character.isNpc && (
                  <FieldSet
                    key={participance.id}
                    onClick={() => handleMemberClick(participance.character)}
                  >
                    {participance.character.name}
                  </FieldSet>
                )
            )}
          </FieldContainerStyled>
        </LeftPanel>
      </MainGrid>
    </Container>
  );
}
