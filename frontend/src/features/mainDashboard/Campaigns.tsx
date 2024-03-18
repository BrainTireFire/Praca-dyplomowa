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

export default function Campaigns() {
  return (
    <>
      <Heading as="h4" align="left">
        Campaigns
      </Heading>
      <StyledElementBox>
        <Button size="large" variation="primary">
          Campaigns I attend
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button size="large" variation="primary">
          Campaigns I run
        </Button>
        <Button size="large" variation="secondary">
          New campaign
        </Button>
      </StyledElementBox>
    </>
  );
}
