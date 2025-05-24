import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import { Cell } from "../../../ui/containers/Cell";

import RadioButton from "../../../ui/containers/RadioButton";
import { WeaponAttack } from "../../../models/weaponattack";
import { DiceSetString } from "../../../models/diceset";
import { DamageType } from "../../../models/damageType";

export default function WeaponAttackTable({
  weaponAttacks,
}: {
  weaponAttacks: WeaponAttack[];
}) {
  return (
    <Menus>
      <Table header="Weapon attack" columns="auto auto auto auto auto">
        <Table.Header>
          <div>Main</div>
          <div>Damage</div>
          <div>Attack bonus</div>
          <div>D. type</div>
          <div>Reach/Range</div>
        </Table.Header>
        <Table.Body
        columnCount={5}
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

function WeaponAttackRow({ weaponAttack }: { weaponAttack: WeaponAttack }) {
  return (
    <Table.Row>
      <Cell>
        <RadioButton
          //   label="Option 1"
          checked={weaponAttack.main}
          //   checked={selectedOption === "option1"}
          //   onChange={handleRadioChange}
        />  
      </Cell>

      <Cell>{DiceSetString(weaponAttack.damage)}</Cell>
      <Cell>{DiceSetString(weaponAttack.attackBonus)}</Cell>
      <Cell>{DamageType(weaponAttack.damageType)}</Cell>
      <Cell>
        {weaponAttack.reach ?? "-"}/{weaponAttack.range ?? "-"}
      </Cell>
    </Table.Row>
  );
}
