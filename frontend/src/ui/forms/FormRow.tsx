import React, { ReactNode } from "react";
import styled from "styled-components";

const StyledFormRow = styled.div`
  display: grid;
  align-items: center;
  grid-template-columns: 24rem 1fr 1.2fr;
  gap: 2.4rem;

  padding: 1.2rem 0;

  &:first-child {
    padding-top: 0;
  }

  &:last-child {
    padding-bottom: 0;
  }

  &:not(:last-child) {
    border-bottom: 1px solid var(--color-border);
  }

  &:has(button) {
    display: flex;
    justify-content: flex-end;
    gap: 1.2rem;
  }
`;

const StyledFormRow2 = styled.div`
  display: grid;
  align-items: center;
  grid-template-columns: 24rem 1fr 1fr 1fr 1fr;
  gap: 2.4rem;

  padding: 1.2rem 0;

  &:first-child {
    padding-top: 0;
  }

  &:last-child {
    padding-bottom: 0;
  }

  &:not(:last-child) {
    border-bottom: 1px solid var(--color-border);
  }

  &:has(button) {
    display: flex;
    justify-content: flex-end;
    gap: 1.2rem;
  }
`;

const Label = styled.label`
  font-weight: 500;
`;

const Error = styled.span`
  font-size: 1.4rem;
  color: var(--color-form-error);
`;

type FormRowProps = {
  label?: string;
  error?: string;
  children: ReactNode;
};

export function FormRow({ label, error, children }: FormRowProps) {
  return (
    <StyledFormRow>
      {label && React.isValidElement(children) && (
        <Label htmlFor={children.props.id}>{label}</Label>
      )}
      {children}
      {error && <Error>{error}</Error>}
    </StyledFormRow>
  );
}
export function FormRow2({ label, children }: FormRowProps) {
  return (
    <StyledFormRow2>
      {label && React.isValidElement(children) && (
        <Label htmlFor={children.props.id}>{label}</Label>
      )}
      {children}
    </StyledFormRow2>
  );
}
