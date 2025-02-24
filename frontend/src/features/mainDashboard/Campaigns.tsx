import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import Modal from "../../ui/containers/Modal";
import CreateCampaign from "../campaigns/CreateCampaign";
import TextArea from "../../ui/forms/TextArea";
import CampaignsAttended from "../campaigns/CampaignsAttended";
import { useNavigate } from "react-router-dom";

const StyledElementBox = styled.div`
  display: grid;
  grid-template-columns: 3fr 1fr;
  gap: 2rem;
  margin-top: 1.5rem;
`;

export default function Campaigns() {
  const navigate = useNavigate();
  return (
    <>
      <Heading as="h4" align="left">
        Campaigns
      </Heading>
      <StyledElementBox>
        <Button
          size="large"
          variation="primary"
          onClick={() => navigate(`/campaignAttend`)}
        >
          Campaigns I attend
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button
          size="large"
          variation="primary"
          onClick={() => navigate(`/campaigns`)}
        >
          Campaigns I run
        </Button>
        <Modal>
          <Modal.Open opens="CreateCampaign">
            <Button size="large" variation="primary">
              New Campaign
            </Button>
          </Modal.Open>
          <Modal.Window name="CreateCampaign">
            <CreateCampaign />
          </Modal.Window>
        </Modal>
      </StyledElementBox>
    </>
  );
}
