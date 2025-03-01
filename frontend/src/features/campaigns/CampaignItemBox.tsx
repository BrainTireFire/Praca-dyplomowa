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

  cursor: pointer;
  transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;

  &:hover {
    transform: translateY(-3px);
    box-shadow: var(--shadow-md);
    background: var(--color-navbar-hover);
  }
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
    <Box
      radius="tiny"
      customStyles={BoxCustomStyles}
      onClick={() => navigate(`/campaigns/${campaign.id}`)}
    >
      <Heading as="h1">{campaign.name}</Heading>
      <StyledElementBox>GM: {campaign.gameMaster}</StyledElementBox>
      <StyledElementBox>Description: {campaign.description}</StyledElementBox>
      <div onClick={(e) => e.stopPropagation()}>
        <ButtonGroup justify="center">
          <Modal>
            <Modal.Open opens="ConfirmDelete">
              <Button size="large" onClick={(e) => e.stopPropagation()}>
                Remove
              </Button>
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
