import styled, { css } from "styled-components";

type LineProps = {
  size?: "percantage" | "verySmall" | "small" | "medium" | "large";
  bold?: "small" | "medium" | "large";
};

const sizes = {
  percantage: css`
    height: 100%;
    width: 1px;
  `,
  verySmall: css`
    height: 70vh;
    width: 1px;
  `,
  small: css`
    height: 80vh;
    width: 1px;
  `,
  medium: css`
    height: 85vh;
    width: 3px;
  `,
  large: css`
    height: 90vh;
    width: 5px;
  `,
};

const bolds = {
  small: css`
    width: 1px;
  `,
  medium: css`
    width: 3px;
  `,
  large: css`
    width: 5px;
  `,
};

const VerticalLine = styled.div<LineProps>`
  border: none;
  background-color: var(--color-header-text);
  margin-right: 5px;
  margin-left: 5px;
  ${(props) => sizes[props.size || "large"]}
  ${(props) => bolds[props.bold || "small"]}
`;

VerticalLine.defaultProps = {
  size: "percantage",
  bold: "small",
};

export default VerticalLine;
