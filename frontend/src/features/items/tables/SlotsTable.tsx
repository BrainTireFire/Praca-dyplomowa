import { useContext, useEffect, useState } from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled, { css } from "styled-components";
import { ItemIdContext } from "../contexts/ItemIdContext";
import { ReusableTable } from "../../../ui/containers/ReusableTable";
import { useSlots } from "../hooks/useSlots";
import { useItemSlots } from "../hooks/useItemSlots";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { Slot } from "../../../models/slot";
import { useUpdateItemSlots } from "../hooks/useUpdateItemSlots";

export default function SlotsTable({ slots }: { slots: Slot[] }) {
  return (
    <Menus>
      <Table
        header="Occupied slots"
        button="Add new"
        columns="1fr"
        modal={<SlotSelectionForm />}
      >
        <Table.Header>
          <div>Name</div>
        </Table.Header>
        <Table.Body
          data={slots}
          render={(slot) => <SlotRow key={slot.id} slot={slot} />}
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

function SlotRow({ slot }: { slot: Slot }) {
  return (
    <Table.Row>
      <Cell>{slot.name}</Cell>
    </Table.Row>
  );
}

function SlotSelectionForm() {
  const { itemId } = useContext(ItemIdContext);

  const { isLoading: isLoadingAllSlots, slots: allSlots } = useSlots();
  const { isLoading: isLoadingItemSlots, slots: itemSlots } =
    useItemSlots(itemId);
  const { isPending, updateItemSlots } = useUpdateItemSlots(() => {},
  itemId as number);

  const [itemSlotsLocal, setItemSlotsLocal] = useState(itemSlots as Slot[]);
  useEffect(() => {
    setItemSlotsLocal(itemSlots ?? []);
  }, [itemSlots]);

  const [selectedSlotIdFromAll, setSelectedSlotIdFromAll] = useState<
    number | null
  >(null);
  const [selectedSlotIdFromItem, setSelectedSlotIdFromItem] = useState<
    number | null
  >(null);

  const allSlotsWithoutLocal = allSlots
    ? allSlots.filter(
        (slot) => !itemSlotsLocal?.find((itemSlot) => itemSlot.id === slot.id)
      )
    : [];

  const handleSelectAllSlots = (row: { id: number; name: string }) => {
    let selectedItem = allSlotsWithoutLocal?.find(
      (_value, index) => index === row.id
    );
    setSelectedSlotIdFromAll(selectedItem ? selectedItem.id : null);
    setSelectedSlotIdFromItem(null);
  };
  const handleSelectItemSlots = (row: { id: number; name: string }) => {
    let selectedItem = itemSlotsLocal?.find(
      (_value, index) => index === row.id
    );
    setSelectedSlotIdFromItem(selectedItem ? selectedItem.id : null);
    setSelectedSlotIdFromAll(null);
  };

  return (
    <Grid>
      <Column1>
        {!isLoadingAllSlots && (
          <ReusableTable
            mainHeader="All possible slots"
            tableRowsColomns={{ Name: "name" }}
            data={allSlotsWithoutLocal.map((slot, index) => {
              return {
                id: index,
                name: slot.name,
              };
            })}
            isSelectable={true}
            onSelect={handleSelectAllSlots}
            customTableContainer={css`
              height: 100%;
            `}
          ></ReusableTable>
        )}
        {isLoadingAllSlots && <Spinner />}
      </Column1>
      <Column2>
        {!isPending && (
          <>
            <Button
              disabled={selectedSlotIdFromAll === null}
              onClick={() => {
                setItemSlotsLocal(() => {
                  return [
                    ...(itemSlotsLocal as Slot[]),
                    allSlots?.find(
                      (slot) => slot.id === selectedSlotIdFromAll
                    ) as Slot,
                  ];
                });
                setSelectedSlotIdFromAll(null);
              }}
            >
              {">>"}
            </Button>
            <Button
              disabled={selectedSlotIdFromItem === null}
              onClick={() => {
                setItemSlotsLocal(() => {
                  return (itemSlotsLocal as Slot[]).filter(
                    (slot) => slot.id !== selectedSlotIdFromItem
                  );
                });
                setSelectedSlotIdFromItem(null);
              }}
            >
              {"<<"}
            </Button>
            <Button onClick={() => updateItemSlots(itemSlotsLocal)}>
              {"Save"}
            </Button>
          </>
        )}
      </Column2>
      <Column3>
        {!isLoadingItemSlots && (
          <ReusableTable
            mainHeader="Selected slots"
            tableRowsColomns={{ Name: "name" }}
            data={
              itemSlotsLocal?.map((slot, index) => {
                return {
                  id: index,
                  name: slot.name,
                };
              }) ?? []
            }
            isSelectable={true}
            onSelect={handleSelectItemSlots}
            customTableContainer={css`
              height: 100%;
            `}
          ></ReusableTable>
        )}
        {isLoadingItemSlots && <Spinner />}
      </Column3>
    </Grid>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  grid-column-gap: 10px;
  height: 70vh;
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
