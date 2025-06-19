import Heading from "../../../ui/text/Heading";
import Spinner from "../../../ui/interactive/Spinner";
import { useShop } from "./hooks/useShop";
import styled from "styled-components";
import React, { useEffect, useState } from "react";
import Button from "../../../ui/interactive/Button";
import Line from "../../../ui/separators/Line";
import { Shop, ShopItem } from "../../../models/shop";
import { CoinPurse, coinPursePrint } from "../../items/models/coinPurse";
import { useShopItems } from "./hooks/useShopItems";
import { useAllItems } from "./hooks/useAllItems";
import useUpdateShopItem from "./hooks/useUpdateShopItem";
import useRemoveShopItem from "./hooks/useRemoveShopItem";

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
  const { shop, isLoading } = useShop();
  const { isLoading: isLoadingItems, items: allItems, error } = useAllItems();
  const {
    isLoading: isLoadingShopItems,
    shopItems: data,
    error: shopItemsError,
  } = useShopItems();
  const [items, setItems] = useState<ShopItem[]>([]);
  const [shopItems, setShopItems] = useState(data);
  const [copyPrice, setCopyPrice] = React.useState(true);
  const { updateShopItem } = useUpdateShopItem();
  const { removeShopItem } = useRemoveShopItem();

  useEffect(() => {
    if (data) {
      setShopItems(data);
    }
    if (allItems) {
      setItems(allItems);
    }
  }, [data, allItems]);

  const [selectedItem, setSelectedItem] = useState<ShopItem | undefined>(
    undefined
  );

  const [selectedShopItem, setSelectedShopItem] = useState<
    ShopItem | undefined
  >(undefined);

  const [price, setPrice] = useState<CoinPurse>({
    goldPieces: 0,
    silverPieces: 0,
    copperPieces: 0,
  });

  if (isLoading || isLoadingItems || isLoadingShopItems) {
    return <Spinner />;
  }

  if (error || shopItemsError) {
    return <div>Error when fetching data</div>;
  }

  if (!shop || !items || !shopItems) {
    return <div>No data has been recieved</div>;
  }

  const handleClick = (item: ShopItem) => {
    setSelectedItem((prev) => (prev === item ? undefined : item));
  };

  const handleShopClick = (item: ShopItem) => {
    setSelectedShopItem((prev) => (prev === item ? undefined : item));
  };

  const changePrice = (type: keyof CoinPurse, value: number) => {
    setPrice((prev) => ({
      ...prev,
      [type]: value,
    }));
  };

  const handleRemoveShopItem = () => {
    if (!selectedShopItem) return;

    removeShopItem({
      shopId: shop.id,
      itemId: selectedShopItem.id,
    });

    setSelectedShopItem(undefined);
  };

  const handleUpdateShopItem = () => {
    if (!selectedItem) return;

    const updatedPrice = copyPrice ? selectedItem.price : price;

    const updatedItem: ShopItem = {
      ...selectedItem,
      price: updatedPrice,
    };

    updateShopItem({ shopId: shop.id, shopItem: updatedItem });
    setSelectedItem(undefined);
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
                <Th>Weight</Th>
                <Th>Description</Th>
                <Th>Price</Th>
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
                    <Td>{coinPursePrint(item.price) || 0}</Td>
                  </Tr>
                ))}
              </tbody>
            </Table>
          </TableWrapper>
        </TableContainer>
        <MiddleContainer>
          <div style={{ display: "flex", gap: "10px" }}>
            <div style={{ textAlign: "center" }}>
              <Input
                type="number"
                value={price?.goldPieces}
                onChange={(e) =>
                  changePrice("goldPieces", parseInt(e.target.value))
                }
                disabled={!selectedItem || copyPrice}
                style={{ width: "35px" }}
              />
              <p>GP</p>
            </div>
            <div style={{ textAlign: "center" }}>
              <Input
                type="number"
                value={price?.silverPieces}
                onChange={(e) =>
                  changePrice("silverPieces", parseInt(e.target.value))
                }
                disabled={!selectedItem || copyPrice}
                style={{ width: "35px" }}
              />
              <p>SP</p>
            </div>
            <div style={{ textAlign: "center" }}>
              <Input
                type="number"
                value={price?.copperPieces}
                onChange={(e) =>
                  changePrice("copperPieces", parseInt(e.target.value))
                }
                disabled={!selectedItem || copyPrice}
                style={{ width: "35px" }}
              />
              <p>CP</p>
            </div>
          </div>
          <p>Copy original price</p>
          <input
            type="checkbox"
            checked={copyPrice}
            onChange={() => setCopyPrice(!copyPrice)}
          />
          <Button
            onClick={() => handleUpdateShopItem()}
            disabled={!selectedItem}
          >
            →
          </Button>
          <Button
            onClick={() => handleRemoveShopItem()}
            disabled={!selectedShopItem}
          >
            ←
          </Button>
        </MiddleContainer>
        <TableContainer>
          <Heading as="h1">Shop Inventory</Heading>
          <TableWrapper>
            <Table>
              <thead>
                <Th>Name</Th>
                <Th>Description</Th>
                <Th>Price</Th>
              </thead>
              <tbody>
                {shopItems?.map((item: ShopItem) => (
                  <Tr
                    key={item.id}
                    onClick={() => handleShopClick(item)}
                    style={{
                      backgroundColor:
                        item.id === selectedShopItem?.id
                          ? "rgba(136, 213, 136, 0.59)"
                          : undefined,
                    }}
                  >
                    <Td>{item.name}</Td>
                    <Td>{item.description}</Td>
                    <Td>{coinPursePrint(item.price)}</Td>
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
