import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import Modal from "../../../ui/containers/Modal";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import { Cell } from "../../../ui/containers/Cell";
import RadioButton from "../../../ui/containers/RadioButton";
import { EffectBlueprint } from "../../effects/EffectBlueprintForm";
import { ItemFamilyWithWorth } from "../../../models/itemfamily";
import { coinPursePrint } from "../../items/coinPurse";
import { ImmaterialResource, MaterialComponent } from "../models/power";
import MaterialComponentForm from "../MaterialComponentForm";
import { PowerIdContext } from "../contexts/PowerIdContext";

export default function MatierialResourceTable({
  materialComponents,
  powerId,
}: {
  materialComponents: MaterialComponent[];
  powerId: number;
}) {
  return (
    <PowerIdContext.Provider
      value={{
        powerId: powerId,
      }}
    >
      <Menus>
        <Table
          header="Material resources"
          button="Add new"
          columns="1fr 1fr 0.01rem"
          modal={<MaterialComponentForm materialComponentId={null} />}
        >
          <Table.Header>
            <div>Family</div>
            <div>Value</div>
            <div></div>
          </Table.Header>
          <Table.Body
            data={materialComponents}
            columnCount={3}
            render={(materialComponent) => (
              <MaterialComponentRow
                key={materialComponent.id}
                materialComponent={materialComponent}
              />
            )}
          />
          <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
        </Table>
      </Menus>
    </PowerIdContext.Provider>
  );
}

function MaterialComponentRow({
  materialComponent,
}: {
  materialComponent: ItemFamilyWithWorth;
}) {
  return (
    <Table.Row>
      <Cell>{materialComponent.name}</Cell>
      <Cell>{coinPursePrint(materialComponent.worth)}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={materialComponent.id} />
          <Menus.List id={materialComponent.id}>
            <Modal.Open opens="open">
              <Menus.Button icon={<HiEye />} onClick={() => {}}>
                Open
              </Menus.Button>
            </Modal.Open>
            <Modal.Open opens="delete">
              <Menus.Button icon={<HiTrash />} onClick={() => {}}>
                Delete
              </Menus.Button>
            </Modal.Open>
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="delete">
          {/* <ConfirmDelete
            resourceName="equipment"
            disabled={isDeleting}
            onConfirm={() => {
              deleteBooking(bookingId);
            }}
          /> */}
        </Modal.Window>
      </Modal>
    </Table.Row>
  );
}
