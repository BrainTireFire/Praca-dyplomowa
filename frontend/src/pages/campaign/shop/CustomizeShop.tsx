import Heading from "../../../ui/text/Heading";
import { useParams } from "react-router-dom";
import Spinner from "../../../ui/interactive/Spinner";
import { useTranslation } from "react-i18next";
import { useShop } from "../../../features/campaigns/shop/hooks/useShop";
import styled from "styled-components";
import React, { useState } from "react";
import Button from "../../../ui/interactive/Button";
import Line from "../../../ui/separators/Line";

const Container = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  width: 100%;
  gap: 3%;
  margin-top: 1%;
`;

const BodyContainer = styled.div`
  display: flex;
  justify-content: space-between;
  padding: 20px;
  padding-top: 0px;
  height: 78vh;
`;

const TableContainer = styled.div`
  width: 45%;
  padding: 10px;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  height: 92%;
`;

const TableWrapper = styled.div`
  margin-top: 30px;
  flex-grow: 1;
  overflow-y: auto;
  border: 1px solid var(--color-border);
  border-radius: var(--border-radius-md);
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);

  &::-webkit-scrollbar {
    display: none;
  }
  -ms-overflow-style: none;
  scrollbar-width: none;
`;

const Table = styled.table`
  width: 100%;
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border-spacing: 0;
`;

const Th = styled.th`
  padding: 10px;
`;

const Tr = styled.tr`
  transition: background-color 100ms ease;
  cursor: pointer;
  &:hover {
    background-color: rgba(116, 177, 116, 0.5);
  }
`;

const Td = styled.td`
  padding: 8px;
  border-top: 1px solid var(--color-border);
  text-align: center;
`;

const ButtonContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 20px;
  width: 10%;
`;

const Input = styled.input`
  width: 30%;
  padding: 5px;
  margin: 5px;
  text-align: center;
  &::-webkit-inner-spin-button,
  &::-webkit-outer-spin-button {
    -webkit-appearance: none;
    margin: 0;
  }

  & {
    -moz-appearance: textfield;
  }
`;

const itemsData = [
  { id: 1, name: "Name of armor", type: "Armor", dateCreated: "2/19/2024" },
  { id: 2, name: "Name of weapon", type: "Weapon", dateCreated: "2/19/2024" },
  { id: 3, name: "Name of potion", type: "Potion", dateCreated: "2/19/2024" },
  { id: 4, name: "Name of ring", type: "Ring", dateCreated: "2/19/2024" },
  { id: 5, name: "Name of helmet", type: "Helmet", dateCreated: "2/19/2024" },
  { id: 6, name: "Name of armor", type: "Armor", dateCreated: "2/19/2024" },
  { id: 7, name: "Name of weapon", type: "Weapon", dateCreated: "2/19/2024" },
  { id: 8, name: "Name of potion", type: "Potion", dateCreated: "2/19/2024" },
  { id: 9, name: "Name of ring", type: "Ring", dateCreated: "2/19/2024" },
  { id: 10, name: "Name of helmet", type: "Helmet", dateCreated: "2/19/2024" },
  { id: 11, name: "Name of armor", type: "Armor", dateCreated: "2/19/2024" },
  { id: 12, name: "Name of weapon", type: "Weapon", dateCreated: "2/19/2024" },
  { id: 13, name: "Name of potion", type: "Potion", dateCreated: "2/19/2024" },
  { id: 14, name: "Name of ring", type: "Ring", dateCreated: "2/19/2024" },
  { id: 15, name: "Name of helmet", type: "Helmet", dateCreated: "2/19/2024" },
  { id: 16, name: "Name of armor", type: "Armor", dateCreated: "2/19/2024" },
  { id: 17, name: "Name of weapon", type: "Weapon", dateCreated: "2/19/2024" },
  { id: 18, name: "Name of potion", type: "Potion", dateCreated: "2/19/2024" },
  { id: 19, name: "Name of ring", type: "Ring", dateCreated: "2/19/2024" },
  { id: 20, name: "Name of helmet", type: "Helmet", dateCreated: "2/19/2024" },
];

function CustomizeShop() {
  const { shop, isPending } = useShop();
  const { t } = useTranslation();
  const [allItems, setAllItems] = useState(itemsData);
  const [shopItems, setShopItems] = useState([]);
  const [quantity, setQuantity] = useState(1);

  if (isPending) {
    return <Spinner />;
  }

  if (!shop) {
    return <div>shop not found</div>;
  }

  const addToShop = (item) => {
    const existingItem = shopItems.find((i) => i.id === item.id);
    if (existingItem) {
      setShopItems(
        shopItems.map((i) =>
          i.id === item.id ? { ...i, qty: i.qty + quantity } : i
        )
      );
    } else {
      setShopItems([...shopItems, { ...item, qty: quantity }]);
    }
  };

  const removeFromShop = (item) => {
    setShopItems(shopItems.filter((i) => i.id !== item.id));
  };

  return (
    <Container>
      <Heading as="h1">Customize Shop</Heading>
      <Line size="percantage" bold="large" />
      <BodyContainer>
        <TableContainer>
          <Heading as="h1">All Items</Heading>
          <TableWrapper>
            <Table>
              <thead>
                <Th>Name</Th>
                <Th>Type</Th>
                <Th>Date Created</Th>
              </thead>
              <tbody>
                {allItems.map((item) => (
                  <Tr key={item.id}>
                    <Td>{item.name}</Td>
                    <Td>{item.type}</Td>
                    <Td>{item.dateCreated}</Td>
                  </Tr>
                ))}
              </tbody>
            </Table>
          </TableWrapper>
        </TableContainer>

        <ButtonContainer>
          <Button onClick={() => addToShop(allItems[0])}> → </Button>
          <Input
            type="number"
            value={quantity}
            onChange={(e) => setQuantity(parseInt(e.target.value))}
          />
          <Button onClick={() => removeFromShop(shopItems[0])}> ← </Button>
        </ButtonContainer>

        <TableContainer>
          <Heading as="h1">Shop Inventory</Heading>
          <TableWrapper>
            <Table>
              <thead>
                <Th>Name</Th>
                <Th>Type</Th>
                <Th>Date Created</Th>
                <Th>Qty</Th>
              </thead>
              <tbody>
                {shopItems.map((item) => (
                  <Tr key={item.id}>
                    <Td>{item.name}</Td>
                    <Td>{item.type}</Td>
                    <Td>{item.dateCreated}</Td>
                    <Td>{item.qty}</Td>
                  </Tr>
                ))}
              </tbody>
            </Table>
          </TableWrapper>
        </TableContainer>
      </BodyContainer>
    </Container>
  );
}

export default CustomizeShop;
