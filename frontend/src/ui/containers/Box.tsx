import styled, { css } from "styled-components";

const Box = styled.div`
  padding: 4rem 6rem;

  /* Box */
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  border-radius: var(--border-radius-md);
  overflow: hidden;
  font-size: 1.4rem;
  witdh: 500px;
  height: 500px;
`;

export default Box;
