import React from "react";
import Table from "../../../ui/containers/Table";
import Modal from "../../../ui/containers/Modal";
import Menus from "../../../ui/containers/Menus";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import styled from "styled-components";
import RadioButton from "../../../ui/containers/RadioButton";
import { Item } from "../../../models/item";

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-grey-600);
`;

const Stacked = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.2rem;

  & span:first-child {
    font-weight: 500;
  }

  & span:last-child {
    color: var(--color-grey-500);
    font-size: 1.2rem;
  }
`;

export default function EquipmentRow({ equipment }: { equipment: Item }) {
  return (
    <Table.Row>
      <Cell>{equipment.name}</Cell>

      <Cell>
        {equipment.slots.reduce((accumulator, currentValue) => {
          return (accumulator += currentValue.name + ", ");
        }, "")}
      </Cell>

      <RadioButton
        //   label="Option 1"
        checked={equipment.equipped}
        //   checked={selectedOption === "option1"}
        //   onChange={handleRadioChange}
      />
      <Cell>{equipment.itemFamily.name}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={equipment.id} />
          <Menus.List id={equipment.id}>
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
