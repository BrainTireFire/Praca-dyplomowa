import { useState } from "react";
import {
  apparelBodyInitialValue,
  itemInitialValue,
  meleeWeaponBodyInitialValue,
  mundaneItemBodyInitialValue,
  rangedWeaponBodyInitialValue,
} from "../../features/items/models/item";
import Button from "../../ui/interactive/Button";
import { ItemIdentity } from "./itemTypes";
import ItemForm from "../../features/items/ItemForm";
import Heading from "../../ui/text/Heading";

export default function CreateNewItemForm({onCloseModal}: {onCloseModal: any}) {
  const [itemType, setItemType] = useState<ItemIdentity | null>(null);
  return (
    <>
      {itemType === null && (
        <>
          <Heading as="h2">What kind of item do you wish to create?</Heading>
          <div style={{ display: "flex", gap: "5px" }}>
            <Button onClick={() => setItemType("MeleeWeapon")}>
              Melee weapon
            </Button>
            <Button onClick={() => setItemType("RangedWeapon")}>
              Ranged weapon
            </Button>
            <Button onClick={() => setItemType("Apparel")}>Apparel</Button>
            <Button onClick={() => setItemType("MundaneItem")}>Mundane item</Button>
          </div>
        </>
      )}
      {itemType === "MeleeWeapon" && (
        <ItemForm onCloseModal={onCloseModal}
          initialFormValues={{
            ...itemInitialValue,
            itemType: "MeleeWeapon",
            itemTypeBody: meleeWeaponBodyInitialValue,
          }}
        ></ItemForm>
      )}
      {itemType === "RangedWeapon" && (
        <ItemForm onCloseModal={onCloseModal}
          initialFormValues={{
            ...itemInitialValue,
            itemType: "RangedWeapon",
            itemTypeBody: rangedWeaponBodyInitialValue,
          }}
        ></ItemForm>
      )}
      {itemType === "Apparel" && (
        <ItemForm onCloseModal={onCloseModal}
          initialFormValues={{
            ...itemInitialValue,
            itemType: "Apparel",
            itemTypeBody: apparelBodyInitialValue,
          }}
        ></ItemForm>
      )}
      {itemType === "MundaneItem" && (
        <ItemForm onCloseModal={onCloseModal}
          initialFormValues={{
            ...itemInitialValue,
            itemType: "MundaneItem",
            itemTypeBody: mundaneItemBodyInitialValue,
          }}
        ></ItemForm>
      )}
    </>
  );
}


CreateNewItemForm.defaultProps = {
  onCloseModal: () => {}
}