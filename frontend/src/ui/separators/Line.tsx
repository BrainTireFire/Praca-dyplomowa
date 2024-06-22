import styled, { css } from "styled-components";

type LineProps = {
  size?: "percantage" | "verySmall" | "small" | "medium" | "large";
  align?: "left" | "center" | "right";
  bold?: "small" | "medium" | "large";
};

const sizes = {
  percantage: css`
    width: 100%;
    height: 1px;
  `,
  verySmall: css`
    width: 70vw;
    height: 1px;
  `,
  small: css`
    width: 80vw;
    height: 1px;
  `,
  medium: css`
    width: 85vw;
    height: 3px;
  `,
  large: css`
    width: 90vw;
    height: 5px;
  `,
};

const bolds = {
  small: css`
    height: 1px;
  `,
  medium: css`
    height: 3px;
  `,
  large: css`
    height: 5px;
  `,
};

const aligns = {
  left: css`
    text-align: left;
  `,
  center: css`
    text-align: center;
  `,
  right: css`
    text-align: right;
  `,
};

const Line = styled.hr<LineProps>`
  border: none;
  background-color: var(--color-header-text);
  ${(props) => sizes[props.size || "large"]}
  ${(props) => aligns[props.align || "center"]}
  ${(props) => bolds[props.bold || "small"]}
`;

Line.defaultProps = {
  size: "large",
  align: "center",
  bold: "small",
};

export default Line;
