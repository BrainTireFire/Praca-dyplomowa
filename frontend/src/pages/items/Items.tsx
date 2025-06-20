import styled, { css } from "styled-components";
import ItemForm from "../../features/items/ItemForm";
import Button from "../../ui/interactive/Button";
import Spinner from "../../ui/interactive/Spinner";
import { useItems } from "./hooks/useItems";
import { useContext, useState } from "react";
import Modal from "../../ui/containers/Modal";
import CreateNewItemForm from "./CreateNewItemForm";
import { EditModeContext } from "../../context/EditModeContext";
import { CharacterIdContext } from "../../features/characters/contexts/CharacterIdContext";
import { ReusableTable } from "../../ui/containers/ReusableTable2";
import { useNavigate, useSearchParams } from "react-router-dom";

export default function Items() {
  const editMode = useContext(EditModeContext);

  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const selectedItemId = searchParams.get("id");

  const handleChangeItem = (chosenItemId: number) => {
    navigate(`/items?id=${chosenItemId}`);
  };

  const { isLoading, items, error } = useItems("blueprint");

  // const [selectedItemId, setSelectedItemId] = useState<null | number>(null);
  // const { createItem, isPending: isPendingCreation } = useCreateItem(() => {});
  const handleSelect = (row: any) => {
    let selectedItem = items?.find((_value, index) => index === row.id);

    // setSelectedItemId(selectedItem ? selectedItem.id : null);
    handleChangeItem(selectedItem!.id);
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
            items
              ? items.map((item, index) => {
                  return {
                    id: index,
                    Name: item.name,
                    OwnerName: item.ownerName,
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
              <CreateNewItemForm></CreateNewItemForm>
            </Modal.Window>
          </Modal>
        )}
      </Column1>
      <Column2>
        {!!Number(selectedItemId) && (
          <ItemForm
            itemId={Number(selectedItemId)}
            key={selectedItemId}
            maxHeight="100%"
          ></ItemForm>
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
