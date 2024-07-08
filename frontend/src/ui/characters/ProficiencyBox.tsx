import React from "react";
import Box from "../containers/Box";
import RadioButton from "../containers/RadioButton";
import styled from "styled-components";
import Heading from "../text/Heading";

const StyledProficiencyBox = styled.div`
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  padding: 1rem 1rem;
  width: auto;
  height: 100%;
  border-radius: var(--border-radius-lg);
`;

const InputBox = styled.div`
  display: flex;
  justify-content: flex-start;
  gap: 0.7rem;
  padding: 0.5rem;
  margin-bottom: 0.2rem;
  align-items: center;
  width: auto;
  height: 2rem;
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  white-space: nowrap;
  /* overflow: hidden; */
  font-size: 1rem;
  border-radius: var(--border-radius-lg);
`;

const Radio = styled.input`
  width: 10px;
  height: 10px;
  appearance: none;
  border: 2px solid var(--color-button-primary);
  border-radius: 4px;
  outline: none;

  &:checked {
    background-color: var(--color-button-primary);
    border-color: var(--color-button-primary);
  }
`;

export default function ProficiencyBox({ data, header }) {
  return (
    <StyledProficiencyBox>
      <Heading as="h3">{header}</Heading>
      {data.map((item) => (
        <InputBox key={item.Id}>
          <Radio
            //   label="Option 1"
            type="radio"
            checked={item.proficient}
            //   checked={selectedOption === "option1"}
            //   onChange={handleRadioChange}
          />
          <div>{item.value}</div>
          <div>{item.ability}</div>
          <div>{item.name}</div>
        </InputBox>
      ))}
    </StyledProficiencyBox>
  );
}
