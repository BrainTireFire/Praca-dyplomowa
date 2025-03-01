import styled, { css } from "styled-components";

type ButtonProps = {
  size?: "small" | "medium" | "large" | "verylarge";
  variation?: "primary" | "secondary" | "danger";
  customStyles?: ReturnType<typeof css>;
};

const sizes = {
  small: css`
    font-size: 1.2rem;
    padding: 0.4rem 0.8rem;
    text-transform: uppercase;
    font-weight: 600;
    text-align: center;
  `,
  medium: css`
    font-size: 1.4rem;
    padding: 1.2rem 1.6rem;
    font-weight: 500;
  `,
  likeInput: css`
    font-size: 1.4rem;
    padding: 0.8rem 1.2rem;
    font-weight: 500;
  `,
  large: css`
    font-size: 1.6rem;
    padding: 1.2rem 2.4rem;
    font-weight: 500;
  `,
  verylarge: css`
    font-size: 3rem;
    padding: 2.3rem 8rem;
    font-weight: 500;
  `,
};

const variations = {
  primary: css`
    color: var(--color-button-text);
    background-color: var(--color-button-primary);
    &:disabled {
      background-color: var(--color-button-primary-disabled);
    }

    &:hover {
      background-color: var(--color-button-hover-primary);
    }
    &:hover:disabled {
      background-color: var(--color-button-primary-disabled);
    }
  `,
  secondary: css`
    color: var(--color-button-text);
    background: var(--color-button-secondary);
    border: 1px solid var(--color-button-secondary);

    &:hover {
      background-color: var(--color-button-hover-secondary);
    }
  `,
  danger: css`
    color: var(--color-button-text);
    background-color: var(--color-button-danger);

    &:hover {
      background-color: var(--color-button-hover-danger);
    }
  `,
};

const Button = styled.button<ButtonProps>`
  border: none;
  border-radius: var(--border-radius-sm);
  box-shadow: var(--shadow-sm);

  ${(props) => sizes[props.size || "medium"]}
  ${(props) => variations[props.variation || "primary"]}
  /* Custom styles */

  ${(props) => props.customStyles}
`;

Button.defaultProps = {
  variation: "primary",
  size: "medium",
  customStyles: css``,
};

export default Button;
