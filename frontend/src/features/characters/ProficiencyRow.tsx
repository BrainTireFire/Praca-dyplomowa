import React from "react";
import Table from "../../ui/containers/Table";
import Modal from "../../ui/containers/Modal";
import Menus from "../../ui/containers/Menus";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import styled from "styled-components";
import RadioButton from "../../ui/containers/RadioButton";
import { ItemFamily } from "../../models/itemfamily";
import { Language } from "../../models/language";

const Name = styled.div`
  font-size: 1rem;
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

export default function ProficiencyRow({
  item,
}: {
  item: ItemFamily | Language;
}) {
  return (
    <Table.Row>
      <Name>{item.name}</Name>

      {/* <Modal>
        <Menus.Menu>
          <Menus.Toggle id={item.id} />
          <Menus.List id={tool.id}>
            <Modal.Open opens="delete">
              <Menus.Button icon={<HiTrash />}>Test 4</Menus.Button>
            </Modal.Open>
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="delete">
          <ConfirmDelete
            resourceName="equipment"
            disabled={isDeleting}
            onConfirm={() => {
              deleteBooking(bookingId);
            }}
          />
        </Modal.Window>
      </Modal> */}
    </Table.Row>
  );
}
