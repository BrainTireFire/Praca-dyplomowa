import styled, { css } from "styled-components";
import Line from "../../../ui/separators/Line";
import Heading from "../../../ui/text/Heading";
import Input from "../../../ui/forms/Input";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import { HiXMark } from "react-icons/hi2";
import Button from "../../../ui/interactive/Button";
import { useState } from "react";
import { Navigate, useLocation, useNavigate } from "react-router-dom";
import Modal from "../../../ui/containers/Modal";
import CreateShop from "./CreateShop";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  align-items: center;
`;

const SearchFormContainer = styled.div`
  display: flex;
  justify-content: space-around;
  flex-direction: row;
`;

const Table = styled.table`
  width: 90%;
  border-radius: var(--border-radius-md);
  border: 1px solid var(--color-border);
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border-spacing: 0;
`;

const Th = styled.th`
  padding: 10px;
`;

const Tr = styled.tr`
  transition: background-color 100ms ease;
  &:hover {
    background-color: rgba(116, 177, 116, 0.5);
  }
`;

const Td = styled.td`
  padding: 8px;
  border-top: 1px solid var(--color-border);
  text-align: center;
`;

export default function Shops() {
  const location = useLocation();
  const { shops } = location.state;
  const [searchInputs, setSearchInputs] = useState({
    name: "",
    type: "",
    location: "",
  });
  console.log(shops);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    if (value.length < 3) {
      setSearchInputs((previous) => ({ ...previous, [name]: "" }));
      return;
    }
    setSearchInputs((previous) => ({ ...previous, [name]: value }));
  };

  const filterShopsData = shops.filter((shop) => {
    return Object.keys(searchInputs).every((key) =>
      shop[key].toLowerCase().includes(searchInputs[key].toLowerCase())
    );
  });

  return (
    <Container>
      <Heading as="h4">Shops</Heading>
      <Line size="percantage" bold="large" />
      <Box style={{ width: "70%" }}>
        <SearchFormContainer>
          <SearchForm onInputChange={handleInputChange} />
        </SearchFormContainer>
      </Box>
      <ShopsTable shops={filterShopsData}></ShopsTable>
      <Modal>
        <Modal.Open opens="CreateShop">
          <Button style={{ width: "200px" }}>Create new Shop</Button>
        </Modal.Open>
        <Modal.Window name="CreateShop">
          <CreateShop />
        </Modal.Window>
      </Modal>
    </Container>
  );
}

function SearchForm({ onInputChange }) {
  return (
    <>
      <FormRowVertical label="Name">
        <Input
          style={{ width: "250px" }}
          placeholder="Name of the store"
          name="name"
          onChange={onInputChange}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label="Type">
        <Input
          style={{ width: "250px" }}
          placeholder="Type of the store"
          name="type"
          onChange={onInputChange}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label="Location">
        <Input
          style={{ width: "250px" }}
          placeholder="Location of the store"
          name="location"
          onChange={onInputChange}
        ></Input>
      </FormRowVertical>
    </>
  );
}

function ShopsTable({ shops }) {
  const navigate = useNavigate();
  return (
    <Table>
      <thead>
        <Th>Name:</Th>
        <Th>Type:</Th>
        <Th>Location:</Th>
        <Th>Description:</Th>
        <Th></Th>
      </thead>
      <tbody>
        {shops.map((shop, index: number) => (
          <Tr onClick={() => navigate(`${shop.id}`)} key={index}>
            <Td>{shop.name}</Td>
            <Td>{shop.type}</Td>
            <Td>{shop.location}</Td>
            <Td>{shop.description}</Td>
            <Td>
              <Button>
                <HiXMark />
              </Button>
            </Td>
          </Tr>
        ))}
      </tbody>
    </Table>
  );
}
