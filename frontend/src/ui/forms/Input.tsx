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
  border: 1px solid var(--color-navbar-border);
  background-color: font-variant(--color-secondary-background);
  border-radius: var(--border-radius-sm);
  box-shadow: var(--shadow-sm);
  padding: 0.8rem 1.2rem;

  ${(props) => sizes[props.size || "medium"]}
  ${(props) => heights[props.height || "normal"]}
  ${(props) => props.customStyles}

  &[type="radio"],
  &[type="checkbox"] {
    appearance: none;
    width: 1.1rem;
    height: 1.1rem;
    border: 1px solid var(--color-navbar-border);
    background-color: var(--color-secondary-background);
    position: relative;
    padding: 0.7rem;
    margin: 0;
  }

  &[type="radio"] {
    border-radius: 50%;

    &:checked::after {
      content: "";
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      width: 0.9rem;
      height: 0.9rem;
      border-radius: 50%;
      background-color: #555;
    }

    &:disabled {
      border-color: #999;
      background-color: #ccc;
    }

    &:disabled:checked::after {
      background-color: #333;
    }
  }

  &[type="checkbox"] {
    border-radius: 3px;

    &:checked::after {
      content: "";
      position: absolute;
      top: 50%;
      left: 50%;
      width: 0.4rem;
      height: 0.75rem;
      border: solid #555;
      border-width: 0 0.2rem 0.2rem 0;
      transform: translate(-50%, -50%) rotate(45deg);
    }

    &:disabled {
      border-color: #999;
      background-color: #ccc;
    }

    &:disabled:checked::after {
      border-color: #333;
    }
  }
`;



Input.defaultProps = {
  size: "medium",
  customStyles: css``,
  height: "normal",
};

export default Input;
