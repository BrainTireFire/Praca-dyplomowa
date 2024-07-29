import styled, { css } from "styled-components";

type InputProps = {
  size?: "small" | "medium";
  height?: "normal" | "tall";
  customStyles?: ReturnType<typeof css>;
};

const sizes = {
  small: css`
    font-size: 1rem;
    padding: 0.4rem 0.8rem;
    font-weight: 300;
  `,
  medium: css`
    font-size: 1.4rem;
    padding: 0.8rem 1.2rem;
    font-weight: 500;
  `,
};

const heights = {
  normal: css``,
  tall: css`
    height: 100%;
  `,
};

const Input = styled.input<InputProps>`
  border: 1px solid var(--color-grey-300);
  background-color: font-variant(--color-grey-0);
  border-radius: var(--border-radius-sm);
  box-shadow: var(--shadow-sm);
  padding: 0.8rem 1.2rem;

  ${(props) => sizes[props.size || "medium"]}
  ${(props) => heights[props.height || "normal"]}
  /* Custom styles */

  ${(props) => props.customStyles}
`;

Input.defaultProps = {
  size: "medium",
  customStyles: css``,
  height: "normal",
};

export default Input;
