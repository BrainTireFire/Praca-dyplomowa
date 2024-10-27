import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import EquipmentRow from "./EquipmentRow";
import styled from "styled-components";
import Modal from "../../../ui/containers/Modal";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import RadioButton from "../../../ui/containers/RadioButton";
import { Effect } from "../../../models/effect";
import { Cell } from "../../../ui/containers/Cell";

export default function ConstantEffectTable({
  effects,
}: {
  effects: Effect[];
}) {
  return (
    <Menus>
      <Table
        header="Constant effects"
        button="Add new"
        columns="1fr 1fr 0.01rem"
      >
        <Table.Header>
          <Cell>Name</Cell>
          <Cell>Source</Cell>
          <Cell></Cell>
        </Table.Header>
        <Table.Body
          data={effects}
          columnCount={3}
          render={(effect) => (
            <ConstantEffectRow key={effect.id} effect={effect} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function ConstantEffectRow({ effect }: { effect: Effect }) {
  return (
    <Table.Row>
      <Cell>{effect.name}</Cell>

      <Cell>{effect.source}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={effect.id} />
          <Menus.List id={effect.id}>
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
