import React from "react";
import Line from "../../../ui/separators/Line";
import Heading from "../../../ui/text/Heading";
import styled from "styled-components";
import MapList from "../../../features/homebrew/maps/MapList";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  align-items: center;
  padding-top: 20px;
`;

export default function HomebrewMap() {
  return (
    <>
      <Container>
        <Heading as="h1">Maps</Heading>
        <Line size="percantage" bold="small" />
      </Container>
      <MapList />
    </>
  );
}
