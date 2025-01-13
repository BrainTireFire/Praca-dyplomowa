import styled, { css } from "styled-components";
import ItemForm from "../../features/items/ItemForm";
import { ReusableTable } from "../../ui/containers/ReusableTable2";
import Button from "../../ui/interactive/Button";
import Spinner from "../../ui/interactive/Spinner";
import { useContext, useState } from "react";
import Modal from "../../ui/containers/Modal";
import { EditModeContext } from "../../context/EditModeContext";
import { CharacterIdContext } from "../../features/characters/contexts/CharacterIdContext";
import { useItems } from "../../pages/items/hooks/useItems";
import { useAddItemToEquipment } from "./hooks/useAddItemToEquipment";

export default function AddEquipmentScreen() {
  const { isLoading, items, error } = useItems("blueprint");
  const { characterId } = useContext(CharacterIdContext);
  const [selectedItemId, setSelectedItemId] = useState<null | number>(null);
  // const { createItem, isPending: isPendingCreation } = useCreateItem(() => {});
  const handleSelect = (row: any) => {
    console.log(items);
    console.log(row);
    let selectedItem = items?.find((_value, index) => index === row.id);
    console.log(selectedItem);

    setSelectedItemId(selectedItem ? selectedItem.id : null);
    console.log(selectedItemId);
  };

  const { addToEquipment, isPending: isPendingAdd } = useAddItemToEquipment(
    () => {},
    characterId as number
  );

  if (isLoading || isPendingAdd) {
    return <Spinner></Spinner>;
  }
  return (
    <Container>
      <Column1>
        <ReusableTable
          tableRowsColomns={{
            Name: "Name",
          }}
          data={
            items
              ? items.map((item, index) => {
                  return {
                    id: index,
                    Name: item.name,
                  };
                })
              : []
          }
          isSelectable={true}
          onSelect={handleSelect}
          customTableContainer={css``}
        ></ReusableTable>

        <Button
          disabled={!selectedItemId}
          onClick={() => addToEquipment(selectedItemId as number)}
        >
          Add to character
        </Button>
      </Column1>
      <Column2>
        {selectedItemId && (
          <EditModeContext.Provider value={{ editMode: false }}>
            <ItemForm itemId={selectedItemId} key={selectedItemId}></ItemForm>
          </EditModeContext.Provider>
        )}
      </Column2>
    </Container>
  );
}

const Container = styled.div`
  display: grid;
  grid-template-columns: 1fr 2fr;
  width: 90vw;
  height: 90vh;
`;

const Column1 = styled.div`
  grid-column: 1;
  max-height: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  overflow: hidden;
`;
const Column2 = styled.div`
  grid-column: 2;
  overflow-y: auto;
  max-height: 100%;
`;
