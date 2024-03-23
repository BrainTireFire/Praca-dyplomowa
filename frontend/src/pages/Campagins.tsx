import React from "react";
import styled from "styled-components";
import Heading from "../ui/text/Heading";
import Line from "../ui/separators/Line";
import Box from "../ui/containers/Box";
import ButtonGroup from "../ui/interactive/ButtonGroup";
import Button from "../ui/interactive/Button";
import LinkContainer from "../ui/containers/LinkContainer";
import { Link } from "react-router-dom";

const CampaginsLayout = styled.main`
  min-height: 5vh;
  display: grid;
  align-content: center;
  justify-content: center;
  gap: 3rem;
`;

export default function Campagins() {
  return (
    <CampaginsLayout>
      <div>
        <Heading as="h4">My campaigns</Heading>
        <Line size="small" />
      </div>
      <Box radius="tiny" variation="rectangleLarge">
        <Heading as="h4">My campaigns</Heading>
        <p>Test test eetstasaf jhaosfhasjhfjkashfkjash hfj</p>
        <ButtonGroup align="end">
          <Button variation="primary">tesst</Button>
          <Button variation="primary">test</Button>
          <Button variation="primary">test</Button>
        </ButtonGroup>
      </Box>
    </CampaginsLayout>
  );
}
