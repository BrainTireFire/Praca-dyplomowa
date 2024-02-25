import React from "react";
import styled from "styled-components";
import StatsContainer from "../../ui/characters/StatsContainer";
import Attributes from "./Attributes";
import EquipmentTable from "./EquipmentTable";

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

export default function CharactersSheet() {
  return (
    <>
      <StyledCharactersSheet>
        {/* <Attributes />
    <Attributes /> */}
        {/* <StatsContainer></StatsContainer> */}
      </StyledCharactersSheet>
      <EquipmentContainer>
        <EquipmentTable />
        <EquipmentTable />
      </EquipmentContainer>
    </>
  );
}
