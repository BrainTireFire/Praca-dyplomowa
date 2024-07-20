import styled from "styled-components";
import { ChangeEvent, useState } from "react";
import { useDarkMode } from "../../context/DarkModeContext";

const Label = styled.label`
  display: flex;
  align-items: center;
  gap: 10px;
  cursor: pointer;
`;

const Switch = styled.div`
  position: relative;
  width: 68px;
  height: 32px;
  background: var(--color-button-hover-primary);
  border-radius: 32px;
  padding: 4px;
  transition: 300ms all;

  &:before {
    transition: 300ms all;
    content: "";
    position: absolute;
    width: 28px;
    height: 28px;
    border-radius: 35px;
    top: 50%;
    left: 4px;
    background: var(--color-secondary-background);
    transform: translate(0, -50%);
  }
`;

const Input = styled.input`
  opacity: 0;
  position: absolute;

  &:checked + ${Switch} {
    background: var(--color-button-hover-secondary);

    &:before {
      transform: translate(32px, -50%);
    }
  }
`;

const ToggleSwitch = () => {
  const { isDarkMode, toggleDarkMode } = useDarkMode();

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    toggleDarkMode();
  };

  return (
    <Label>
      <Input checked={isDarkMode} type="checkbox" onChange={handleChange} />
      <Switch />
    </Label>
  );
};

export default ToggleSwitch;
