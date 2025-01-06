import styled, { css } from "styled-components";
import React, { useState } from "react";
import AttributeBox from "./AttributeBox";
import { Attribute } from "../../models/attribute";

const StyledStatsContainer = styled.div`
  /* border: 1px solid var(--color-border); */
  gap: 1rem;
  width: auto;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem;
`;

function StatsContainer({ stats }: { stats: Attribute[] }) {
  return (
    <StyledStatsContainer>
      {stats?.map((attribute) => (
        <AttributeBox key={attribute.name} attribute={attribute}></AttributeBox>
      ))}
    </StyledStatsContainer>
  );
}

export default StatsContainer;
