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
import { Cell } from "../../../ui/containers/Cell";
import ConfirmDelete from "../../../ui/containers/ConfirmDelete";
import { useDeleteItem } from "../hooks/useDeleteItem";
import ItemForm from "../../items/ItemForm";

const Stacked = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.2rem;

  & span:first-child {
    font-weight: 500;
  }

  & span:last-child {
    color: var(--color-secondary-text);
    font-size: 1.2rem;
  }
`;

export default function EquipmentRow({ equipment }: { equipment: Item }) {
  const { isPending, deleteItem } = useDeleteItem(() => {});
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
          <ConfirmDelete
            resourceName="effect instance"
            disabled={isPending}
            onConfirm={() => {
              deleteItem(equipment.id);
            }}
          />
        </Modal.Window>
        <Modal.Window name="open">
          <ItemForm itemId={equipment.id}></ItemForm>
        </Modal.Window>
      </Modal>
    </Table.Row>
  );
}
