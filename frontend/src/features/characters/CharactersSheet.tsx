import React from "react";
import styled from "styled-components";
import StatsContainer from "../../ui/characters/StatsContainer";
import Attributes from "./Attributes";
import EquipmentTable from "./EquipmentTable";
import ProficiencyBox from "../../ui/characters/ProficiencyBox";
import Box from "../../ui/containers/Box";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import KnownLanguagesTable from "./KnownLanguagesTable";
import ToolProficiencyTable from "./ToolProficiencyTable";
import DisplayBox from "./DisplayBox";

const StyledCharactersSheet = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  grid-template-rows: auto auto auto;
  gap: 0.4rem;
`;

const EquipmentContainer = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 2.4rem;
`;

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

const DisplayBoxContent = styled.div`
  font-size: 2rem;
  text-align: center;
`;

export default function CharactersSheet() {
  return (
    <Box radius="tiny">
      <MainGrid>
        <MainGridColumn1>
          <div style={{ gridColumnStart: 1, gridColumnEnd: 3 }}>
            <FormRowVertical label="Name">
              <Input></Input>
            </FormRowVertical>
          </div>
          <div
            style={{ gridColumnStart: 1, gridColumnEnd: 3, gridRowStart: 2 }}
          >
            <FormRowVertical label="Description">
              <Input></Input>
            </FormRowVertical>
          </div>
          <div style={{ gridColumnStart: 3, gridRowStart: 1, gridRowEnd: 4 }}>
            <ProficiencyBox data={data} header="Skills"></ProficiencyBox>
          </div>
          <div style={{ gridColumnStart: 2, gridRowStart: 3, gridRowEnd: 4 }}>
            <ProficiencyBox
              data={savingThrows}
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
            <ToolProficiencyTable></ToolProficiencyTable>
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
            <ToolProficiencyTable></ToolProficiencyTable>
          </div>
          <div style={{ gridColumnStart: 3, gridColumnEnd: 5 }}>
            <FormRowVertical label="Race">
              <Input></Input>
            </FormRowVertical>
          </div>
          <div
            style={{ gridColumnStart: 3, gridColumnEnd: 5, gridRowStart: 2 }}
          >
            <FormRowVertical label="Size">
              <Input></Input>
            </FormRowVertical>
          </div>
          <div
            style={{ gridColumnStart: 1, gridColumnEnd: 2, gridRowStart: 3 }}
          >
            <FormRowVertical label="Hit points">
              <Input></Input>
            </FormRowVertical>
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
        </MainGridColumn2>
      </MainGrid>
      <EquipmentContainer>
        <EquipmentTable />
        <EquipmentTable />
      </EquipmentContainer>
    </Box>
  );
}

const data = [
  {
    name: "Acrobatics",
    ability: "DEX",
    value: "+3",
    proficient: false,
  },
  {
    name: "Animal Handling",
    ability: "WIS",
    value: "+5",
    proficient: true,
  },
  {
    name: "Arcana",
    ability: "INT",
    value: "+0",
    proficient: true,
  },
  {
    name: "Athletics",
    ability: "STR",
    value: "+4",
    proficient: true,
  },
  {
    name: "Deception",
    ability: "CHA",
    value: "-1",
    proficient: true,
  },
  {
    name: "History",
    ability: "INT",
    value: "+0",
    proficient: true,
  },
  {
    name: "Insight",
    ability: "WIS",
    value: "+2",
    proficient: true,
  },
  {
    name: "Intimidation",
    ability: "CHA",
    value: "-1",
    proficient: true,
  },
  {
    name: "Investigation",
    ability: "INT",
    value: "+0",
    proficient: true,
  },
  {
    name: "Medicine",
    ability: "WIS",
    value: "+2",
    proficient: true,
  },
  {
    name: "Nature",
    ability: "INT",
    value: "+3",
    proficient: true,
  },
  {
    name: "Perception",
    ability: "WIS",
    value: "+5",
    proficient: true,
  },
  {
    name: "Performance",
    ability: "CHA",
    value: "-1",
    proficient: true,
  },
  {
    name: "Persuasion",
    ability: "CHA",
    value: "-1",
    proficient: true,
  },
  {
    name: "Religion",
    ability: "INT",
    value: "+0",
    proficient: true,
  },
  {
    name: "Sleight of Hand",
    ability: "DEX",
    value: "+3",
    proficient: true,
  },
  {
    name: "Stealth",
    ability: "DEX",
    value: "+6",
    proficient: true,
  },
  {
    name: "Survival",
    ability: "WIS",
    value: "+5",
    proficient: true,
  },
];

const savingThrows = [
  {
    name: "Strength",
    ability: "",
    value: "+2",
    proficient: true,
  },
  {
    name: "Dexterity",
    ability: "",
    value: "+2",
    proficient: true,
  },
  {
    name: "Constitution",
    ability: "",
    value: "+2",
    proficient: true,
  },
  {
    name: "Intelligence",
    ability: "",
    value: "+2",
    proficient: true,
  },
  {
    name: "Wisdom",
    ability: "",
    value: "+2",
    proficient: true,
  },
  {
    name: "Charisma",
    ability: "",
    value: "+2",
    proficient: true,
  },
];
