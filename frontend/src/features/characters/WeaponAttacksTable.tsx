import React from "react";
import Menus from "../../ui/containers/Menus";
import Table from "../../ui/containers/Table";
import styled from "styled-components";

import Modal from "../../ui/containers/Modal";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import RadioButton from "../../ui/containers/RadioButton";

const weaponAttacks = [
  {
    id: 1,
    main: true,
    damage: "2d4+2",
    damageType: "Bludgeoning",
    range: 5,
  },
  {
    id: 2,
    main: false,
    damage: "1d4+2",
    damageType: "Piercing",
    range: 5,
  },
];

export default function WeaponAttackTable() {
  return (
    <Menus>
      <Table header="Weapon attack" columns="1fr 1fr 1fr 1fr">
        <Table.Header>
          <div>Main</div>
          <div>Damage</div>
          <div>D. type</div>
          <div>Range</div>
        </Table.Header>
        <Table.Body
          data={weaponAttacks}
          render={(weaponAttack) => (
            <WeaponAttackRow
              key={weaponAttack.id}
              weaponAttack={weaponAttack}
            />
          )}
        />
      </Table>
    </Menus>
  );
}

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-grey-600);
`;

function WeaponAttackRow({ weaponAttack }) {
  return (
    <Table.Row>
      <RadioButton
        //   label="Option 1"
        checked={weaponAttack.main}
        //   checked={selectedOption === "option1"}
        //   onChange={handleRadioChange}
      />

      <Cell>{weaponAttack.damage}</Cell>
      <Cell>{weaponAttack.damageType}</Cell>
      <Cell>{weaponAttack.range}</Cell>
    </Table.Row>
  );
}
