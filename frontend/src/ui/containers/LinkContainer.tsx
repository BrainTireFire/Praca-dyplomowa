import styled, { css } from "styled-components";

type LinkContainerProps = {
  variation?: "center" | "right";
};

const variations = {
  center: css`
    text-align: center;
  `,
  right: css`
    text-align: right;
  `,
};

const LinkContainer = styled.p<LinkContainerProps>`
  ${(props) => variations[props.variation || "right"]}
`;

LinkContainer.defaultProps = {
  variation: "right",
};

export default LinkContainer;
