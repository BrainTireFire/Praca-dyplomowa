import styled from "styled-components";
import Heading from "../../../ui/text/Heading";
import Line from "../../../ui/separators/Line";
import { coinPursePrint } from "../../items/models/coinPurse";
import { useShopItems } from "./hooks/useShopItems";
import { useEffect, useState } from "react";
import { ShopItem } from "../../../models/shop";
import { useLocation } from "react-router-dom";
import { useShopCharacter } from "./hooks/useShopCharacter";
import Button from "../../../ui/interactive/Button";
import { useBuyItem } from "./hooks/useBuyItem";

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

const CoinPurseDisplay = styled.div`
  margin-left: auto;
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  border-radius: var(--border-radius-sm);
  padding: 0.6rem 1.2rem;
  font-size: 1.4rem;
  font-weight: bold;
`;

const WeightDisplay = styled.div`
  margin-left: auto;
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  border-radius: var(--border-radius-sm);
  padding: 0.6rem 1.2rem;
  font-size: 1.4rem;
  font-weight: bold;
`;

export default function TradeShop() {
  const { isLoading: isLoadingShopItems, shopItems: data } = useShopItems();
  const { isLoading: isLoadingCharacter, shopCharacter } = useShopCharacter();
  const [shopItems, setShopItems] = useState(data);
  const [characterItems, setCharacterItems] = useState<ShopItem[]>(undefined);

  useEffect(() => {
    if (data) {
      setShopItems(data);
    }
    if (shopCharacter?.items) {
      setCharacterItems(shopCharacter.items);
    }
  }, [data, shopCharacter]);

  const [selectedShopItem, setSelectedShopItem] = useState<
    ShopItem | undefined
  >(undefined);

  const [selectedItem, setSelectedItem] = useState<ShopItem | undefined>(
    undefined
  );

  const { mutate: buyItemMutate } = useBuyItem();

  if (
    isLoadingCharacter ||
    isLoadingShopItems ||
    !shopCharacter ||
    !shopItems
  ) {
    return <div>No data has been recieved</div>;
  }

  return (
    <Container>
      <Heading as="h1">Trading</Heading>
      <Line size="percantage" bold="large" />
      <BodyContainer>
        <TableContainer>
          <Heading as="h1">Shop Inventory</Heading>
          <TableWrapper>
            <Table>
              <thead>
                <Th>Name</Th>
                <Th>Weight</Th>
                <Th>Price</Th>
                <Th>Qty</Th>
              </thead>
              <tbody>
                {shopItems?.map((item: ShopItem) => (
                  <Tr
                    key={item.id}
                    onClick={() =>
                      setSelectedShopItem((prev) =>
                        prev === item ? undefined : item
                      )
                    }
                    style={{
                      backgroundColor:
                        item.id === selectedShopItem?.id
                          ? "rgba(136, 213, 136, 0.59)"
                          : undefined,
                    }}
                  >
                    <Td>{item.name}</Td>
                    <Td>{item.weight}</Td>
                    <Td>{coinPursePrint(item.price)}</Td>
                    <Td>{item.quantity}</Td>
                  </Tr>
                ))}
              </tbody>
            </Table>
          </TableWrapper>
        </TableContainer>
        <MiddleContainer>
          <Button
            onClick={() =>
              buyItemMutate({
                characterId: shopCharacter.id,
                itemId: selectedShopItem.id,
              })
            }
            disabled={
              !selectedShopItem ||
              characterItems?.some((item) => item.id === selectedShopItem.id)
            }
          >
            Buy
          </Button>
          <Button onClick={() => 0} disabled={!selectedItem}>
            Sell
          </Button>
        </MiddleContainer>
        <TableContainer>
          <div
            style={{
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
            }}
          >
            <Heading as="h1">Character</Heading>
            <WeightDisplay>⚖️ {shopCharacter.itemsWeight}</WeightDisplay>
            <CoinPurseDisplay>
              💰 {coinPursePrint(shopCharacter.coinPurse)}
            </CoinPurseDisplay>
          </div>
          <TableWrapper>
            <Table>
              <thead>
                <Th>Name</Th>
                <Th>Weight</Th>
                <Th>Price</Th>
                <Th>Description</Th>
              </thead>
              <tbody>
                {characterItems?.map((item: ShopItem) => (
                  <Tr
                    key={item.id}
                    onClick={() =>
                      setSelectedItem((prev) =>
                        prev === item ? undefined : item
                      )
                    }
                    style={{
                      backgroundColor:
                        item.id === selectedItem?.id
                          ? "rgba(136, 213, 136, 0.59)"
                          : undefined,
                    }}
                  >
                    <Td>{item.name}</Td>
                    <Td>{item.weight}</Td>
                    <Td>{coinPursePrint(item.price)}</Td>
                    <Td>{item.description}</Td>
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
