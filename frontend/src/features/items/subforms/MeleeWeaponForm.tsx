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
import { MeleeWeaponBody } from "../models/item";

export default function MeleeWeaponForm({
  body,
  dispatch,
}: {
  body: MeleeWeaponBody;
  dispatch: (value: ItemAction) => void;
}) {
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
    <div>
      <FormRowLabelRight label="Finesse">
        <Input
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
      <FormRowVertical label={"Weight property"}>
        <RadioGroup
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
      <FormRowVertical label={"Damage"}>
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
              type: "UPDATE_MELEE_WEAPON_BODY",
              field: "damageType",
              value: value,
            })
          }
        ></Dropdown>
      </FormRowVertical>
      <FormRowLabelRight label="Thrown">
        <Input
          type="checkbox"
          checked={body.throwable}
          onChange={(e) =>
            dispatch({
              type: "UPDATE_MELEE_WEAPON_BODY",
              field: "throwable",
              value: e.target.checked,
            })
          }
        ></Input>
      </FormRowLabelRight>
      <FormRowVertical label="Throw range">
        <Input
          disabled={!body.throwable}
          type="number"
          value={body.rangeThrowable}
          onChange={(e) =>
            dispatch({
              type: "UPDATE_MELEE_WEAPON_BODY",
              field: "rangeThrowable",
              value: e.target.value,
            })
          }
        ></Input>
      </FormRowVertical>
    </div>
  );
}
