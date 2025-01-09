import styled from "styled-components";
import Heading from "../../../ui/text/Heading";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import TextArea from "../../../ui/forms/TextArea";
import { ShopInsertDto } from "../../../models/shop";
import { useContext, useState } from "react";
import { useParams } from "react-router-dom";
import useCreateShop from "./hooks/useCreateShop";
import Spinner from "../../../ui/interactive/Spinner";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 12px;
`;

function CreateShop({ onCloseModal }: { onCloseModal: () => void }) {
  const { createShop, isPending } = useCreateShop();
  const { campaignId } = useParams<{ campaignId: string }>();
  const [shop, setShop] = useState<ShopInsertDto>({
    name: "",
    type: "",
    location: "",
    description: "",
    campaignId: Number(campaignId),
  });

  if (isPending) {
    return <Spinner />;
  }

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setShop((previous) => ({
      ...previous,
      [e.target.name]: e.target.value,
    }));
  };

  const handleClick = () => {
    createShop(shop);
    onCloseModal();
  };

  return (
    <Container>
      <Heading as="h1">Name</Heading>
      <Input
        name="name"
        placeholder="Name of the store"
        style={{ width: "200px" }}
        value={shop.name}
        onChange={handleChange}
      ></Input>
      <Heading as="h1">Type</Heading>
      <Input
        name="type"
        placeholder="Type of the store"
        style={{ width: "200px" }}
        value={shop.type}
        onChange={handleChange}
      ></Input>
      <Heading as="h1">Location</Heading>
      <Input
        name="location"
        placeholder="Location of the store"
        style={{ width: "200px" }}
        value={shop.location}
        onChange={handleChange}
      ></Input>
      <Heading as="h1">Description</Heading>
      <TextArea
        name="description"
        style={{ width: "300px", height: "150px" }}
        value={shop.description}
        onChange={handleChange}
      ></TextArea>
      <Button size="large" onClick={handleClick}>
        Create
      </Button>
    </Container>
  );
}

export default CreateShop;
