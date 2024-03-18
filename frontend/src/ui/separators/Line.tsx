import styled, { css } from "styled-components";

type LineProps = {
  size?: "small" | "medium" | "large";
};

const sizes = {
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

const Line = styled.hr<LineProps>`
  border: none;
  background-color: var(--color-header-text);
  ${(props) => sizes[props.size || "large"]}
`;

Line.defaultProps = {
  size: "large",
};

export default Line;
