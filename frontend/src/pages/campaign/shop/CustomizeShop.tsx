import Heading from "../../../ui/text/Heading";
import Spinner from "../../../ui/interactive/Spinner";
import { useShop } from "../../../features/campaigns/shop/hooks/useShop";
import styled from "styled-components";
import React, { useState } from "react";
import Button from "../../../ui/interactive/Button";
import Line from "../../../ui/separators/Line";
import { Shop, ShopItem } from "../../../models/shop";
import {
  CoinPurse,
  coinPursePrint,
} from "../../../features/items/models/coinPurse";
import { useShopItems } from "../../../features/campaigns/shop/hooks/useShopItems";
import { useAllItems } from "../../../features/campaigns/shop/hooks/useAllItems";

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

const MiddleContainer = styled.div`
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
`;

function CustomizeShop() {
  const { shop, isPending } = useShop();
  const { isLoading, items = [], error } = useAllItems();
  const { isLoading: isFetching, shopItems, error: exception } = useShopItems();

  const [selectedItem, setSelectedItem] = useState<ShopItem | undefined>(
    undefined
  );

  const [price, setPrice] = useState<CoinPurse>({
    goldPieces: 0,
    silverPieces: 0,
    copperPieces: 0,
  });

  const [quantity, setQuantity] = useState(1);

  if (isPending || isLoading || isFetching) {
    return <Spinner />;
  }

  if (!shop || error || exception) {
    return <div>Error when fetching data</div>;
  }

  const handleClick = (item: ShopItem) => {
    setSelectedItem((prev) => (prev === item ? undefined : item));
  };

  const changePrice = (type: keyof CoinPurse, value: number) => {
    setPrice((prev) => ({
      ...prev,
      [type]: value,
    }));
  };

  const addToShop = () => {
    const existingItem = shopItems.find(
      (i: ShopItem) => i.id === selectedItem?.id
    );

    if (existingItem) {
      setShopItems(
        shopItems.map((i: ShopItem) =>
          i.id === existingItem.id
            ? { ...i, qty: i.qty + quantity, price: price }
            : i
        )
      );
    } else {
      setShopItems([...shopItems, { ...item, qty: quantity, price: price }]);
    }
  };

  const removeFromShop = (quantity) => {
    setShopItems(shopItems.filter((i: ShopItem) => i.id !== item.id));
  };

  const updateShopItems = () => {};

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
                <Th>Weight</Th>
                <Th>Description</Th>
              </thead>
              <tbody>
                {items?.map((item) => (
                  <Tr
                    key={item.id}
                    onClick={() => handleClick(item)}
                    style={{
                      backgroundColor:
                        item === selectedItem
                          ? "rgba(136, 213, 136, 0.59)"
                          : undefined,
                    }}
                  >
                    <Td>{item.name}</Td>
                    <Td>{item.weight}</Td>
                    <Td>{item.description}</Td>
                  </Tr>
                ))}
              </tbody>
            </Table>
          </TableWrapper>
        </TableContainer>
        <MiddleContainer>
          <p>Gold</p>
          <Input
            type="number"
            value={price?.goldPieces || 0}
            onChange={(e) =>
              changePrice("goldPieces", parseInt(e.target.value))
            }
            disabled={!selectedItem}
          />
          <p>Silver</p>
          <Input
            type="number"
            value={price?.silverPieces || 0}
            onChange={(e) =>
              changePrice("silverPieces", parseInt(e.target.value))
            }
            disabled={!selectedItem}
          />
          <p>Copper</p>
          <Input
            type="number"
            value={price?.copperPieces || 0}
            onChange={(e) =>
              changePrice("copperPieces", parseInt(e.target.value))
            }
            disabled={!selectedItem}
          />
          <Button onClick={() => addToShop()}> → </Button>
          <p>Quantity</p>
          <Input
            type="number"
            value={quantity}
            onChange={(e) => setQuantity(parseInt(e.target.value))}
            disabled={!selectedItem}
          />
          <Button onClick={() => updateShopItems()}> ← </Button>
        </MiddleContainer>
        <TableContainer>
          <Heading as="h1">Shop Inventory</Heading>
          <TableWrapper>
            <Table>
              <thead>
                <Th>Name</Th>
                <Th>Price</Th>
                <Th>Qty</Th>
              </thead>
              <tbody>
                {shopItems?.map((item: ShopItem) => (
                  <Tr
                    key={item.id}
                    onClick={() => handleClick(item)}
                    style={{
                      backgroundColor:
                        item.id === selectedItem?.id
                          ? "rgba(136, 213, 136, 0.59)"
                          : undefined,
                    }}
                  >
                    <Td>{item.name}</Td>
                    <Td>{coinPursePrint(item.price)}</Td>
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
