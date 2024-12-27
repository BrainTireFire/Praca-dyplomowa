import { useCallback, useContext } from "react";
import Dropdown from "../../../ui/forms/Dropdown";
import FormRowLabelRight from "../../../ui/forms/FormRowLabelRight";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import RadioGroup from "../../../ui/forms/RadioGroup";
import { damageTypesDropdown } from "../../effects/damageTypes";
import { DiceSetExtended, DiceSetForm } from "../../effects/DiceSetForm";
import { weightsDropdown } from "../enums/weight";
import { ItemAction } from "../ItemForm";
import { MeleeWeaponBody } from "../models/item";
import styled, { css } from "styled-components";
import { EditModeContext } from "../../../context/EditModeContext";

export default function MeleeWeaponForm({
  body,
  dispatch,
}: {
  body: MeleeWeaponBody;
  dispatch: (value: ItemAction) => void;
}) {
  const { editMode } = useContext(EditModeContext);
  const handleValueFormStateUpdate = useCallback(
    (e: DiceSetExtended) => {
      dispatch({
        type: "UPDATE_MELEE_WEAPON_BODY",
        field: "damage",
        value: e,
      });
    },
    [dispatch]
  );
  return (
    <Grid>
      <Row>
        <FormRowVertical label={"Damage"}>
          <DiceSetForm
            disabled={!editMode}
            onChange={handleValueFormStateUpdate}
            diceSet={body.damage as DiceSetExtended}
            useExtendedValues={false}
          ></DiceSetForm>
        </FormRowVertical>
        <FormRowVertical label="Damage type">
          <Dropdown
            disabled={!editMode}
            valuesList={damageTypesDropdown}
            chosenValue={body.damageType}
            setChosenValue={(value) =>
              dispatch({
                type: "UPDATE_MELEE_WEAPON_BODY",
                field: "damageType",
                value: value,
              })
            }
          ></Dropdown>
        </FormRowVertical>
      </Row>
      <Row>
        <FormRowVertical label="Properties">
          <FormRowLabelRight label="Finesse">
            <Input
              disabled={!editMode}
              type="checkbox"
              checked={body.finesse}
              onChange={(e) =>
                dispatch({
                  type: "UPDATE_MELEE_WEAPON_BODY",
                  field: "finesse",
                  value: e.target.checked,
                })
              }
            ></Input>
          </FormRowLabelRight>
          <FormRowLabelRight label="Reach">
            <Input
              disabled={!editMode}
              type="checkbox"
              checked={body.reach}
              onChange={(e) =>
                dispatch({
                  type: "UPDATE_MELEE_WEAPON_BODY",
                  field: "reach",
                  value: e.target.checked,
                })
              }
            ></Input>
          </FormRowLabelRight>
          <FormRowLabelRight label="Thrown">
            <Input
              disabled={!editMode}
              type="checkbox"
              checked={body.thrown}
              onChange={(e) =>
                dispatch({
                  type: "UPDATE_MELEE_WEAPON_BODY",
                  field: "thrown",
                  value: e.target.checked,
                })
              }
            ></Input>
          </FormRowLabelRight>
        </FormRowVertical>
        <FormRowVertical label="Throw range">
          <Input
            disabled={!body.thrown || !editMode}
            type="number"
            value={body.rangeThrown}
            onChange={(e) =>
              dispatch({
                type: "UPDATE_MELEE_WEAPON_BODY",
                field: "rangeThrown",
                value: e.target.value,
              })
            }
          ></Input>
        </FormRowVertical>
        <FormRowVertical label={"Weight property"}>
          <RadioGroup
            disabled={!editMode}
            values={weightsDropdown}
            label=""
            name="Weight"
            currentValue={body.weightProperty}
            onChange={(value) =>
              dispatch({
                type: "UPDATE_MELEE_WEAPON_BODY",
                field: "weightProperty",
                value: value,
              })
            }
          ></RadioGroup>
        </FormRowVertical>
      </Row>
    </Grid>
  );
}

const Grid = styled.div`
  display: grid;
  grid-template-rows: auto auto;
`;

const Row = styled.div`
  display: flex;
  flex-direction: row;
  gap: 10px;
`;
