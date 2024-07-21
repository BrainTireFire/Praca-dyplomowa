import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";

import RadioButton from "../../../ui/containers/RadioButton";
import { WeaponAttack } from "../../../models/weaponattack";

export default function WeaponAttackTable({
  weaponAttacks,
}: {
  weaponAttacks: WeaponAttack[];
}) {
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

function WeaponAttackRow({ weaponAttack }: { weaponAttack: WeaponAttack }) {
  return (
    <Table.Row>
      <RadioButton
        //   label="Option 1"
        checked={weaponAttack.main}
        //   checked={selectedOption === "option1"}
        //   onChange={handleRadioChange}
      />

      <Cell>
        {Object.entries(weaponAttack.damage).reduce(
          (accumulator, currentValue) => {
            if (accumulator === "") {
              return (
                accumulator +
                (currentValue[1] > 0 ? currentValue[1] + currentValue[0] : "")
              );
            } else {
              return (
                accumulator +
                (currentValue[1] > 0
                  ? "+" + currentValue[1] + currentValue[0]
                  : "")
              );
            }
          },
          ""
        )}
      </Cell>
      <Cell>{weaponAttack.damageType}</Cell>
      <Cell>{weaponAttack.range}</Cell>
    </Table.Row>
  );
}
