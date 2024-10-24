import React from "react";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";
import Modal from "../../../ui/containers/Modal";
import Menus from "../../../ui/containers/Menus";
import { Board } from "../../../models/map/Board";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";

export default function EncounterMapRow({ board }: Board) {
  return (
    <Table.Row>
      <Cell>{board.name}</Cell>
      <Cell>{board.sizeX + " x " + board.sizeY}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={board.id} />
          <Menus.List id={board.id}>
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
        <Modal.Window name="delete"></Modal.Window>
      </Modal>
    </Table.Row>
  );
}
