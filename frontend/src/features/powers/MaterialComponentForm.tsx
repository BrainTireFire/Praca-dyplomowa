import { useContext, useEffect, useState } from "react";
import { useItemFamilies } from "./hooks/useItemFamilies";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Dropdown from "../../ui/forms/Dropdown";
import Spinner from "../../ui/interactive/Spinner";
import Button from "../../ui/interactive/Button";
import { useCreateMaterialComponent } from "./hooks/useCreateMaterialComponent";
import { PowerIdContext } from "./contexts/PowerIdContext";
import { useUpdateMaterialComponent } from "./hooks/useUpdateMaterialComponent";
import { MaterialComponent } from "./models/power";

export default function MaterialComponentForm({
  materialComponent,
  onCloseModal
}: {
  materialComponent: MaterialComponent;
  onCloseModal: any
}) {
  const { powerId } = useContext(PowerIdContext);
  const { isLoading, itemFamilies, error } = useItemFamilies(powerId);

  const [goldPieces, setGoldPieces] = useState(
    materialComponent.worth.goldPieces
  );
  const [silverPieces, setSilverPieces] = useState(
    materialComponent.worth.silverPieces
  );
  const [copperPieces, setCopperPieces] = useState(
    materialComponent.worth.copperPieces
  );

  const [selectedItemFamilyId, setSelectedItemFamilyId] = useState<
    null | number
  >(materialComponent.itemFamilyId);

  const { createMaterialComponent, isPending: isPendingCreate } =
    useCreateMaterialComponent(() => {onCloseModal()}, powerId);
  const { updateMaterialComponent, isPending: isPendingUpdate } =
    useUpdateMaterialComponent(() => {onCloseModal()}, powerId);

  if (isLoading || isPendingCreate || isPendingUpdate) {
    return <Spinner></Spinner>;
  }

  if (error) {
    return <>Error</>;
  }

  return (
    <>
      <FormRowVertical label={"Gold pieces cost"}>
        <Input
          value={goldPieces}
          type="number"
          onChange={(e) => setGoldPieces(Number(e.target.value))}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label={"Silver pieces cost"}>
        <Input
          value={silverPieces}
          type="number"
          onChange={(e) => setSilverPieces(Number(e.target.value))}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label={"Copper pieces cost"}>
        <Input
          value={copperPieces}
          type="number"
          onChange={(e) => setCopperPieces(Number(e.target.value))}
        ></Input>
      </FormRowVertical>
      <FormRowVertical label={"Item family"}>
        <Dropdown
          valuesList={
            itemFamilies?.map((family) => {
              return {
                label: family.name,
                value: family.id.toString(),
              };
            }) ?? []
          }
          chosenValue={selectedItemFamilyId?.toString() ?? null}
          setChosenValue={(value) => setSelectedItemFamilyId(Number(value))}
        ></Dropdown>
      </FormRowVertical>
      {materialComponent.id == null && (
        <Button
          disabled={selectedItemFamilyId == null}
          onClick={() =>
            createMaterialComponent({
              id: -1,
              itemFamilyId: Number(selectedItemFamilyId),
              name: "",
              worth: {
                goldPieces: goldPieces,
                silverPieces: silverPieces,
                copperPieces: copperPieces,
              },
            })
          }
        >
          Save
        </Button>
      )}
      {materialComponent.id != null && (
        <Button
          onClick={() => {
            updateMaterialComponent({
              id: materialComponent.id,
              itemFamilyId: Number(selectedItemFamilyId),
              name: "",
              worth: {
                goldPieces: goldPieces,
                silverPieces: silverPieces,
                copperPieces: copperPieces,
              },
            });
          }}
        >
          Update
        </Button>
      )}
    </>
  );
}

MaterialComponentForm.defaultProps = {
  onCloseModal: () => {}
}