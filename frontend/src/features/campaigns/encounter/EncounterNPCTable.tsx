import { useState } from "react";
import Spinner from "../../../ui/interactive/Spinner";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import { useMaps } from "./useMaps";
import styled from "styled-components";
import Button from "../../../ui/interactive/Button";
import { useNpcCharacters } from "../../characters/hooks/useNpcCharacters";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
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
  Size: "size",
  Description: "description",
};

export default function EncounterNPCTable() {
  const { isLoading, npcCharacters } = useNpcCharacters();

  if (isLoading) {
    return <Spinner />;
  }

  if (!npcCharacters || npcCharacters.length === 0) {
    return <div>No npcCharacters available.</div>;
  }

  return (
    <Container>
      <StyledHeader>
        <StyledTitle>Pick a NCP</StyledTitle>
        <Button> Add NPC </Button>
      </StyledHeader>
      <ReusableTable
        tableRowsColomns={TABLE_COLUMNS}
        data={npcCharacters}
        isSelectable={false}
        //isSearching={true}
        //mainHeader="Maps"
      />
    </Container>
  );
}
