import React from "react";
import styled from "styled-components";
import Heading from "../ui/text/Heading";
import Line from "../ui/separators/Line";

const CampaginsLayout = styled.main`
  min-height: 5vh;
  display: grid;
  align-content: center;
  justify-content: center;
  gap: 2rem;
`;

export default function Campagins() {
  return (
    <CampaginsLayout>
      <div>
        <Heading as="h4">My campaigns</Heading>
        <Line size="small" />
      </div>
    </CampaginsLayout>
  );
}
