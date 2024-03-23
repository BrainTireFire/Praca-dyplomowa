import styled, { css } from "styled-components";
import React, { useState } from "react";
import AttributeBox from "./AttributeBox";

const StyledStatsContainer = styled.div`
  /* border: 1px solid var(--color-border); */
  gap: 3rem;
  width: 200px;
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

function StatsContainer() {
  return (
    <StyledStatsContainer>
      {dataForTest.map((attribute) => (
        <AttributeBox key={attribute.id}>
          <AttributeBox.Header>{attribute.header}</AttributeBox.Header>
          <AttributeBox.Box>
            {/* <AttributeBox.Input
              type="text"
              id={attribute.id}
              value={attribute.value}
              // onChange={(e) => setStrength(e.target.value)}
            /> */}
            <AttributeBox.Text>{attribute.value}</AttributeBox.Text>
            <AttributeBox.Circle>{attribute.modifier}</AttributeBox.Circle>
          </AttributeBox.Box>
        </AttributeBox>
      ))}
    </StyledStatsContainer>
  );
}

export default StatsContainer;
