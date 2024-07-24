import styled from "styled-components";
import Heading from "../../../ui/text/Heading";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 12px;
`;

function CreateShop() {
  return (
    <Container>
      <Heading as="h1">Name</Heading>
      <Input placeholder="Name of the store" style={{ width: "200px" }}></Input>
      <Heading as="h1">Type</Heading>
      <Input placeholder="Type of the store" style={{ width: "200px" }}></Input>
      <Heading as="h1">Location</Heading>
      <Input
        placeholder="Location of the store"
        style={{ width: "200px" }}
      ></Input>
      <Heading as="h1">Description</Heading>
      <Input style={{ width: "200px" }}></Input>
      <Button size="large">Create</Button>
    </Container>
  );
}

export default CreateShop;
