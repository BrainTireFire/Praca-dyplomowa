import React from "react";
import styled from "styled-components";
import Line from "../../../ui/separators/Line";
import Heading from "../../../ui/text/Heading";
import { useTranslation } from "react-i18next";
import EncounterForm from "../../../features/campaigns/encounter/EncounterForm";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  align-items: center;
  margin-top: 20px;
`;

export default function Encounter() {
  const { t } = useTranslation();

  return (
    <>
      <Container>
        <Heading as="h1">{"Encounter Creator"}</Heading>
        <Line size="large" bold="medium" />
      </Container>
      <EncounterForm />
    </>
  );
}
