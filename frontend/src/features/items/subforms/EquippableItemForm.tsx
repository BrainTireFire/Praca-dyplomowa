import Table from "../../../ui/containers/Table";
import { ItemAction } from "../ItemForm";
import { EquippableItemBody } from "../models/item";
import SlotsTable from "../tables/SlotsTable";

export default function EquippableItemForm({
  body,
}: {
  body: EquippableItemBody;
}) {
  return (
    <>
      <SlotsTable slots={body.slots}></SlotsTable>
    </>
  );
}
