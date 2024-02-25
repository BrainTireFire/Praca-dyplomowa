import React from "react";
import styled from "styled-components";

const StyledAttribute = styled.div`
  border: 1px solid var(--color-border);
  border-radius: var(--border-radius-md);

  padding: 3.2rem;
  display: flex;
  flex-direction: column;
  gap: 2.4rem;
  grid-column: 1 / span 2;
  padding-top: 2.4rem;
  /* padding: 1.6rem;
  display: grid;
  grid-template-columns: 6.4rem 2fr;
  grid-template-rows: auto auto;
  column-gap: 1rem;
  row-gap: 0.4rem; */
`;

export default function Attribute() {
  return <StyledAttribute>Attribute</StyledAttribute>;
}
