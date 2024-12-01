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
import { ImmaterialResource } from "../models/power";

export default function MatierialResourceTable({
  materialComponents,
}: {
  materialComponents: ImmaterialResource[];
}) {
  return (
    <Menus>
      <Table
        header="Material resources"
        button="Add new"
        columns="1fr 1fr 0.01rem"
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
  );
}

function MaterialComponentRow({
  materialComponent,
}: {
  materialComponent: ImmaterialResource;
}) {
  return (
    <Table.Row>
      <Cell>{materialComponent.name}</Cell>
      <Cell>{coinPursePrint(materialComponent.worth)}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={materialComponent.id} />
          <Menus.List id={materialComponent.id}>
            <Menus.Button icon={<HiEye />} onClick={() => alert("Test")}>
              Test 1
            </Menus.Button>

            <Menus.Button
              icon={<HiArrowDownOnSquare />}
              onClick={() => alert("Test")}
            >
              Test 2
            </Menus.Button>

            <Menus.Button
              icon={<HiArrowUpOnSquare />}
              onClick={() => alert("Test")}
            >
              Test 3
            </Menus.Button>

            <Modal.Open opens="delete">
              <Menus.Button icon={<HiTrash />}>Test 4</Menus.Button>
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
