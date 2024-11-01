import React from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";
import { Navigate, useNavigate } from "react-router-dom";

const BoxCustomStyles = css`
  display: grid;
  grid-template-rows: 0.5fr 0.5fr 1.5fr 0.5fr;
`;

const StyledElementBox = styled.div`
  text-align: center;
`;

export default function CampaignItemBox({ campaign }) {
  const navigate = useNavigate();

  return (
    <Box radius="tiny" customStyles={BoxCustomStyles}>
      <Heading as="h4">{campaign.name}</Heading>
      <StyledElementBox>{campaign.player}</StyledElementBox>
      <StyledElementBox>{campaign.description}</StyledElementBox>
      <div>
        <ButtonGroup justify="center">
          <Button
            variation="primary"
            size="large"
            onClick={() => navigate(`/campaigns/${campaign.id}`)}
          >
            View
          </Button>
          <Button variation="primary" size="large">
            Edit
          </Button>
          <Button variation="primary" size="large">
            Remove
          </Button>
        </ButtonGroup>
      </div>
    </Box>
  );
}
