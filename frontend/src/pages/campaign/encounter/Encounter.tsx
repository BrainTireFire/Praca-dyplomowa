import React from "react";
import styled from "styled-components";
import Heading from "../../../ui/text/Heading";
import Line from "../../../ui/separators/Line";
import EncounterList from "../../../features/campaigns/encounter/EncounterList";
import Button from "../../../ui/interactive/Button";
import { useNavigate, useParams } from "react-router-dom";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
`;

const ButtonContainer = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 2rem;
`;

export default function Encounter() {
  const { campaignId } = useParams<{ campaignId: string }>();
  const navigate = useNavigate();

  return (
    <>
      <Container>
        <Heading as="h1" style={{ padding: "15px" }}>
          {"Encounters "}
        </Heading>
        <Line size="large" bold="medium" />
        <EncounterList campaignId={campaignId} />
        <ButtonContainer>
          <Button
            size="large"
            onClick={() => navigate(`/campaigns/${campaignId}/createEncounter`)}
          >
            Create encounter
          </Button>
        </ButtonContainer>
      </Container>
    </>
  );
}
