import { useContext, useEffect, useState } from "react";
import { useItemFamilies } from "./hooks/useItemFamilies";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import Dropdown from "../../ui/forms/Dropdown";
import Spinner from "../../ui/interactive/Spinner";
import Button from "../../ui/interactive/Button";
import { useCreateMaterialComponent } from "./hooks/useCreateMaterialComponent";
import { PowerIdContext } from "./contexts/PowerIdContext";

export default function MaterialComponentForm({
  materialComponentId,
}: {
  materialComponentId: number | null;
}) {
  const { powerId } = useContext(PowerIdContext);
  const { isLoading, itemFamilies, error } = useItemFamilies();

  const [goldPieces, setGoldPieces] = useState(0);
  const [silverPieces, setSilverPieces] = useState(0);
  const [copperPieces, setCopperPieces] = useState(0);

  const [selectedItemFamilyId, setSelectedItemFamilyId] = useState<
    null | number
  >(null);

  const { createMaterialComponent, isPending } = useCreateMaterialComponent(
    () => {},
    powerId
  );

  if (isLoading || isPending) {
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
      {materialComponentId == null && (
        <Button
          disabled={selectedItemFamilyId == null}
          onClick={() =>
            createMaterialComponent({
              id: selectedItemFamilyId ?? 0,
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
      {materialComponentId != null && <Button>Update</Button>}
    </>
  );
}
