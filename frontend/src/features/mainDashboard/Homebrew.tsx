import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/Button";

const StyledElementBox = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 2rem;
  margin-top: 1.5rem;
`;

const StyledElementBoxWithSpace = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  margin-top: 5rem;
`;

export default function Homebrew() {
  return (
    <>
      <Heading as="h4" align="left">
        Homebrew
      </Heading>
      <StyledElementBox>
        <Button size="large" variation="primary">
          My custom items
        </Button>
        <Button size="large" variation="secondary">
          New Item
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button size="large" variation="primary">
          My custom powers
        </Button>
        <Button size="large" variation="secondary">
          New Power
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button size="large" variation="primary">
          My custom item families
        </Button>
        <Button size="large" variation="secondary">
          New item family
        </Button>
      </StyledElementBox>
      <StyledElementBoxWithSpace>
        <StyledElementBox>
          <Button size="large" variation="primary">
            My custom maps
          </Button>
          <Button size="large" variation="secondary">
            New map
          </Button>
        </StyledElementBox>
        <StyledElementBox>
          <Button size="large" variation="primary">
            My custom encounters
          </Button>
          <Button size="large" variation="secondary">
            New encounter
          </Button>
        </StyledElementBox>
      </StyledElementBoxWithSpace>
    </>
  );
}
