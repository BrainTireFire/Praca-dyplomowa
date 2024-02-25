import React from "react";
import Attribute from "./Attribute";
import styled from "styled-components";

const StyledAttributes = styled.div`
  border: 1px solid var(--color-border);
`;

export default function Attributes() {
  return (
    <StyledAttributes>
      <Attribute />
      <Attribute />
      <Attribute />
    </StyledAttributes>
  );
}
