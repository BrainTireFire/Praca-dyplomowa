import React from "react";
import Box from "../containers/Box";
import RadioButton from "../containers/RadioButton";
import styled from "styled-components";
import Heading from "../text/Heading";

const StyledProficiencyBox = styled.div`
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  padding: 2rem 3rem;
  width: auto;
  height: 85%;
  border-radius: var(--border-radius-lg);
`;

const InputBox = styled.div`
  display: flex;
  justify-content: flex-start;
  gap: 1rem;
  padding: 1rem;
  margin-bottom: 1rem;
  align-items: center;
  width: 100%;
  height: 100%;
  width: 250px;
  height: 35px;
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);

  /* overflow: hidden; */
  font-size: 1.4rem;
  border-radius: var(--border-radius-lg);
`;

export default function ProficiencyBox({ data, header }) {
  return (
    <StyledProficiencyBox>
      {data.map((item) => (
        <InputBox>
          <RadioButton
            //   label="Option 1"
            value="option1"
            //   checked={selectedOption === "option1"}
            //   onChange={handleRadioChange}
          />
          <div>{item.value}</div>
          <div>{item.ability}</div>
          <div>{item.name}</div>
        </InputBox>
      ))}
      <Heading size="h3">{header}</Heading>
    </StyledProficiencyBox>
  );
}
