import styled from "styled-components";
import { ItemAction } from "../ItemForm";
import { EquippableItemBody } from "../models/item";
import EffectTable from "../tables/EffectTable";
import PowersTable from "../tables/PowersTable";
import ResourcesTable from "../tables/ResourcesTable";
import SlotsTable from "../tables/SlotsTable";
import Input from "../../../ui/forms/Input";
import FormRowLabelRight from "../../../ui/forms/FormRowLabelRight";
import EffectOnItemTable from "../tables/EffectOnItemTable";
import { useContext } from "react";
import { EditModeContext } from "../../../context/EditModeContext";
import { ItemContext } from "../../../context/ItemContext";

export default function EquippableItemForm({
  body,
  dispatch,
}: {
  body: EquippableItemBody;
  dispatch: (value: ItemAction) => void;
}) {
  
  const { editMode } = useContext(EditModeContext);
  
  const { objectType: itemObjectType } = useContext(ItemContext);
  return (
    <>
      <FormRowLabelRight label="Occupies all slots">
        <Input
          disabled={!editMode}
          type="checkbox"
          checked={body.occupiesAllSlots}
          onChange={(e) =>
            dispatch({
              type: "UPDATE_EQUIPPABLE_ITEM_BODY",
              field: "occupiesAllSlots",
              value: e.target.checked,
            })
          }
        ></Input>
      </FormRowLabelRight>
      <FormRowLabelRight label="Spell focus">
        <Input
          disabled={!editMode}
          type="checkbox"
          checked={body.isSpellFocus}
          onChange={(e) =>
            dispatch({
              type: "UPDATE_EQUIPPABLE_ITEM_BODY",
              field: "isSpellFocus",
              value: e.target.checked,
            })
          }
        ></Input>
      </FormRowLabelRight>
      <Grid>
        <SlotsTable slots={body.slots}></SlotsTable>
        <PowersTable powers={body.powers}></PowersTable>
        <ResourcesTable resources={body.resourcesOnEquip}></ResourcesTable>
        <EffectTable effects={body.effectsOnWearer}></EffectTable>
        {itemObjectType === "Weapon" && <EffectOnItemTable effects={body.effectsOnItem}></EffectOnItemTable>}
      </Grid>
    </>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-columns: auto auto;
  grid-template-rows: auto auto auto;
`;
