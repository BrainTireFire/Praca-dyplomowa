import Table from "../../../ui/containers/Table";
import { ItemAction } from "../ItemForm";
import { EquippableItemBody } from "../models/item";
import EffectTable from "../tables/EffectTable";
import PowersTable from "../tables/PowersTable";
import ResourcesTable from "../tables/ResourcesTable";
import SlotsTable from "../tables/SlotsTable";

export default function EquippableItemForm({
  body,
}: {
  body: EquippableItemBody;
}) {
  return (
    <>
      <SlotsTable slots={body.slots}></SlotsTable>
      <PowersTable powers={body.powers}></PowersTable>
      <ResourcesTable resources={body.resourcesOnEquip}></ResourcesTable>
      <EffectTable effects={body.effectsOnWearer}></EffectTable>
    </>
  );
}
