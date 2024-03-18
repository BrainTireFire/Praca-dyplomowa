import styled, { css } from "styled-components";

type HeadingProps = {
  as: "h1" | "h2" | "h3" | "h4";
  align?: "left" | "center" | "right";
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

const Heading = styled.h1<HeadingProps>`
  ${(props) =>
    props.as === "h1" &&
    css`
      font-size: 3rem;
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
      font-size: 2rem;
      font-weight: 500;
    `}
    ${(props) =>
    props.as === "h4" &&
    css`
      font-size: 3rem;
      font-weight: 600;
      margin-bottom: 15px;
    `}

  ${(props) => aligns[props.align || "center"]}
  line-height: 1.4;
  color: var(--color-header-text);
`;

Heading.defaultProps = {
  as: "h4",
  align: "center",
};

export default Heading;
