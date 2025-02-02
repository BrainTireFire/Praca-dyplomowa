import React from "react";
import { useParams } from "react-router-dom";
import { useEncounters } from "../hooks/useEncounters";
import Spinner from "../../../ui/interactive/Spinner";
import styled from "styled-components";
import { Encounter } from "../../../models/encounter/Encounter";
import EncounterItemBox from "./EncounterItemBox";

const EncounterListLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 1rem;
  padding-left: 1.5vw;
  padding-right: 1.5vw;
  padding-top: 1.5vh;
`;

export default function EncounterList({ campaignId }: { campaignId: string }) {
  const { isLoading, encounters } = useEncounters(campaignId);

  if (isLoading) {
    return <Spinner />;
  }

  if (!encounters || encounters.length === 0) {
    return <div>There are no encounters</div>;
  }

  return (
    <EncounterListLayout>
      {encounters.map((encounter: Encounter) => (
        <EncounterItemBox
          key={encounter.id}
          encounter={encounter}
          campaignId={parseInt(campaignId)}
        />
      ))}
    </EncounterListLayout>
  );
}
