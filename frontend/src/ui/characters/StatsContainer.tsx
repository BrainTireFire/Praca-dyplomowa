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

const dataForTest = [
  {
    id: "strength",
    header: "STRENGTH",
    value: 10,
    modifier: "+1",
  },
  {
    id: "dexterity",
    header: "DEXTERITY",
    value: 12,
    modifier: "+2",
  },
  {
    id: "constitution",
    header: "CONSTITUTION",
    value: 14,
    modifier: "+3",
  },
  {
    id: "intelligence",
    header: "INTELLIGENCE",
    value: 13,
    modifier: "+2",
  },
  {
    id: "wisdom",
    header: "WISDOM",
    value: 8,
    modifier: "-1",
  },
  {
    id: "charisma",
    header: "CHARISMA",
    value: 10,
    modifier: "+1",
  },
];

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
