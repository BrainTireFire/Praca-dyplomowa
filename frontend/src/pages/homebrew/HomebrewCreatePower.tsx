import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Line from "../../ui/separators/Line";
import CreatePowerLayout from "../../features/homebrew/powers/CreatePowerLayout";

const data = [
  {
    title: "Basic",
    description: "Basic questions",
  },
  {
    title: "Effect table",
    description: "Questions about the effect table",
  },
  {
    title: "Do you ship to countries outside the EU?",
    description: "Yes, we ship worldwide.",
  },
];

const HomebrewCreatePowerLayout = styled.main`
  min-height: 5vh;
  display: grid;
  align-content: center;
  justify-content: center;
  gap: 3rem;
`;

export default function HomebrewCreatePower() {
  return (
    <HomebrewCreatePowerLayout>
      <div>
        <Heading as="h4">Create new power</Heading>
        <Line size="small" />
      </div>
      <div>
        <CreatePowerLayout data={data} />
      </div>
    </HomebrewCreatePowerLayout>
  );
}