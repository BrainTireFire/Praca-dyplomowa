import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";

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
  const navigate = useNavigate();

  return (
    <>
      <Heading as="h4" align="left">
        Homebrew
      </Heading>
      <StyledElementBox>
        <Button
          size="large"
          variation="primary"
          onClick={() => navigate(`/items`)}
        >
          My custom items
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button
          size="large"
          variation="primary"
          onClick={() => navigate(`/powers`)}
        >
          My custom powers
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button
          size="large"
          variation="primary"
          onClick={() => navigate(`/itemFamilies`)}
        >
          My custom item families
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button
          size="large"
          variation="primary"
          onClick={() => navigate(`/immaterialResources`)}
        >
          My custom immaterial resources
        </Button>
      </StyledElementBox>
      <StyledElementBoxWithSpace>
        <StyledElementBox>
          <Button
            size="large"
            variation="primary"
            onClick={() => navigate(`/homebrew/map`)}
          >
            My custom maps
          </Button>
          <Button
            size="large"
            variation="secondary"
            onClick={() => navigate(`/homebrew/createMap`)}
          >
            New map
          </Button>
        </StyledElementBox>
      </StyledElementBoxWithSpace>
    </>
  );
}
