import { Reducer, useContext, useEffect, useReducer, useState } from "react";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Spinner from "../../ui/interactive/Spinner";
import { EditModeContext } from "../../context/EditModeContext";
import Button from "../../ui/interactive/Button";
import Box from "../../ui/containers/Box";
import styled from "styled-components";
import {
  ImmaterialResourceBlueprint,
  ImmaterialResourceBlueprintWithOwner,
  immaterialResourceInitialValue,
  refreshTypeOptions,
} from "../../models/immaterialResourceBlueprint";
import { useImmaterialResource } from "./hooks/useImmaterialResource";
import { useCreateImmaterialResource } from "./hooks/useCreateImmaterialResource";
import { useUpdateImmaterialResource } from "./hooks/useUpdateImmaterialResource";
import { useDeleteImmaterialResource } from "./hooks/useDeleteImmaterialResource";
import Dropdown from "../../ui/forms/Dropdown";
import Modal from "../../ui/containers/Modal";
import ConfirmDelete from "../../ui/containers/ConfirmDelete";

export type ResourceAction =
  | { type: "SET_ITEM"; payload: ImmaterialResourceBlueprintWithOwner }
  | {
      type: "UPDATE_FIELD";
      field: keyof ImmaterialResourceBlueprint;
      value: any;
    }
  | { type: "RESET_ITEM" }
  | { type: "RESET_FIELD"; field: keyof ImmaterialResourceBlueprint };

export const immaterialResourceReducer: Reducer<
  ImmaterialResourceBlueprintWithOwner,
  ResourceAction
> = (state, action) => {
  switch (action.type) {
    case "SET_ITEM":
      return { ...action.payload };

    case "UPDATE_FIELD":
      return { ...state, [action.field]: action.value };

    case "RESET_ITEM":
      return immaterialResourceInitialValue;

    case "RESET_FIELD":
      return {
        ...state,
        [action.field]: immaterialResourceInitialValue[action.field],
      };

    default:
      throw new Error(`Unhandled action type: ${action["type"]}`);
  }
};

export default function ImmaterialResourceForm({
  immaterialResourceId,
}: {
  immaterialResourceId: number | null;
}) {
  const { editMode } = useContext(EditModeContext);
  const { isLoading, immaterialResource, error } =
    useImmaterialResource(immaterialResourceId);
  const { isPending: isPendingPost, createImmaterialResourceBlueprint } =
    useCreateImmaterialResource(() => {});
  const { isPending: isPendingUpdate, updateImmaterialResourceBlueprint } =
    useUpdateImmaterialResource(() => {});
  const { isPending: isPendingDelete, deleteImmaterialResourceBlueprint } =
    useDeleteImmaterialResource(() => {});

  const [state, dispatch] = useReducer(
    immaterialResourceReducer,
    immaterialResource ?? immaterialResourceInitialValue
  );
  const [resetHappened, setResetHappened] = useState(false);

  // Update local state when data is fetched
  useEffect(() => {
    if (immaterialResource) {
      dispatch({
        type: "SET_ITEM",
        payload: immaterialResource,
      });
      setResetHappened(true);
    }
  }, [immaterialResource]);

  const disableChanges = !editMode || !state.editable;
  const isSavingChangesDisallowed = () => {
    return !state.name;
  };

  if (
    isPendingPost ||
    isPendingUpdate ||
    isPendingDelete ||
    (immaterialResourceId !== null && (isLoading || !resetHappened))
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
        <FormRowVertical label="Refreshes on">
          <Dropdown
            valuesList={refreshTypeOptions}
            chosenValue={state.refreshesOn}
            setChosenValue={(e) =>
              dispatch({
                type: "UPDATE_FIELD",
                field: "refreshesOn",
                value: e,
              })
            }
          ></Dropdown>
        </FormRowVertical>
      </Container>
      <UDButtonContainer>
        {immaterialResourceId === null && (
          <Button
            onClick={() =>
              createImmaterialResourceBlueprint(
                state as ImmaterialResourceBlueprint
              )
            }
            disabled={isSavingChangesDisallowed() || disableChanges}
          >
            Save
          </Button>
        )}
        {immaterialResourceId !== null && (
          <Button
            onClick={() =>
              updateImmaterialResourceBlueprint(
                state as ImmaterialResourceBlueprint
              )
            }
            disabled={isSavingChangesDisallowed() || disableChanges}
          >
            {disableChanges ? "You cannot edit this object" : "Update"}
          </Button>
        )}
        {immaterialResourceId !== null && (
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
                resourceName={"Immaterial Resource"}
                onConfirm={() =>  deleteImmaterialResourceBlueprint(immaterialResourceId)}
                />
            </Modal.Window>
          </Modal>
        )}
      </UDButtonContainer>
    </>
  );
}

ImmaterialResourceForm.defaultProps = {
  immaterialResourceId: null,
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