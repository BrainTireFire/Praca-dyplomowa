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
import { FaTimes } from "react-icons/fa";

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

const StyledFaTimes = styled(FaTimes)`
  font-size: 2.1rem;
  cursor: pointer;

  &:hover {
    color: var(--color-image-hover);
    box-shadow: var(--shadow-lg);
  }

  /* pointer-events: auto; */
`;

const FieldSet = styled.div`
  position: relative;
  width: 250px;
  margin-bottom: 10px;
  padding: 10px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: var(--color-button-primary);
  border: 1px solid var(--color-border);
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease, box-shadow 0.3s ease;

  &:hover {
    background-color: var(--color-button-danger);
    box-shadow: var(--shadow-lg);
  }

  /* & > ${StyledFaTimes}:hover + & {
    background-color: inherit;
    box-shadow: inherit;
  } */
`;

const FieldContainerStyled = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

const ButtonContainer = styled.div`
  margin-top: auto;
  width: 100%;
  display: flex;
  justify-content: center;
`;

type FieldEncounterMap = Field & {
  memberId?: number;
  avatarUrl?: string;
  memberName?: string;
  size?: string;
};

export default function EncounterMapCreaterLayout({
  encounterId,
  onToggle,
  startEncounter,
}: {
  encounterId: number;
  onToggle?: Function;
  startEncounter?: boolean;
}) {
  const { isLoading, encounter } = useEncounter(encounterId);
  const [selectedBox, setSelectedBox] = useState<Coordinate>({});
  const [fields, setFields] = useState<FieldEncounterMap[]>([]);
  const { updatePlaceEncounter, isUpdating } = useUpdatePlaceEncounter(
    encounterId,
    startEncounter
  );

  useEffect(() => {
    if (encounter?.board) {
      // setFields(encounter.board.fields);
      const occupiedMap = new Map(
        encounter.participances.map((p) => [p.occupiedField?.id, p])
      );

      const fields = encounter.board.fields.map((field) => {
        const participance = occupiedMap.get(field.id);

        if (!participance) {
          return field;
        }

        return {
          ...field,
          size: participance?.character.size.name,
          memberName: participance?.character.name,
          memberId: participance?.character.id,
          avatarUrl: participance?.character.isNpc
            ? "https://pbs.twimg.com/profile_images/1810521561352617985/ornocKLB_400x400.jpg"
            : "https://s3.amazonaws.com/files.d20.io/images/390056921/JkAY2BnZBWR-IYsYkqx3_Q/original.png",
        };
      });

      setFields(fields);
    }
  }, [encounter?.board, encounter?.participances]);

  const handleFieldUpdate = useCallback(
    (updatedField: {
      positionX: number;
      positionY: number;
      memberName: string;
      memberId: number;
      avatarUrl: string;
    }) => {
      setFields((prevFields) => {
        const existingFieldWithMember = prevFields.find(
          (field) =>
            field.memberId === updatedField.memberId &&
            (field.memberName === updatedField.memberName || !field.memberName)
        );

        let fieldsToUpdate = prevFields;
        if (existingFieldWithMember) {
          fieldsToUpdate = prevFields.map((field) =>
            field === existingFieldWithMember
              ? {
                  ...field,
                  memberName: undefined,
                  memberId: undefined,
                  avatarUrl: undefined,
                }
              : field
          );
        }

        return fieldsToUpdate.map((field) =>
          field.positionX === updatedField.positionX &&
          field.positionY === updatedField.positionY
            ? {
                ...field,
                memberName: updatedField.memberName,
                memberId: updatedField.memberId,
                avatarUrl: updatedField.avatarUrl,
              }
            : field
        );
      });
    },
    [setFields]
  );

  const handleMemberRemove = useCallback((character: CharacterItem) => {
    setFields((prevFields) =>
      prevFields.map((field) =>
        field.memberId === character.id
          ? {
              ...field,
              memberName: undefined,
              memberId: undefined,
              avatarUrl: undefined,
            }
          : field
      )
    );
  }, []);

  const handleResetAllMemebers = useCallback(() => {
    setFields((prevFields) =>
      prevFields.map((field) =>
        field.memberId
          ? {
              ...field,
              memberName: undefined,
              memberId: undefined,
              avatarUrl: undefined,
            }
          : field
      )
    );
  }, []);

  const handleMemberClick = useCallback(
    (character: CharacterItem) => {
      if (!selectedBox) return;

      const field = fields.find(
        (field) =>
          field.positionX === selectedBox.x && field.positionY === selectedBox.y
      );

      if (!field) return;

      if (field.fieldMovementCost === "Impassable") return;

      const avatarUrl = character.isNpc
        ? "https://pbs.twimg.com/profile_images/1810521561352617985/ornocKLB_400x400.jpg"
        : "https://s3.amazonaws.com/files.d20.io/images/390056921/JkAY2BnZBWR-IYsYkqx3_Q/original.png";

      handleFieldUpdate({
        positionX: selectedBox.x,
        positionY: selectedBox.y,
        memberName: character.name,
        memberId: character.id,
        avatarUrl: avatarUrl,
      });

      setSelectedBox(null);
    },
    [selectedBox, handleFieldUpdate, fields]
  );

  if (isLoading) {
    return <Spinner />;
  }

  if (!encounter) {
    return <div>There are no encounter</div>;
  }

  const handleSubmit = () => {
    const fieldsToUpdate = getFieldsToUpdate();
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
        {onToggle && <Button onClick={() => onToggle(false)}>Back</Button>}
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
                <StyledFaTimes
                  onClick={(e) => {
                    e.stopPropagation();
                    handleMemberRemove(member);
                  }}
                />
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
                    <StyledFaTimes
                      onClick={(e) => {
                        e.stopPropagation();
                        handleMemberRemove(participance.character);
                      }}
                    />
                  </FieldSet>
                )
            )}
          </FieldContainerStyled>
          <ButtonContainer>
            <Button onClick={handleResetAllMemebers}>Reset all members</Button>
          </ButtonContainer>
        </LeftPanel>
      </MainGrid>
    </Container>
  );
}
