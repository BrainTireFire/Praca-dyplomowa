import React, { useState } from "react";
import styled, { css } from "styled-components";
import StatsContainer from "../../ui/characters/StatsContainer";
import EquipmentTable from "./EquipmentTable";
import ProficiencyBox from "../../ui/characters/ProficiencyBox";
import Box from "../../ui/containers/Box";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import KnownLanguagesTable from "./KnownLanguagesTable";
import ToolProficiencyTable from "./ToolProficiencyTable";
import DisplayBox from "./DisplayBox";
import TextArea from "../../ui/forms/TextArea";
import WeaponArmorProficiencyTable from "./WeaponArmorProficiencyTable";
import ClassTable from "./ClassTable";
import WeaponAttackTable from "./WeaponAttacksTable";
import ReadyPowerTable from "./ReadyPowersTable";
import ConstantEffectTable from "./ConstantEffectTable";
import EffectTable from "./EffectTable";
import ResourceTable from "./ResourceTable";
import PowersTable from "./PowersTable";
import { useCharacter } from "./useCharacter";
import Spinner from "../../ui/interactive/Spinner";

const MainGrid = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  gap: 0.4rem;
`;

const MainGridColumn1 = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  grid-template-rows: auto auto auto auto auto auto;
  grid-column-start: 1;
  grid-column-end: 2;
  gap: 0.4rem;
`;
const MainGridColumn2 = styled.div`
  display: grid;
  grid-template-columns: auto auto auto auto;
  grid-template-rows: auto auto auto auto auto auto auto;
  grid-column-start: 2;
  grid-column-end: 3;
  gap: 0.4rem;
`;
const MainGridColumn3 = styled.div`
  display: grid;
  grid-template-columns: auto;
  grid-template-rows: auto auto auto auto;
  grid-column-start: 3;
  grid-column-end: 4;
  gap: 0.4rem;
`;

const DisplayBoxContent = styled.div`
  font-size: 2rem;
  text-align: center;
`;

export default function CharactersSheet({
  characterId,
}: {
  characterId: number;
}) {
  const { isLoading, isError, error, character } = useCharacter(characterId);

  console.log(characterId);
  console.log(character);
  if (isLoading) {
    return <Spinner />;
  }
  if (isError) {
    return `${error}`;
  }

  return (
    <Box radius="tiny">
      <MainGrid>
        <MainGridColumn1>
          <div style={{ gridColumnStart: 1, gridColumnEnd: 3 }}>
            <FormRowVertical label="Name">
              <Input
                size="small"
                customStyles={css`
                  text-transform: uppercase;
                `}
                value={character?.name}
              ></Input>
            </FormRowVertical>
          </div>
          <div
            style={{
              gridColumnStart: 1,
              gridColumnEnd: 3,
              gridRowStart: 2,
              display: "flex",
              flexDirection: "column",
            }}
          >
            <FormRowVertical label="Description" fillHeight={true}>
              <TextArea value={character?.description}></TextArea>
            </FormRowVertical>
          </div>
          <div style={{ gridColumnStart: 3, gridRowStart: 1, gridRowEnd: 4 }}>
            <ProficiencyBox
              data={character?.skills}
              header="Skills"
            ></ProficiencyBox>
          </div>
          <div style={{ gridColumnStart: 2, gridRowStart: 3, gridRowEnd: 4 }}>
            <ProficiencyBox
              data={character?.savingThrows}
              header="Saving throws"
            ></ProficiencyBox>
          </div>
          <div style={{ gridColumnStart: 1, gridRowStart: 3, gridRowEnd: -1 }}>
            <StatsContainer></StatsContainer>
          </div>
          <div
            style={{
              gridColumnStart: 2,
              gridColumnEnd: -1,
              gridRowStart: 4,
              gridRowEnd: 5,
            }}
          >
            <KnownLanguagesTable></KnownLanguagesTable>
          </div>
          <div
            style={{
              gridColumnStart: 2,
              gridColumnEnd: -1,
              gridRowStart: 5,
              gridRowEnd: 6,
            }}
          >
            <ToolProficiencyTable></ToolProficiencyTable>
          </div>
          <div
            style={{
              gridColumnStart: 2,
              gridColumnEnd: -1,
              gridRowStart: 6,
              gridRowEnd: 7,
            }}
          >
            <WeaponArmorProficiencyTable></WeaponArmorProficiencyTable>
          </div>
        </MainGridColumn1>
        <MainGridColumn2>
          <div
            style={{
              gridColumnStart: 1,
              gridColumnEnd: 3,
              gridRowStart: 1,
              gridRowEnd: 3,
            }}
          >
            <ClassTable></ClassTable>
          </div>
          <div style={{ gridColumnStart: 3, gridColumnEnd: 5 }}>
            <FormRowVertical label="Race">
              <Input></Input>
            </FormRowVertical>
          </div>
          <div
            style={{ gridColumnStart: 3, gridColumnEnd: 5, gridRowStart: 2 }}
          >
            <DisplayBox label="Size">
              <DisplayBoxContent>Humungous</DisplayBoxContent>
            </DisplayBox>
          </div>
          <div
            style={{ gridColumnStart: 1, gridColumnEnd: 2, gridRowStart: 3 }}
          >
            <DisplayBox label="Hit points">
              <DisplayBoxContent>75/75 (+5)</DisplayBoxContent>
            </DisplayBox>
          </div>
          <div
            style={{ gridColumnStart: 2, gridColumnEnd: 3, gridRowStart: 3 }}
          >
            <DisplayBox label="Initiative">
              <DisplayBoxContent>+5</DisplayBoxContent>
            </DisplayBox>
          </div>
          <div
            style={{ gridColumnStart: 3, gridColumnEnd: 4, gridRowStart: 3 }}
          >
            <DisplayBox label="Speed">
              <DisplayBoxContent>30</DisplayBoxContent>
            </DisplayBox>
          </div>
          <div
            style={{ gridColumnStart: 4, gridColumnEnd: 5, gridRowStart: 3 }}
          >
            <DisplayBox label="Armor Class">
              <DisplayBoxContent>20</DisplayBoxContent>
            </DisplayBox>
          </div>
          <div
            style={{ gridColumnStart: 1, gridColumnEnd: 5, gridRowStart: 4 }}
          >
            <WeaponAttackTable />
          </div>
          <div
            style={{ gridColumnStart: 1, gridColumnEnd: 5, gridRowStart: 5 }}
          >
            <EquipmentTable />
          </div>
          <div
            style={{ gridColumnStart: 1, gridColumnEnd: 5, gridRowStart: 6 }}
          >
            <ReadyPowerTable />
          </div>
        </MainGridColumn2>
        <MainGridColumn3>
          <div style={{ gridColumnStart: 1, gridRowStart: 1 }}>
            <ConstantEffectTable />
          </div>
          <div style={{ gridColumnStart: 1, gridRowStart: 2 }}>
            <EffectTable />
          </div>
          <div style={{ gridColumnStart: 1, gridRowStart: 3 }}>
            <ResourceTable />
          </div>
          <div style={{ gridColumnStart: 1, gridRowStart: 4 }}>
            <PowersTable />
          </div>
        </MainGridColumn3>
      </MainGrid>
    </Box>
  );
}
