import { Reducer, useContext, useEffect, useReducer, useState } from "react";
import { useItemFamily } from "./hooks/useItemFamily";
import { useItem } from "../items/hooks/useItem";
import { ItemFamily, itemFamilyInitialValue } from "../../models/itemfamily";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Spinner from "../../ui/interactive/Spinner";
import Dropdown from "../../ui/forms/Dropdown";
import { itemTypeLabel, itemTypeOptions } from "../../pages/items/itemTypes";
import { useCreateItemFamily } from "./hooks/useCreateItemFamily";
import { useUpdateItemFamily } from "./hooks/useUpdateItemFamily";
import { EditModeContext } from "../../context/EditModeContext";
import Button from "../../ui/interactive/Button";
import { useDeleteItemFamily } from "./hooks/useDeleteItemFamily";
import Box from "../../ui/containers/Box";
import styled from "styled-components";
import Modal from "../../ui/containers/Modal";
import ConfirmDelete from "../../ui/containers/ConfirmDelete";

export type ItemFamilyAction =
  | { type: "SET_ITEM"; payload: ItemFamily }
  | { type: "UPDATE_FIELD"; field: keyof ItemFamily; value: any }
  | { type: "RESET_ITEM" }
  | { type: "RESET_FIELD"; field: keyof ItemFamily };

export const itemFamilyReducer: Reducer<ItemFamily, ItemFamilyAction> = (
  state,
  action
) => {
  switch (action.type) {
    case "SET_ITEM":
      return { ...action.payload };

    case "UPDATE_FIELD":
      return { ...state, [action.field]: action.value };

    case "RESET_ITEM":
      return itemFamilyInitialValue;

    case "RESET_FIELD":
      return { ...state, [action.field]: itemFamilyInitialValue[action.field] };

    default:
      throw new Error(`Unhandled action type: ${action["type"]}`);
  }
};

export default function ItemFamilyForm({
  itemFamilyId,
}: {
  itemFamilyId: number | null;
}) {
  const { editMode } = useContext(EditModeContext);
  const { isLoading, itemFamily, error } = useItemFamily(itemFamilyId);
  const { isPending: isPendingPost, createItemFamily } = useCreateItemFamily(
    () => {}
  );
  const { isPending: isPendingUpdate, updateItemFamily } = useUpdateItemFamily(
    () => {}
  );
  const { isPending: isPendingDelete, deleteItemFamily } = useDeleteItemFamily(
    () => {}
  );

  const [state, dispatch] = useReducer(
    itemFamilyReducer,
    itemFamily ?? itemFamilyInitialValue
  );
  const [resetHappened, setResetHappened] = useState(false);

  // Update local state when data is fetched
  useEffect(() => {
    if (itemFamily) {
      dispatch({
        type: "SET_ITEM",
        payload: itemFamily,
      });
      setResetHappened(true);
    }
  }, [itemFamily]);

  const disableChanges = !editMode || !state.editable;
  const isSavingChangesDisallowed = () => {
    return !state.name || !state.itemType;
  };

  if (
    isPendingPost ||
    isPendingUpdate ||
    isPendingDelete ||
    (itemFamilyId !== null && (isLoading || !resetHappened))
  )
    return <Spinner></Spinner>;
  if (error) return <>Error</>;
  return (
    <>
      <Container>
        <FormRowVertical label="Name">
          <Input
            value={state.name}
            onChange={(e) =>
              dispatch({
                type: "UPDATE_FIELD",
                field: "name",
                value: e.target.value,
              })
            }
          ></Input>
        </FormRowVertical>
        <FormRowVertical label="Item type">
          <Dropdown
            valuesList={itemTypeOptions}
            chosenValue={state.itemType}
            setChosenValue={(e) =>
              dispatch({
                type: "UPDATE_FIELD",
                field: "itemType",
                value: e,
              })
            }
          ></Dropdown>
        </FormRowVertical>
      </Container>
      <UDButtonContainer>
        {itemFamilyId === null && (
          <Button
            onClick={() => createItemFamily(state)}
            disabled={isSavingChangesDisallowed() || disableChanges}
          >
            Save
          </Button>
        )}
        {itemFamilyId !== null && (
          <Button
            onClick={() => updateItemFamily(state)}
            disabled={isSavingChangesDisallowed() || disableChanges}
          >
            {disableChanges ? "You cannot edit this object" : "Update"}
          </Button>
        )}
        {itemFamilyId !== null && (
          <Modal>
            <Modal.Open opens="ConfirmDelete">
              <Button
                disabled={isSavingChangesDisallowed() || disableChanges}
              >
                {disableChanges ? "You cannot delete this object" : "Delete"}
              </Button>
            </Modal.Open>
            <Modal.Window name="ConfirmDelete">
              <ConfirmDelete
                resourceName={"Item Family"}
                onConfirm={() =>  deleteItemFamily(itemFamilyId)}
                />
            </Modal.Window>
          </Modal>
        )}
      </UDButtonContainer>
    </>
  );
}

ItemFamilyForm.defaultProps = {
  itemFamilyId: null,
};

const Container = styled(Box)`
  flex: 1;
  overflow-y: hidden;
  padding: 0rem 1rem 0rem 1rem;
`;

const UDButtonContainer = styled.div`
  display: flex;
  flex-direction: row;
  gap: 10px;
  justify-content: center;
  margin: 10px;
`;
