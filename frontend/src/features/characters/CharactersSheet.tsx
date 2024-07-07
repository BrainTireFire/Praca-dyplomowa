import React, { useState } from "react";
import styled, { css } from "styled-components";
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
import TextArea from "../../ui/forms/TextArea";
import WeaponArmorProficiencyRow from "./WeaponArmorProficiencyRow";
import WeaponArmorProficiencyTable from "./WeaponArmorProficiencyTable";
import ClassTable from "./ClassTable";
import WeaponAttackTable from "./WeaponAttacksTable";
import ReadyPowerTable from "./ReadyPowersTable";
import ConstantEffectTable from "./ConstantEffectTable";
import EffectTable from "./EffectTable";
import ResourceTable from "./ResourceTable";
import PowersTable from "./PowersTable";

const StyledCharactersSheet = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  grid-template-rows: auto auto auto;
  gap: 0.4rem;
`;

// const EquipmentContainer = styled.div`
//   display: grid;
//   grid-template-columns: 1fr 1fr;
//   gap: 2.4rem;
// `;

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

export default function CharactersSheet({ characterId }) {
  const character = characters.find(
    (character) => character.Id === characterId
  );

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
              <TextArea></TextArea>
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

const characters = [
  {
    Id: 1,
    Name: "Gimli",
    Description: "Lorem ipsum cośtam cośtam",
    Attributes: [
      {
        name: "Strength",
        value: 15,
      },
      {
        name: "Dexterity",
        value: 14,
      },
      {
        name: "Constitution",
        value: 13,
      },
      {
        name: "Intelligence",
        value: 12,
      },
      {
        name: "Wisdom",
        value: 11,
      },
      {
        name: "Charisma",
        value: 10,
      },
    ],
    SavingThrows: [
      {
        name: "Strength",
        value: 4,
        proficient: true,
      },
      {
        name: "Dexterity",
        value: 3,
        proficient: true,
      },
      {
        name: "Constitution",
        value: 3,
        proficient: true,
      },
      {
        name: "Intelligence",
        value: 3,
        proficient: true,
      },
      {
        name: "Wisdom",
        value: 5,
        proficient: true,
      },
      {
        name: "Charisma",
        value: 1,
        proficient: true,
      },
    ],
    Skills: [
      {
        name: "Acrobatics",
        ability: "Dexterity",
        value: 3,
        proficient: false,
      },
      {
        name: "Animal Handling",
        ability: "Wisdom",
        value: 5,
        proficient: true,
      },
      {
        name: "Arcana",
        ability: "Intelligence",
        value: 0,
        proficient: true,
      },
      {
        name: "Athletics",
        ability: "Strength",
        value: 4,
        proficient: true,
      },
      {
        name: "Deception",
        ability: "Charisma",
        value: -1,
        proficient: true,
      },
      {
        name: "History",
        ability: "Intelligence",
        value: 0,
        proficient: true,
      },
      {
        name: "Insight",
        ability: "Wisdom",
        value: 2,
        proficient: true,
      },
      {
        name: "Intimidation",
        ability: "Charisma",
        value: -1,
        proficient: true,
      },
      {
        name: "Investigation",
        ability: "Intelligence",
        value: 0,
        proficient: true,
      },
      {
        name: "Medicine",
        ability: "Wisdom",
        value: 2,
        proficient: true,
      },
      {
        name: "Nature",
        ability: "Intelligence",
        value: 3,
        proficient: true,
      },
      {
        name: "Perception",
        ability: "Wisdom",
        value: 5,
        proficient: true,
      },
      {
        name: "Performance",
        ability: "Charisma",
        value: -1,
        proficient: true,
      },
      {
        name: "Persuasion",
        ability: "Charisma",
        value: "-1",
        proficient: true,
      },
      {
        name: "Religion",
        ability: "Intelligence",
        value: 0,
        proficient: true,
      },
      {
        name: "Sleight of Hand",
        ability: "Dexterity",
        value: 3,
        proficient: true,
      },
      {
        name: "Stealth",
        ability: "Dexterity",
        value: "+6",
        proficient: true,
      },
      {
        name: "Survival",
        ability: "Wisdom",
        value: 5,
        proficient: true,
      },
    ],
    KnownLanguages: [
      {
        Id: 1,
        Name: "Common",
      },
      {
        Id: 2,
        Name: "Elvish",
      },
      {
        Id: 3,
        Name: "Dwarven",
      },
    ],
    ToolProficiency: [
      {
        Id: 1,
        Name: "Smithing tools",
      },
      {
        Id: 2,
        Name: "Carpentry tools",
      },
    ],
    WeaponAndArmorProficiency: [
      {
        Id: 3,
        Name: "Longswords",
      },
      {
        Id: 4,
        Name: "Heavy armor",
      },
    ],
    Classes: [
      {
        Id: 1,
        Name: "Fighter",
        Level: 10,
      },
      {
        Id: 2,
        Name: "Paladin",
        Level: 15,
      },
    ],
    Race: {
      Id: 1,
      Name: "Human",
    },
    Hitpoints: {
      Current: 50,
      Maximum: 70,
      Temporary: 40,
    },
    Initiative: 5,
    Speed: 30,
    ArmorClass: 20,
    DeathSaves: {
      Success: 0,
      Failure: 0,
    },
    HitDice: {
      Total: {
        d20: 0,
        d12: 2,
        d10: 0,
        d8: 0,
        d6: 1,
        d4: 0,
      },
      Left: {
        d20: 0,
        d12: 0,
        d10: 0,
        d8: 0,
        d6: 0,
        d4: 0,
      },
    },
    WeaponAttacks: [
      {
        Main: true,
        Damage: {
          d20: 0,
          d12: 0,
          d10: 0,
          d8: 0,
          d6: 0,
          d4: 0,
        },
        DamageType: "Slashing",
        Range: 0,
      },
      {
        Main: false,
        Damage: {
          d20: 0,
          d12: 0,
          d10: 0,
          d8: 0,
          d6: 0,
          d4: 0,
        },
        DamageType: "Slashing",
        Range: 0,
      },
    ],
    Equipment: [
      {
        Id: 1,
        Name: "Stabby McStabface",
        ItemFamily: "Longsword",
        Slots: [
          {
            Id: 1,
            Name: "Main hand",
          },
        ],
      },
    ],
  },
];
