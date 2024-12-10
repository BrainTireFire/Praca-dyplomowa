import FormRowLabelRight from "../../../ui/forms/FormRowLabelRight";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import { ItemAction } from "../ItemForm";
import { ApparelBody } from "../models/item";

export default function ApparelForm({
  body,
  dispatch,
}: {
  body: ApparelBody;
  dispatch: (value: ItemAction) => void;
}) {
  return (
    <>
      <FormRowLabelRight label="Disadvantage on stealth">
        <Input
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
      <FormRowVertical label="Minimum strength">
        <Input
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
      <FormRowVertical label="Armor class">
        <Input
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
    </>
  );
}
