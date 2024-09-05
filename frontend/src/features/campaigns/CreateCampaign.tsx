import { useState } from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Input from "../../ui/forms/Input";
import TextArea from "../../ui/forms/TextArea";
import Button from "../../ui/interactive/Button";
import toast from "react-hot-toast";
import useCreateCampaign from "./hooks/useCreateCampaign";
import Spinner from "../../ui/interactive/Spinner";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 12px;
`;

function CreateCampaign({ onCloseModal }) {
  const [campaign, setCampaign] = useState({
    name: "",
    description: "",
    invitationLink: "http://ddbutbetter.com/join/",
  });

  const { createCampaign, isPending } = useCreateCampaign();

  if (isPending) {
    return <Spinner />;
  }

  const handleChange = (e) => {
    setCampaign((previous) => ({
      ...previous,
      [e.target.name]: e.target.value,
    }));

    //May be temporary (appending invitation link as name of campaign)
    if (e.target.name === "name") {
      setCampaign((previous) => ({
        ...previous,
        invitationLink: previous.invitationLink + previous.name,
      }));
    }
  };

  const handleClick = () => {
    createCampaign(campaign);
    onCloseModal();
  };

  return (
    <Container>
      <Heading as="h1">Name</Heading>
      <Input
        name="name"
        placeholder="Name of the campaign"
        style={{ width: "200px" }}
        value={campaign.name}
        onChange={handleChange}
      ></Input>
      <Heading as="h1">Description</Heading>
      <TextArea
        name="description"
        style={{ width: "300px", height: "150px" }}
        value={campaign.description}
        onChange={handleChange}
      ></TextArea>
      <Button size="large" onClick={handleClick}>
        Create
      </Button>
    </Container>
  );
}

export default CreateCampaign;
