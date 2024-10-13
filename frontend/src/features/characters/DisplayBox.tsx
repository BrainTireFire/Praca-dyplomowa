import React from "react";
import Box from "../../ui/containers/Box";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";

const Grid = styled.div`
  display: grid;
  grid-template-rows: auto, 1rem;
`;

const Body = styled.div`
  grid-row-start: 1;
  grid-row-end: 2;
`;
const Label = styled.div`
  grid-row-start: 2;
  grid-row-end: 3;
`;

export const DisplayBoxContent = styled.div`
  font-size: 2rem;
  text-align: center;
`;

export default function DisplayBox({
  label,
  children,
}: {
  label: string;
  children: React.ReactNode;
}) {
  return (
    <Box radius="tiny">
      <Grid>
        <Body>{children}</Body>
        <Heading as="h3">{label}</Heading>
      </Grid>
    </Box>
  );
}
