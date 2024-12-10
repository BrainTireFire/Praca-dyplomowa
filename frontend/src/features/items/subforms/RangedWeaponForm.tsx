import { useCallback } from "react";
import Dropdown from "../../../ui/forms/Dropdown";
import FormRowLabelRight from "../../../ui/forms/FormRowLabelRight";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import RadioGroup from "../../../ui/forms/RadioGroup";
import { damageTypesDropdown } from "../../effects/damageTypes";
import { DiceSetExtended, DiceSetForm } from "../../effects/DiceSetForm";
import { weightsDropdown } from "../enums/weight";
import { ItemAction } from "../ItemForm";
import { MeleeWeaponBody, RangedWeaponBody } from "../models/item";

export default function RangedWeaponForm({
  body,
  dispatch,
}: {
  body: RangedWeaponBody;
  dispatch: (value: ItemAction) => void;
}) {
  const handleValueFormStateUpdate = useCallback(
    (e: DiceSetExtended) => {
      dispatch({
        type: "UPDATE_RANGED_WEAPON_BODY",
        field: "damage",
        value: e,
      });
    },
    [dispatch]
  );
  return (
    <div>
      <FormRowVertical label="Damage">
        <DiceSetForm
          onChange={handleValueFormStateUpdate}
          diceSet={body.damage as DiceSetExtended}
          useExtendedValues={false}
        ></DiceSetForm>
      </FormRowVertical>
      <FormRowVertical label="Damage type">
        <Dropdown
          valuesList={damageTypesDropdown}
          chosenValue={body.damageType}
          setChosenValue={(value) =>
            dispatch({
              type: "UPDATE_RANGED_WEAPON_BODY",
              field: "damageType",
              value: value,
            })
          }
        ></Dropdown>
      </FormRowVertical>
      <FormRowVertical label="Range">
        <Input
          type="number"
          value={body.range}
          onChange={(value) =>
            dispatch({
              type: "UPDATE_RANGED_WEAPON_BODY",
              field: "range",
              value: value,
            })
          }
        ></Input>
      </FormRowVertical>
      <FormRowLabelRight label="Requires reloading">
        <Input
          type="checkbox"
          checked={body.loaded}
          onChange={(e) =>
            dispatch({
              type: "UPDATE_RANGED_WEAPON_BODY",
              field: "loaded",
              value: e.target.checked,
            })
          }
        ></Input>
      </FormRowLabelRight>
    </div>
  );
}
