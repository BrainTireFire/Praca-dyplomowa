import React from "react";
import styled from "styled-components";
import Line from "../../../ui/separators/Line";
import Heading from "../../../ui/text/Heading";
import { useTranslation } from "react-i18next";
import EncounterForm from "../../../features/campaigns/encounter/EncounterForm";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
`;

export default function CreateEncounter() {
  const { t } = useTranslation();

  return (
    <Container>
      <Heading as="h1" style={{ padding: "15px" }}>
        {"Encounter Creator"}
      </Heading>
      <Line size="large" bold="medium" />
      <EncounterForm />
    </Container>
  );
}
