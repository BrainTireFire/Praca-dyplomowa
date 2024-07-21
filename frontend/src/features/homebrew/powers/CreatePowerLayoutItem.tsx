import React, { useState } from "react";
import styled from "styled-components";
import Input from "../../../ui/forms/Input";
import Form from "../../../ui/forms/Form";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import FormRow from "../../../ui/forms/FormRow";
import RadioButton from "../../../ui/containers/RadioButton";
import EquipmentTable from "../../characters/tables/EquipmentTable";

const StyledP = styled.p`
  font-size: 24px;
  font-weight: 500;
`;

const Number = styled(StyledP)`
  color: ${({ isOpen }) =>
    isOpen ? "var(--color-button-green)" : "var(--color-button-hover-primary)"};
`;

const Title = styled(StyledP)`
  color: ${({ isOpen }) =>
    isOpen ? "var(--color-button-green)" : "var(--color-secondary-text)"};
`;

const Description = styled(StyledP)`
  color: ${({ isOpen }) =>
    isOpen ? "var(--color-button-green)" : "var(--color-secondary-text)"};
`;

const Icon = styled.p`
  font-size: 40px;
  font-weight: 520;
  color: ${({ isOpen }) =>
    isOpen ? "var(--color-button-green)" : "var(--color-button-primary)"};
`;

const Item = styled.div`
  border-bottom: 4px solid
    ${({ isOpen }) =>
      isOpen ? "var(--color-button-green)" : "var(--color-link)"};
`;

const ItemOpen = styled.div`
  box-shadow: 0 0 30px rgba(0, 0, 0, 0.1);
  padding: 20px 24px;
  padding-right: 48px;
  cursor: pointer;
  border-top: 4px solid
    ${({ isOpen }) =>
      isOpen ? "var(--color-button-green)" : "var(--color-link)"};
  border-bottom: 2px solid
    ${({ isOpen }) =>
      isOpen ? "var(--color-button-green)" : "var(--color-link)"};

  display: grid;
  grid-template-columns: 1fr auto;
  column-gap: 24px;
  row-gap: 32px;
  align-items: center;
`;

const ContentBox = styled.div`
  display: grid;
  grid-template-columns: auto;
  gap: 24px;
  padding: 24px;
`;

export default function CreatePowerLayoutItem({
  title,
  description,
  num,
  children,
}) {
  const [isOpen, setIsOpen] = useState(false);

  const toggleOpen = () => {
    setIsOpen(!isOpen);
  };

  return (
    <Item isOpen={isOpen}>
      <ItemOpen isOpen={isOpen} onClick={toggleOpen}>
        {/* <Number isOpen={isOpen}>{num < 9 ? `0${num + 1}` : num + 1}</Number> */}
        <Title isOpen={isOpen}>{title}</Title>
        {/* <Description isOpen={isOpen}>{description}</Description> */}
        <Icon isOpen={isOpen}>{isOpen ? "-" : "+"}</Icon>
      </ItemOpen>
      {isOpen && (
        <ContentBox>
          <EquipmentTable />
        </ContentBox>
      )}
    </Item>
  );
}
