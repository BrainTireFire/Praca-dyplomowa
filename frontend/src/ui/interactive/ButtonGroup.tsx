import styled, { css } from "styled-components";
import Button from "./Button";

type ButtonGroupProps = {
  align?: "center" | "start" | "end";
};

const aligns = {
  center: css`
    justify-content: center;
  `,
  start: css`
    justify-content: flex-start;
  `,
  end: css`
    justify-content: flex-end;
  `,
};

const ButtonGroup = styled.div<ButtonGroupProps>`
  display: flex;
  gap: 1.2rem;
  ${(props) => aligns[props.align || "center"]}
`;

ButtonGroup.defaultProps = {
  align: "center",
};

export default ButtonGroup;
