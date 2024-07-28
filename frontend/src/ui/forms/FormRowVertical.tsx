import React, { ReactNode } from "react";
import styled, { css } from "styled-components";

const StyledFormRow = styled.div<StyledFormRowProps>`
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
  padding: 1.2rem 0;
  ${(props) => (props.fillHeight ? heights["expanding"] : heights["normal"])}
`;

const heights = {
  normal: css``,
  expanding: css`
    flex-grow: 1;
    height: 100%;
  `,
};

const Label = styled.label`
  font-weight: 500;
  font-size: 1.3rem;
  height: 2rem;
`;
const PadLabel = styled.div`
  height: 2rem;
`;
const AssistiveText = styled.label`
  font-weight: 500;
  font-size: 1.1rem;
  height: 1.2rem;
`;
const PadAssistiveText = styled.div`
  height: 1.2rem;
`;

const Error = styled.span`
  font-size: 1.4rem;
  color: var(--color-form-error);
  margin-top: 0.2rem;
`;

type FormRowVerticalProps = {
  label?: string;
  padlabel?: boolean;
  assistiveText?: string;
  padassistiveText?: boolean;
  error?: string;
  children: ReactNode;
  fillHeight?: boolean;
};
type StyledFormRowProps = {
  fillHeight?: boolean;
};

function FormRowVertical({
  label,
  padlabel,
  assistiveText,
  padassistiveText,
  error,
  children,
  fillHeight,
}: FormRowVerticalProps) {
  return (
    <StyledFormRow fillHeight={fillHeight}>
      {label && React.isValidElement(children) && (
        <Label htmlFor={children.props.id}>{label}</Label>
      )}
      {padlabel && <PadLabel></PadLabel>}
      {children}
      {assistiveText && React.isValidElement(children) && (
        <AssistiveText htmlFor={children.props.id}>
          {assistiveText}
        </AssistiveText>
      )}
      {padassistiveText && <PadAssistiveText></PadAssistiveText>}
      {error && <Error>{error}</Error>}
    </StyledFormRow>
  );
}

export default FormRowVertical;
