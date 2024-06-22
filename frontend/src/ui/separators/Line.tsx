import styled, { css } from "styled-components";

type LineProps = {
  size?: "verySmall" | "small" | "medium" | "large";
  align?: "left" | "center" | "right";
};

const sizes = {
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
`;

Line.defaultProps = {
  size: "large",
  align: "center",
};

export default Line;
