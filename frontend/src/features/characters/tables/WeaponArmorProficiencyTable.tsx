import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import EquipmentRow from "./EquipmentRow";
import styled from "styled-components";
import ProficiencyRow from "./ProficiencyRow";
import { ItemFamily } from "../../../models/itemfamily";

export default function WeaponAndArmorProficiencyTable({
  weaponAndArmorProficiencies,
}: {
  weaponAndArmorProficiencies: ItemFamily[];
}) {
  console.log(weaponAndArmorProficiencies);
  return (
    <Menus>
      <Table header="Item proficiency" columns="1fr">
        <Table.Body
          columnCount={1}
          data={weaponAndArmorProficiencies}
          render={(item) => <ProficiencyRow key={item.id} item={item} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}
