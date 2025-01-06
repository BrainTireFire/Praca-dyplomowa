import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";
import { useNavigate } from "react-router-dom";
import { Campaign } from "../../models/campaign";
import Modal from "../../ui/containers/Modal";
import { useTranslation } from "react-i18next";
import ConfirmDelete from "../../ui/containers/ConfirmDelete";
import useRemoveCampaign from "./hooks/useRemoveCampaign";
import Spinner from "../../ui/interactive/Spinner";

const BoxCustomStyles = css`
  display: grid;
  grid-template-rows: 0.5fr 0.5fr 1.5fr 0.5fr;
`;

const StyledElementBox = styled.div`
  text-align: center;
`;

export default function CampaignItemBox({ campaign }: { campaign: Campaign }) {
  const navigate = useNavigate();
  const { removeCampaign, isPending } = useRemoveCampaign();

  if (isPending) {
    return <Spinner />;
  }

  return (
    <Box radius="tiny" customStyles={BoxCustomStyles}>
      <Heading as="h1">{campaign.name}</Heading>
      <StyledElementBox>
        {campaign.gameMaster?.name ?? "GameMaster"}
      </StyledElementBox>
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
          <Modal>
            <Modal.Open opens="ConfirmDelete">
              <Button size="large">Remove</Button>
            </Modal.Open>
            <Modal.Window name="ConfirmDelete">
              <ConfirmDelete
                resourceName={campaign.name + " campaign"}
                onConfirm={() => removeCampaign(campaign.id)}
              />
            </Modal.Window>
          </Modal>
        </ButtonGroup>
      </div>
    </Box>
  );
}
