import { useContext, useEffect, useState } from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import { ItemIdContext } from "../contexts/ItemIdContext";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { PowerListItem } from "../../../models/power";
import { usePowers } from "../../../pages/powers/hooks/usePowers";
import { useItemPowers } from "../hooks/useItemPowers";
import { useUpdateItemPowers } from "../hooks/useUpdateItemPowers";

export default function PowersTable({ powers }: { powers: PowerListItem[] }) {
  return (
    <Menus>
      <Table
        header="Powers available when equipped"
        button="Add new"
        columns="1fr"
        modal={<PowerSelectionForm />}
      >
        <Table.Header>
          <div>Name</div>
        </Table.Header>
        <Table.Body
          data={powers}
          render={(power) => <PowerRow key={power.id} power={power} />}
          columnCount={1}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-secondary-text);
`;

function PowerRow({ power }: { power: PowerListItem }) {
  return (
    <Table.Row>
      <Cell>{power.name}</Cell>
    </Table.Row>
  );
}

function PowerSelectionForm() {
  const { itemId } = useContext(ItemIdContext);

  const { isLoading: isLoadingAllPowers, powers: allPowers } = usePowers();
  const { isLoading: isLoadingItemPowers, powers: itemPowers } =
    useItemPowers(itemId);
  const { isPending, updateItemPowers } = useUpdateItemPowers(() => {},
  itemId as number);

  const [itemPowersLocal, setItemPowersLocal] = useState(
    itemPowers as PowerListItem[]
  );
  useEffect(() => {
    setItemPowersLocal(itemPowers ?? []);
  }, [itemPowers]);

  const [selectedPowerIdFromAll, setSelectedPowerIdFromAll] = useState<
    number | null
  >(null);
  const [selectedPowerIdFromItem, setSelectedPowerIdFromItem] = useState<
    number | null
  >(null);

  const allPowersWithoutLocal = allPowers
    ? allPowers.filter(
        (power) =>
          !itemPowersLocal?.find((itemPower) => itemPower.id === power.id)
      )
    : [];

  const handleSelectAllPowers = (row: { id: number; name: string }) => {
    let selectedItem = allPowersWithoutLocal?.find(
      (_value, index) => index === row.id
    );
    setSelectedPowerIdFromAll(selectedItem ? selectedItem.id : null);
    setSelectedPowerIdFromItem(null);
  };
  const handleSelectItemPowers = (row: { id: number; name: string }) => {
    let selectedItem = itemPowersLocal?.find(
      (_value, index) => index === row.id
    );
    setSelectedPowerIdFromItem(selectedItem ? selectedItem.id : null);
    setSelectedPowerIdFromAll(null);
  };

  return (
    <Grid>
      <Column1>
        {!isLoadingAllPowers && (
          <ReusableTable
            mainHeader="All possible slots"
            tableRowsColomns={{ Name: "name" }}
            data={allPowersWithoutLocal.map((power, index) => {
              return {
                id: index,
                name: power.name,
              };
            })}
            isSelectable={true}
            onSelect={handleSelectAllPowers}
          ></ReusableTable>
        )}
        {isLoadingAllPowers && <Spinner />}
      </Column1>
      <Column2>
        {!isPending && (
          <>
            <Button
              disabled={selectedPowerIdFromAll === null}
              onClick={() => {
                setItemPowersLocal(() => {
                  return [
                    ...(itemPowersLocal as PowerListItem[]),
                    allPowers?.find(
                      (power) => power.id === selectedPowerIdFromAll
                    ) as PowerListItem,
                  ];
                });
                setSelectedPowerIdFromAll(null);
              }}
            >
              {">>"}
            </Button>
            <Button
              disabled={selectedPowerIdFromItem === null}
              onClick={() => {
                setItemPowersLocal(() => {
                  return (itemPowersLocal as PowerListItem[]).filter(
                    (slot) => slot.id !== selectedPowerIdFromItem
                  );
                });
                setSelectedPowerIdFromItem(null);
              }}
            >
              {"<<"}
            </Button>
            <Button onClick={() => updateItemPowers(itemPowersLocal)}>
              {"Save"}
            </Button>
          </>
        )}
      </Column2>
      <Column3>
        {!isLoadingItemPowers && (
          <ReusableTable
            mainHeader="Selected slots"
            tableRowsColomns={{ Name: "name" }}
            data={
              itemPowersLocal?.map((power, index) => {
                return {
                  id: index,
                  name: power.name,
                };
              }) ?? []
            }
            isSelectable={true}
            onSelect={handleSelectItemPowers}
          ></ReusableTable>
        )}
        {isLoadingItemPowers && <Spinner />}
      </Column3>
    </Grid>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  grid-column-gap: 10px;
`;

const Column1 = styled.div`
  grid-column: 1/2;
`;
const Column2 = styled.div`
  grid-column: 2/3;
  display: flex;
  flex-direction: column;
`;
const Column3 = styled.div`
  grid-column: 3/4;
`;
