import React from "react";
import Heading from "../../../ui/text/Heading";
import styled from "styled-components";

const StyledStatistics = styled.div`
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr;
  gap: 3rem;
  margin-top: 3rem;
`;

const StyledStatisticsRow = styled.div`
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr 1fr;
  gap: 7rem;
  margin-left: 5rem;
`;

export default function Statistics() {
  return (
    <>
      <Heading as="h4">Statistics</Heading>
      <StyledStatistics>
        <StyledStatisticsRow>
          <Heading as="h3" align="left">
            Games played:
          </Heading>
          3000
        </StyledStatisticsRow>

        <StyledStatisticsRow>
          <Heading as="h3" align="left">
            Games won:
          </Heading>
          200
        </StyledStatisticsRow>

        <StyledStatisticsRow>
          <Heading as="h3" align="left">
            Account created:
          </Heading>
          1000
        </StyledStatisticsRow>
      </StyledStatistics>
    </>
  );
}
