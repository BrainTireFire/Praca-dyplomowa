import { Reducer, useContext, useEffect, useReducer, useState } from "react";
import Box from "../../ui/containers/Box";
import Dropdown from "../../ui/forms/Dropdown";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import TextArea from "../../ui/forms/TextArea";
import Spinner from "../../ui/interactive/Spinner";
import { CoinPurseForm } from "./CoinPurseForm";
import { useItem } from "./hooks/useItem";
import {
  ApparelBody,
  EquippableItemBody,
  Item,
  itemInitialValue,
  MeleeWeaponBody,
  RangedWeaponBody,
} from "./models/item";
import { useItemFamilies } from "./hooks/useItemFamilies";
import styled, { css } from "styled-components";
import ApparelForm from "./subforms/ApparelForm";
import MeleeWeaponForm from "./subforms/MeleeWeaponForm";
import Button from "../../ui/interactive/Button";
import { useCreateItem } from "./hooks/useCreateItem";
import { useUpdateItem } from "./hooks/useUpdateItem";
import RangedWeaponForm from "./subforms/RangedWeaponForm";
import { ItemIdContext } from "./contexts/ItemIdContext";
import EquippableItemForm from "./subforms/EquippableItemForm";
import { EditModeContext } from "../../context/EditModeContext";

export type ItemAction =
  | { type: "SET_ITEM"; payload: Item }
  | { type: "UPDATE_FIELD"; field: keyof Item; value: any }
  | { type: "RESET_ITEM" }
  | { type: "RESET_FIELD"; field: keyof Item }
  | { type: "UPDATE_APPAREL_BODY"; field: keyof ApparelBody; value: any }
  | {
      type: "UPDATE_MELEE_WEAPON_BODY";
      field: keyof MeleeWeaponBody;
      value: any;
    }
  | {
      type: "UPDATE_RANGED_WEAPON_BODY";
      field: keyof RangedWeaponBody;
      value: any;
    }
  | { type: "UPDATE_GOLD"; value: number }
  | { type: "UPDATE_SILVER"; value: number }
  | { type: "UPDATE_COPPER"; value: number };

export const itemReducer: Reducer<Item, ItemAction> = (state, action) => {
  switch (action.type) {
    case "SET_ITEM":
      return { ...action.payload };

    case "UPDATE_FIELD":
      return { ...state, [action.field]: action.value };

    case "RESET_ITEM":
      return itemInitialValue;

    case "RESET_FIELD":
      return { ...state, [action.field]: itemInitialValue[action.field] };

    case "UPDATE_GOLD":
      return { ...state, price: { ...state.price, goldPieces: action.value } };

    case "UPDATE_SILVER":
      return {
        ...state,
        price: { ...state.price, silverPieces: action.value },
      };

    case "UPDATE_COPPER":
      return {
        ...state,
        price: { ...state.price, copperPieces: action.value },
      };
    case "UPDATE_MELEE_WEAPON_BODY":
      return {
        ...state,
        itemTypeBody: { ...state.itemTypeBody, [action.field]: action.value },
      };
    case "UPDATE_RANGED_WEAPON_BODY":
      return {
        ...state,
        itemTypeBody: { ...state.itemTypeBody, [action.field]: action.value },
      };
    case "UPDATE_APPAREL_BODY":
      return {
        ...state,
        itemTypeBody: { ...state.itemTypeBody, [action.field]: action.value },
      };

    default:
      throw new Error(`Unhandled action type: ${action["type"]}`);
  }
};

