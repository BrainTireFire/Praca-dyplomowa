import styled, { css } from "styled-components";

type ButtonGroupProps = {
  justify?: "center" | "start" | "end";
  align?: "center" | "start" | "end";
};

const justifies = {
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

const aligns = {
  center: css`
    align-content: center;
  `,
  start: css`
    align-content: flex-start;
  `,
  end: css`
    align-content: flex-end;
  `,
};

const ButtonGroup = styled.div<ButtonGroupProps>`
  display: flex;
  gap: 1.2rem;
  ${(props) => justifies[props.justify || "center"]}
  ${(props) => aligns[props.align || "center"]}
`;

ButtonGroup.defaultProps = {
  justify: "center",
  align: "center",
};

export default ButtonGroup;
