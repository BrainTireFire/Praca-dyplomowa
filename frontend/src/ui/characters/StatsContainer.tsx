import styled, { css } from "styled-components";
import React, { useState } from "react";
import AttributeBox from "./AttributeBox";

const StyledStatsContainer = styled.div`
  border: 1px solid var(--color-border);
  gap: 3rem;
  width: 200px;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem;
`;

function StatsContainer() {
  const [strength, setStrength] = useState("");

  return (
    <StyledStatsContainer>
      <AttributeBox>
        <AttributeBox.Header>STRENGTH</AttributeBox.Header>
        <AttributeBox.Box>
          <AttributeBox.Input
            type="text"
            id="strength"
            value={strength}
            onChange={(e) => setStrength(e.target.value)}
          />
          <AttributeBox.Circle>+5</AttributeBox.Circle>
        </AttributeBox.Box>
      </AttributeBox>
      <AttributeBox>
        <AttributeBox.Header>DEXTERITY</AttributeBox.Header>
        <AttributeBox.Box>
          <AttributeBox.Input
            type="text"
            id="strength"
            value={strength}
            onChange={(e) => setStrength(e.target.value)}
          />
          <AttributeBox.Circle>+5</AttributeBox.Circle>
        </AttributeBox.Box>
      </AttributeBox>
      <AttributeBox>
        <AttributeBox.Header>CONSTITUTION</AttributeBox.Header>
        <AttributeBox.Box>
          <AttributeBox.Input
            type="text"
            id="strength"
            value={strength}
            onChange={(e) => setStrength(e.target.value)}
          />
          <AttributeBox.Circle>+5</AttributeBox.Circle>
        </AttributeBox.Box>
      </AttributeBox>
      <AttributeBox>
        <AttributeBox.Header>INTELLIGENCE</AttributeBox.Header>
        <AttributeBox.Box>
          <AttributeBox.Input
            type="text"
            id="strength"
            value={strength}
            onChange={(e) => setStrength(e.target.value)}
          />
          <AttributeBox.Circle>+5</AttributeBox.Circle>
        </AttributeBox.Box>
      </AttributeBox>
      <AttributeBox>
        <AttributeBox.Header>WISDOM</AttributeBox.Header>
        <AttributeBox.Box>
          <AttributeBox.Input
            type="text"
            id="strength"
            value={strength}
            onChange={(e) => setStrength(e.target.value)}
          />
          <AttributeBox.Circle>+5</AttributeBox.Circle>
        </AttributeBox.Box>
      </AttributeBox>
      <AttributeBox>
        <AttributeBox.Header>CHARISMA</AttributeBox.Header>
        <AttributeBox.Box>
          <AttributeBox.Input
            type="text"
            id="strength"
            value={strength}
            onChange={(e) => setStrength(e.target.value)}
          />
          <AttributeBox.Circle>+5</AttributeBox.Circle>
        </AttributeBox.Box>
      </AttributeBox>
    </StyledStatsContainer>
  );
}

export default StatsContainer;