export default function ItemForm({
  itemId,
  initialFormValues,
}: {
  itemId: number | null;
  initialFormValues: Item;
}) {
  const { editMode } = useContext(EditModeContext);
  const { isPending: isPendingPost, createItem } = useCreateItem(() => {});
  const { isPending: isPendingUpdate, updateItem } = useUpdateItem(() => {});
  const { isLoading, item, error } = useItem(itemId);
  const [state, dispatch] = useReducer(itemReducer, item ?? initialFormValues);
  const [resetHappened, setResetHappened] = useState(false);
  // Update local state when data is fetched
  useEffect(() => {
    if (item) {
      dispatch({
        type: "SET_ITEM",
        payload: item,
      });
      setResetHappened(true);
    }
  }, [item]);
  const {
    isLoading: isLoadingItemFamilies,
    itemFamilies,
    error: errorItemFamilies,
  } = useItemFamilies();
  console.log(state);

  const isSavingChangesDisallowed = () => {
    return state.itemFamilyId === null || !state.name;
  };
  const disableUpdate = !editMode || !state.editable;

  if (
    isPendingPost ||
    isPendingUpdate ||
    (itemId !== null && (isLoading || !resetHappened || isLoadingItemFamilies))
  )
    return <Spinner></Spinner>;
  if (error || errorItemFamilies) return <>Error</>;
  return (
    <Container>
      <EditModeContext.Provider value={{ editMode: !disableUpdate }}>
        <ItemIdContext.Provider
          value={{
            itemId: itemId,
          }}
        >
          <GridContainer>
            <Grid>
              <Row1>
                <FormRowVertical
                  label="Name"
                  error={!state.name ? "Name must be filled" : undefined}
                >
                  <Input
                    disabled={disableUpdate}
                    type="text"
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
                <FormRowVertical label="Description" fillHeight={true}>
                  <TextArea
                    disabled={disableUpdate}
                    value={state.description}
                    onChange={(e) =>
                      dispatch({
                        type: "UPDATE_FIELD",
                        field: "description",
                        value: e.target.value,
                      })
                    }
                    customStyles={css`
                      width: auto;
                    `}
                  ></TextArea>
                </FormRowVertical>
              </Row1>
              <Row2>
                <FormRowVertical
                  label="Item family"
                  customStyles={css`
                    grid-column: 1;
                  `}
                >
                  <Dropdown
                    disabled={disableUpdate}
                    valuesList={
                      itemFamilies?.map((family) => {
                        return {
                          label: family.name,
                          value: family.id.toString(),
                        };
                      }) ?? []
                    }
                    chosenValue={state.itemFamilyId?.toString() ?? null}
                    setChosenValue={(value) =>
                      dispatch({
                        type: "UPDATE_FIELD",
                        field: "itemFamilyId",
                        value: Number(value),
                      })
                    }
                  ></Dropdown>
                </FormRowVertical>
                <FormRowVertical
                  label="Weight"
                  customStyles={css`
                    grid-column: 2;
                  `}
                >
                  <Input
                    disabled={disableUpdate}
                    type="number"
                    value={state.weight}
                    onChange={(e) =>
                      dispatch({
                        type: "UPDATE_FIELD",
                        field: "weight",
                        value: Number(e.target.value),
                      })
                    }
                  ></Input>
                </FormRowVertical>
                <FormRowVertical
                  label="Cost"
                  customStyles={css`
                    grid-column: 3;
                  `}
                >
                  <CoinPurseForm
                    disabled={disableUpdate}
                    value={state.price}
                    onGoldChange={(e) =>
                      dispatch({
                        type: "UPDATE_GOLD",
                        value: Number(e.target.value),
                      })
                    }
                    onSilverChange={(e) =>
                      dispatch({
                        type: "UPDATE_SILVER",
                        value: Number(e.target.value),
                      })
                    }
                    onCopperChange={(e) =>
                      dispatch({
                        type: "UPDATE_COPPER",
                        value: Number(e.target.value),
                      })
                    }
                  ></CoinPurseForm>
                </FormRowVertical>
              </Row2>
              <Row3>
                {state.itemType === "Apparel" && (
                  <ApparelForm
                    body={state.itemTypeBody as ApparelBody}
                    dispatch={dispatch}
                  ></ApparelForm>
                )}
                {state.itemType === "MeleeWeapon" && (
                  <MeleeWeaponForm
                    body={state.itemTypeBody as MeleeWeaponBody}
                    dispatch={dispatch}
                  ></MeleeWeaponForm>
                )}
                {state.itemType === "RangedWeapon" && (
                  <RangedWeaponForm
                    body={state.itemTypeBody as RangedWeaponBody}
                    dispatch={dispatch}
                  ></RangedWeaponForm>
                )}
                {itemId && (
                  <>
                    {(state.itemType === "RangedWeapon" ||
                      state.itemType === "MeleeWeapon" ||
                      state.itemType === "Apparel") && (
                      <EquippableItemForm
                        body={state.itemTypeBody as EquippableItemBody}
                      ></EquippableItemForm>
                    )}
                  </>
                )}
              </Row3>
            </Grid>
          </GridContainer>
          {itemId === null && (
            <Button
              onClick={() => createItem(state)}
              disabled={isSavingChangesDisallowed() || disableUpdate}
            >
              Save to unlock more configuration options
            </Button>
          )}
          {itemId !== null && (
            <Button
              onClick={() => updateItem(state)}
              disabled={isSavingChangesDisallowed() || disableUpdate}
            >
              {disableUpdate ? "You cannot edit this object" : "Update"}
            </Button>
          )}
        </ItemIdContext.Provider>
      </EditModeContext.Provider>
    </Container>
  );
}

const Container = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
  max-height: 100%;
`;

const GridContainer = styled(Box)`
  flex: 1;
  overflow-y: hidden;
  padding: 0rem 0rem 0rem 1rem;
`;

const Grid = styled.div`
  display: grid;
  grid-template-rows: auto auto auto;
  column-gap: 2px;
  height: 100%;
  overflow-y: auto;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  scrollbar-gutter: stable;
`;

const Row1 = styled.div`
  display: flex;
  flex-direction: row;
  grid-row: 1/2;
  gap: 2px;
`;
const Row2 = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
  grid-row: 2/3;
  column-gap: 2px;
`;
const Row3 = styled.div`
  display: flex;
  flex-direction: column;
  grid-template-rows: auto auto;
  grid-row: 3/4;
  row-gap: 2px;
`;

ItemForm.defaultProps = {
  itemId: null,
  initialFormValues: itemInitialValue,
};
