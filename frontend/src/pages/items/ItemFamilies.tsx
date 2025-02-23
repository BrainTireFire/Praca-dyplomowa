import { useContext, useState } from "react";
import { EditModeContext } from "../../context/EditModeContext";
import { ReusableTable } from "../../ui/containers/ReusableTable2";
import Spinner from "../../ui/interactive/Spinner";
import Modal from "../../ui/containers/Modal";
import styled, { css } from "styled-components";
import Button from "../../ui/interactive/Button";
import ItemFamilyForm from "../../features/itemFamilies/ItemFamilyForm";
import { useItemFamilies } from "./hooks/useItemFamilies";

export default function ItemFamilies() {
  const editMode = useContext(EditModeContext);
  const { isLoading, itemFamilies, error } = useItemFamilies();

  const [selectedItemId, setSelectedItemId] = useState<null | number>(null);
  // const { createItem, isPending: isPendingCreation } = useCreateItem(() => {});
  const handleSelect = (row: any) => {
    let selectedItem = itemFamilies?.find((_value, index) => index === row.id);

    setSelectedItemId(selectedItem ? selectedItem.id : null);
  };

  if (isLoading) {
    return <Spinner></Spinner>;
  }
  return (
    <Container>
      <Column1>
        <ReusableTable
          tableRowsColomns={{
            Name: "Name",
            Owner: "OwnerName",
          }}
          data={
            itemFamilies
              ? itemFamilies.map((itemFamily, index) => {
                  return {
                    id: index,
                    Name: itemFamily.name,
                    OwnerName: itemFamily.ownerName,
                  };
                })
              : []
          }
          isSelectable={true}
          onSelect={handleSelect}
          isSearching={true}
          customTableContainer={css`
            height: 95%;
          `}
        ></ReusableTable>
        {editMode && (
          <Modal>
            <Modal.Open opens="TableAction">
              <Button
                customStyles={css`
                  height: 5%;
                `}
              >
                Create new
              </Button>
            </Modal.Open>
            <Modal.Window name="TableAction">
              <ItemFamilyForm></ItemFamilyForm>
            </Modal.Window>
          </Modal>
        )}
      </Column1>
      <Column2>
        {selectedItemId && (
          <ItemFamilyForm
            itemFamilyId={selectedItemId}
            key={selectedItemId}
          ></ItemFamilyForm>
        )}
      </Column2>
    </Container>
  );
}

const Container = styled.div`
  display: flex;
  flex-direction: row;
  max-height: 100%;
  height: 100%;
  column-gap: 10px;
`;

const Column1 = styled.div`
  max-height: 100%;
  height: 100%;
  width: 40%;
  display: flex;
  flex-direction: column;
`;
const Column2 = styled.div`
  max-height: 100%;
  height: 100%;
  max-width: 60%;
  width: 60%;
`;
