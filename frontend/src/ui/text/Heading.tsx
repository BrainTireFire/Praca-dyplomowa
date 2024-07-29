import styled, { css } from "styled-components";

type HeadingProps = {
  as: "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "h7" | "h8";
  align?: "left" | "center" | "right";
  color?: "headerColor" | "textColor";
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

const colors = {
  headerColor: css`
    color: var(--color-header-text);
  `,
  textColor: css`
    color: var(--color-secondary-text);
  `,
};

const Heading = styled.h1<HeadingProps>`
  ${(props) =>
    props.as === "h1" &&
    css`
      font-size: 1.7rem;
      font-weight: 600;
    `}

  ${(props) =>
    props.as === "h2" &&
    css`
      font-size: 2rem;
      font-weight: 600;
    `}

  ${(props) =>
    props.as === "h3" &&
    css`
      font-size: 1.5rem;
      font-weight: 500;
    `}
    ${(props) =>
    props.as === "h4" &&
    css`
      font-size: 1.4rem;
      font-weight: 450;
    `}
    ${(props) =>
    props.as === "h5" &&
    css`
      font-size: 1.3rem;
      font-weight: 400;
    `}
    ${(props) =>
    props.as === "h6" &&
    css`
      font-size: 1.2rem;
      font-weight: 300;
    `}
    ${(props) =>
    props.as === "h7" &&
    css`
      font-size: 1.1rem;
      font-weight: 300;
    `}
    ${(props) =>
    props.as === "h8" &&
    css`
      font-size: 1rem;
      font-weight: 200;
    `}

  ${(props) => aligns[props.align || "center"]}
  ${(props) => colors[props.color || "headerColor"]}
  line-height: 1.4;
`;

Heading.defaultProps = {
  as: "h4",
  align: "center",
  color: "headerColor",
};

export default Heading;
