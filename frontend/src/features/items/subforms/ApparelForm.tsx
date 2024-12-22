import styled, { css } from "styled-components";
import FormRowLabelRight from "../../../ui/forms/FormRowLabelRight";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import { ItemAction } from "../ItemForm";
import { ApparelBody } from "../models/item";
import { EditModeContext } from "../../../context/EditModeContext";
import { useContext } from "react";

export default function ApparelForm({
  body,
  dispatch,
}: {
  body: ApparelBody;
  dispatch: (value: ItemAction) => void;
}) {
  const { editMode } = useContext(EditModeContext);
  return (
    <Grid>
      <FormRowLabelRight
        label="Disadvantage on stealth"
        customStyles={css`
          grid-column: 1;
        `}
      >
        <Input
          disabled={!editMode}
          type="checkbox"
          checked={body.disadvantageOnStealth}
          onChange={(e) =>
            dispatch({
              type: "UPDATE_APPAREL_BODY",
              field: "disadvantageOnStealth",
              value: e.target.checked,
            })
          }
        ></Input>
      </FormRowLabelRight>
      <FormRowVertical
        label="Minimum strength"
        customStyles={css`
          grid-column: 2;
        `}
      >
        <Input
          disabled={!editMode}
          type="number"
          value={body.minimumStrength}
          onChange={(e) =>
            dispatch({
              type: "UPDATE_APPAREL_BODY",
              field: "minimumStrength",
              value: e.target.value,
            })
          }
        ></Input>
      </FormRowVertical>
      <FormRowVertical
        label="Armor class"
        customStyles={css`
          grid-column: 3;
        `}
      >
        <Input
          disabled={!editMode}
          type="number"
          value={body.armorClass}
          onChange={(e) =>
            dispatch({
              type: "UPDATE_APPAREL_BODY",
              field: "armorClass",
              value: e.target.value,
            })
          }
        ></Input>
      </FormRowVertical>
    </Grid>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-columns: auto auto auto;
`;
