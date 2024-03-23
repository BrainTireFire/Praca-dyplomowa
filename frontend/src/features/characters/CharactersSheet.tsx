import React from "react";
import styled from "styled-components";
import StatsContainer from "../../ui/characters/StatsContainer";
import Attributes from "./Attributes";
import EquipmentTable from "./EquipmentTable";
import ProficiencyBox from "../../ui/characters/ProficiencyBox";

const StyledCharactersSheet = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 5fr;
  grid-template-rows: auto 34rem auto;
  gap: 2.4rem;
`;

const EquipmentContainer = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 2.4rem;
`;

const data = [
  {
    name: "Acrobatics",
    ability: "DEX",
    value: "+3",
  },
  {
    name: "Animal Handling",
    ability: "WIS",
    value: "+5",
  },
  {
    name: "Arcana",
    ability: "INT",
    value: "+0",
  },
  {
    name: "Athletics",
    ability: "STR",
    value: "+4",
  },
  {
    name: "Deception",
    ability: "CHA",
    value: "-1",
  },
  {
    name: "History",
    ability: "INT",
    value: "+0",
  },
  {
    name: "Insight",
    ability: "WIS",
    value: "+2",
  },
  {
    name: "Intimidation",
    ability: "CHA",
    value: "-1",
  },
  {
    name: "Investigation",
    ability: "INT",
    value: "+0",
  },
  {
    name: "Medicine",
    ability: "WIS",
    value: "+2",
  },
  {
    name: "Nature",
    ability: "INT",
    value: "+3",
  },
  {
    name: "Perception",
    ability: "WIS",
    value: "+5",
  },
  {
    name: "Performance",
    ability: "CHA",
    value: "-1",
  },
  {
    name: "Persuasion",
    ability: "CHA",
    value: "-1",
  },
  {
    name: "Religion",
    ability: "INT",
    value: "+0",
  },
  {
    name: "Sleight of Hand",
    ability: "DEX",
    value: "+3",
  },
  {
    name: "Stealth",
    ability: "DEX",
    value: "+6",
  },
  {
    name: "Survival",
    ability: "WIS",
    value: "+5",
  },
];

export default function CharactersSheet() {
  return (
    <>
      <StyledCharactersSheet>
        {/* <Attributes />
    <Attributes /> */}
        <StatsContainer></StatsContainer>
        <ProficiencyBox data={data} header="Skills"></ProficiencyBox>
        <ProficiencyBox data={data} header="Saving throws"></ProficiencyBox>
      </StyledCharactersSheet>
      <EquipmentContainer>
        <EquipmentTable />
        <EquipmentTable />
      </EquipmentContainer>
    </>
  );
}
