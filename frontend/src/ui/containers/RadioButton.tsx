// RadioButton.js
import React from "react";
import styled from "styled-components";

const RadioButtonWrapper = styled.label`
  display: flex;
  align-items: center;
  margin-right: 16px;
  cursor: pointer;
`;

const RadioInput = styled.input`
  margin-right: 8px;
  width: 16px;
  height: 16px;
  appearance: none;
  border: 2px solid var(--color-button-primary);
  border-radius: 4px;
  outline: none;

  &:checked {
    background-color: var(--color-button-primary);
    border-color: var(--color-button-primary);
  }
`;

const RadioLabel = styled.span`
  font-size: 16px;
`;

const RadioButton = ({ label, checked, onChange }) => {
  return (
    <RadioButtonWrapper>
      <RadioInput type="radio" checked={checked} onChange={onChange} />
      <RadioLabel>{label}</RadioLabel>
    </RadioButtonWrapper>
  );
};

export default RadioButton;
