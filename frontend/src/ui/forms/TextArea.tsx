import styled, { css } from "styled-components";

type TextAreaProps = {
  size?: "small" | "medium";
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

const TextArea = styled.textarea<TextAreaProps>`
  border: 1px solid var(--color-grey-300);
  background-color: font-variant(--color-grey-0);
  border-radius: var(--border-radius-sm);
  box-shadow: var(--shadow-sm);
  padding: 0.8rem 1.2rem;
  resize: none;
  width: 100%; // Take full width of the parent
  height: 100%; // Take full height of the parent if required
  flex-grow: 1; // Allow it to grow within flex containers

  ${(props) => sizes[props.size || "medium"]}
  /* Custom styles */

  ${(props) => props.customStyles}
`;

TextArea.defaultProps = {
  size: "medium",
  customStyles: css``,
};

export default TextArea;
