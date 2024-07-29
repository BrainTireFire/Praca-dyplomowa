import styled from "styled-components";
import Heading from "../../../ui/text/Heading";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import TextArea from "../../../ui/forms/TextArea";
import { useState } from "react";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 12px;
`;

function CreateShop() {
  const [shop, setShop] = useState({
    id: Date.now(), // CHANGE THIS ?
    name: "",
    type: "",
    location: "",
    description: "",
  });

  const handleChange = (e) => {
    setShop((previous) => ({
      ...previous,
      [e.target.name]: e.target.value,
    }));
  };

  const handleClick = () => {
    // REQUEST TO DB
    console.log(shop);
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
