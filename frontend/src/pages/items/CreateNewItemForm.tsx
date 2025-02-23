import { useState } from "react";
import {
  apparelBodyInitialValue,
  itemInitialValue,
  meleeWeaponBodyInitialValue,
  rangedWeaponBodyInitialValue,
} from "../../features/items/models/item";
import Button from "../../ui/interactive/Button";
import { useCreateItem } from "../../features/items/hooks/useCreateItem";
import { ItemIdentity, ItemType } from "./itemTypes";
import ItemForm from "../../features/items/ItemForm";

export default function CreateNewItemForm() {
  const [itemType, setItemType] = useState<ItemIdentity | null>(null);
  return (
    <>
      {itemType === null && (
        <>
          <Button onClick={() => setItemType("MeleeWeapon")}>
            Melee weapon
          </Button>
          <Button onClick={() => setItemType("RangedWeapon")}>
            Ranged weapon
          </Button>
          <Button onClick={() => setItemType("Apparel")}>Apparel</Button>
        </>
      )}
      {itemType === "MeleeWeapon" && (
        <ItemForm
          initialFormValues={{
            ...itemInitialValue,
            itemType: "MeleeWeapon",
            itemTypeBody: meleeWeaponBodyInitialValue,
          }}
        ></ItemForm>
      )}
      {itemType === "RangedWeapon" && (
        <ItemForm
          initialFormValues={{
            ...itemInitialValue,
            itemType: "RangedWeapon",
            itemTypeBody: rangedWeaponBodyInitialValue,
          }}
        ></ItemForm>
      )}
      {itemType === "Apparel" && (
        <ItemForm
          initialFormValues={{
            ...itemInitialValue,
            itemType: "Apparel",
            itemTypeBody: apparelBodyInitialValue,
          }}
        ></ItemForm>
      )}
    </>
  );
}
