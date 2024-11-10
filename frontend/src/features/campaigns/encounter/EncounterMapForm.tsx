import React, { useState } from "react";
import styled from "styled-components";
import MapBoard from "../../homebrew/maps/MapBoard";
import { Coordinate } from "../../../models/session/Coordinate";
import Box from "../../../ui/containers/Box";
import Heading from "../../../ui/text/Heading";

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
  justify-content: space-between;
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

const SectionContainer = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 5px;
  overflow-y: auto;
  padding-right: 10px;
`;

const Card = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  background-color: var(--color-button-primary);
  padding: 15px;
  border-radius: 10px;
  box-shadow: 0px 4px 8px var(--shadow-md);
  width: 100%;
  margin-bottom: 5px;
`;

export default function EncounterMapForm({ board, fields, campaign }: any) {
  const [selectedBox, setSelectedBox] = useState<Coordinate | null>(null);

  return (
    <MainGrid>
      <GridContainer>
        <MapBoard
          board={board}
          fields={fields}
          selectedBox={selectedBox}
          onSelectedBox={setSelectedBox}
        />
      </GridContainer>
      <LeftPanel>
        <SectionContainer>
          <Heading as="h2">Characters</Heading>
          {campaign &&
            campaign.members.map((member: any) => (
              <Card key={member.id}>{member.name}</Card>
            ))}
        </SectionContainer>
        <SectionContainer>
          <Heading as="h2">NPC</Heading>
        </SectionContainer>
      </LeftPanel>
    </MainGrid>
  );
}
