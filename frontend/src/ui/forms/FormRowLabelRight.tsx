import React, { ReactNode } from "react";
import styled, { css } from "styled-components";

const StyledFormRow = styled.div<StyledFormRowProps>`
  display: flex;
  flex-direction: row;
  align-items: center;
  gap: 2.4rem;

  padding: 0 0;
  ${(props) => props.customStyles}
`;

const Label = styled.label`
  font-weight: 500;
  margin-top: auto;
  margin-bottom: auto;
`;

type FormRowProps = {
  label?: string;
  children: ReactNode;
  customStyles?: ReturnType<typeof css>;
};
type StyledFormRowProps = {
  customStyles?: ReturnType<typeof css>;
};

function FormRowLabelRight({ label, children, customStyles }: FormRowProps) {
  return (
    <StyledFormRow customStyles={customStyles}>
      {children}
      {label && React.isValidElement(children) && (
        <Label htmlFor={children.props.id}>{label}</Label>
      )}
    </StyledFormRow>
  );
}

export default FormRowLabelRight;
