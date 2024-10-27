import React, { ReactNode } from "react";
import styled from "styled-components";

const StyledFormRow = styled.div`
  display: flex;
  flex-direction: row;
  gap: 2.4rem;

  padding: 0 0;
`;

const Label = styled.label`
  font-weight: 500;
`;

type FormRowProps = {
  label?: string;
  children: ReactNode;
};

function FormRowLabelRight({ label, children }: FormRowProps) {
  return (
    <StyledFormRow>
      {children}
      {label && React.isValidElement(children) && (
        <Label htmlFor={children.props.id}>{label}</Label>
      )}
    </StyledFormRow>
  );
}

export default FormRowLabelRight;
